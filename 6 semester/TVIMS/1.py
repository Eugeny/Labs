import math
import matplotlib
import matplotlib.pyplot as plt
import matplotlib.lines as lines
import matplotlib.transforms as mtransforms
import matplotlib.text as mtext
import sys
import random


FROM = -1
TO = 5
N = 20
Phi = lambda x: x*x*x

YFROM = Phi(FROM)
YTO = Phi(TO)

RESOLUTION = 100


Xi = []
Yi = []

for i in range(0, N):
    rnd = random.random()
    Xi.append(FROM + (TO - FROM) * rnd)

for x in Xi:
    Yi.append(Phi(x))

Xi = sorted(Xi)
Yi = sorted(Yi)


def Fy_empiric(y):
    c = 0
    for yi in Yi:
        if yi < y:
            c += 1
    return 1.0 * c / N

def Fy_theoretic(y):
    if y > 0:
        return (y ** (1.0/3) + 1) / 6
    else:
        return (-((-y) ** (1.0/3)) + 1) / 6


fig = plt.figure()

ga = fig.add_subplot(111)
ga.grid()


def table(t, xs, ys):
    print '-----------------------------'
    print t
    print '-----------------------------'

    for i in range(0, len(xs)):
        print '%5.5s : %5.5s' % (xs[i], ys[i])




fy_empiric_xs = [YFROM + i * (YTO - YFROM) * 1.0 / RESOLUTION for i in range(0, RESOLUTION)]
fy_empiric_ys = [Fy_empiric(y) for y in fy_empiric_xs]
ga.plot(fy_empiric_xs, fy_empiric_ys, color='red')

table('Empiric F(y)', fy_empiric_xs, fy_empiric_ys)

fy_theoretic_xs = [YFROM + i * (YTO - YFROM) * 1.0 / RESOLUTION for i in range(0, RESOLUTION)]
fy_theoretic_ys = [Fy_theoretic(y) for y in fy_theoretic_xs]
ga.plot(fy_theoretic_xs, fy_theoretic_ys, color='green')

table('Theoretic F(y)', fy_theoretic_xs, fy_theoretic_ys)


fig.show()
while True:
    fig.waitforbuttonpress()
