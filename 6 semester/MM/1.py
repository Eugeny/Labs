import numpy as np
import time
import math
import matplotlib
matplotlib.use('GTK')
import matplotlib.pyplot as plt
import matplotlib.lines as lines
import matplotlib.transforms as mtransforms
import matplotlib.text as mtext
import sys
import random


K = 10


def RNG_mersenne():
    return random.random()


def RNG_sq_mid():
    RNG_sq_mid.seed **= 2
    RNG_sq_mid.seed = (RNG_sq_mid.seed / 100) % 10000 + 1
    return (RNG_sq_mid.seed % 1000) * 0.001

RNG_sq_mid.seed = int(random.random() * 9999)


def RNG_mc():
    K = 23
    M = 2 ** K
    RNG_mc.seed = (RNG_mc.seed * K) % M + 1
    return RNG_mc.seed % 1000 * 0.001

RNG_mc.seed = int(random.random() * 99999999)


RNG = RNG_mc


def measure_homogenity(rng, n, ax, color, offset=0):
    xs = [rng() for x in range(0, n)]
    ns = [
        sum(
            (x > 1.0 / K * i) and (x < 1.0 / K * (i+1))
            for x in xs
        )
        for i in range(0, K)
    ]
    ps = [ni * 1.0 / n for ni in ns]
    ax.bar([x + offset for x in np.arange(10)], ps, width=0.20, color=color)


def get_mx(xs):
    return sum(xs) / len(xs)


def get_dx(xs):
    m = get_mx(xs)
    return get_mx([abs(x - m) ** 2 for x in xs])


def measure_coherency(rng, n, di):
    xs = [rng() for x in range(0, n)]
    ys = [xs[(i + di) % n] for i in range(0, n)]
    r = (get_mx([xs[i] * ys[i] for i in range(0, n)]) - get_mx(xs) * get_mx(ys)) / math.sqrt(get_dx(xs) * get_dx(ys))
    return r


plt.ion()
fig = plt.figure()

ax = fig.add_subplot(121)
ax.grid()

measure_homogenity(RNG, 100, ax, '#ff0000')
measure_homogenity(RNG, 1000, ax, '#bb4400', offset=0.2)
measure_homogenity(RNG, 10000, ax, '#888800', offset=0.4)
measure_homogenity(RNG, 100000, ax, '#44bb00', offset=0.6)


ax = fig.add_subplot(122)
ax.grid()

xs = np.arange(4)
ys = [measure_coherency(RNG, 100 * (6 ** n), 1) for n in xs]
ax.plot(xs, ys, color="red")
ys = [measure_coherency(RNG, 100 * (6 ** n), 10) for n in xs]
ax.plot(xs, ys, color="orange")
ys = [measure_coherency(RNG, 100 * (6 ** n), 50) for n in xs]
ax.plot(xs, ys, color="green")


fig.show()
while True:
    fig.waitforbuttonpress()
