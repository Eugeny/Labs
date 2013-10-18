import numpy as np
import time


class Request:
    pass


class Scheme:
    def __init__(self):
        self.blocks = []
        self.t = 0
        self.c_total_drop = 0
        self.c_total_in = 1
        self.c_total_out = 0

    def add(self, b):
        b.scheme = self
        self.blocks.append(b)

    def step(self, dt):
        for b in self.blocks:
            b.step(self.t)
        self.t += dt


class Block:
    def __init__(self):
        self.output = None
        self.counter = 0

    def can_recv(self):
        pass

    def recv(self, r):
        pass

    def step(self, t):
        pass

    def can_out(self):
        if type(self.output) == list:
            outs = [_ for _ in self.output if _.can_recv()]
        else:
            outs = [self.output] if self.output.can_recv() else []
        if outs:
            return outs[0]
        return None

    def out(self, r):
        if self.can_out():
            self.can_out().recv(r)
            self.counter += 1
        else:
            self.scheme.c_total_drop += 1


class Sink (Block):
    def can_recv(self):
        return True

    def recv(self, r):
        self.scheme.c_total_out += 1


class Input (Block):
    def __init__(self, l):
        Block.__init__(self)
        self.l = l
        self.tnext = 0

    def step(self, t):
        self.generated = False
        if t > self.tnext:
            self.generated = True
            self.scheme.c_total_in += 1
            self.tnext = t + np.random.poisson(self.l)
            self.out(Request())


class Buffer (Block):
    def __init__(self, c):
        Block.__init__(self)
        self.capacity = c
        self.buf = []

    def can_recv(self):
        return len(self.buf) < self.capacity

    def recv(self, r):
        self.buf.append(r)

    def step(self, t):
        if self.can_out() and len(self.buf):
            self.out(self.buf.pop(0))


class Worker (Block):
    def __init__(self, l):
        Block.__init__(self)
        self.l = l
        self.req = None
        self.tout = 0
        self.blocked = False

    def can_recv(self):
        return not self.req

    def recv(self, r):
        self.req = r

    def step(self, t):
        if self.req:
            if not self.tout:
                self.tout = t + np.random.poisson(self.l)
            if t > self.tout:
                self.blocked = True
                if self.can_out():
                    self.out(self.req)
                    self.blocked = False
                    self.tout = None
                    self.req = None


DT = 0.1
L1 = 10
L2 = 10

s = Scheme()
i = Input(2)
h1 = Buffer(L1)
h2 = Buffer(L2)
k11 = Worker(7)
k12 = Worker(7)
k21 = Worker(7)
k22 = Worker(7)
k3 = Worker(3)

sink = Sink()

i.output = h1
h1.output = [k11, k12]
k11.output = h2
k12.output = h2
h2.output = [k21, k22]
k21.output = k3
k22.output = k3
k3.output = sink

s.add(sink)
s.add(k3)
s.add(k22)
s.add(k21)
s.add(h2)
s.add(k12)
s.add(k11)
s.add(h1)
s.add(i)

while True:
    s.step(DT)

    print """ \n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n

                            .---[ %s ]---.                     .---[ %s ]---.
                            |            |                     |            |
    %s>---[ %04i / %04i ]---|            |---[ %04i / %04i ]---|            |---[ %s ]--->
                            |            |                     |            |
                            '---[ %s ]---'                     '---[ %s ]---'

             >> %04i >>       >> %04i >>        >> %04i >>       >> %04i >>   >> %04i >>


    IN: %04i    DROP: %04i    OUT: %04i
    P(service) = %.5f
    P(dropout) = %.5f

""" % (
        'XX' if k11.blocked else '**' if k11.req else '  ',
        'XX' if k21.blocked else '**' if k21.req else '  ',
        '* ' if i.generated else '  ',
        len(h1.buf), h1.capacity,
        len(h2.buf), h2.capacity,
        'XX' if k3.blocked else '**' if k3.req else '  ',
        'XX' if k12.blocked else '**' if k12.req else '  ',
        'XX' if k22.blocked else '**' if k22.req else '  ',
        h1.counter, (k11.counter + k12.counter), h2.counter, (k21.counter + k22.counter), k3.counter,
        s.c_total_in, s.c_total_drop, s.c_total_out,
        1.0 * s.c_total_out / (s.c_total_drop + s.c_total_out + 0.01),
        1.0 * s.c_total_drop / (s.c_total_drop + s.c_total_out + 0.01),
    )

    time.sleep(0.01)
