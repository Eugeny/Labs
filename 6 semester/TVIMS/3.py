import math
import numpy as np
import matplotlib
import matplotlib.pyplot as plt
import matplotlib.lines as lines
import matplotlib.transforms as mtransforms
import matplotlib.text as mtext
import sys
import random


FROM = -1
TO = 5
N = 20000
Phi = lambda x: x*x*x

YFROM = Phi(FROM)
YTO = Phi(TO)

RESOLUTION = 100
K = 5


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


def fY(y):
    if y > 0:
        return y ** (1.0/3-1) * (1.0/3) / 6
    else:
        return (-y) ** (1.0/3-1) * (1.0/3) / 6


fig = plt.figure()


def table(t, xs, ys):
    print '-----------------------------'
    print t
    print '-----------------------------'

    for i in range(0, len(xs)):
        print '%5.5s : %5.5s' % (xs[i], ys[i])


def measure_homogenity(xs, ax, ga, ax3, color, offset=0):
    xs = sorted(xs)

    XS = [xs[len(xs)/K * i] for i in range(K)]
    XS += [1]

    xx = XS[:-1]

    step = lambda i: XS[i]

    ns = [
        sum(
            (x >= step(i)) and (x < step(i+1))
            for x in xs
        )
        for i in range(0, K)
    ]
    widths = [XS[i+1]-XS[i] for i in range(K)]
    ps = [ns[i] * 1.0 / len(xs) for i in range(K)]

    barys = [ps[i] / widths[i] for i in range(K)]
    ga.bar(xx, barys, width=widths, color='red')

    ax.plot([XS[(i+1)/2] for i in range(K*2-1)], [barys[(i)/2] for i in range(K*2-1)], color='orange')

    def testFY(y):
        for i in range(K):
            if y >= step(i) and y < step(i+1):
                return sum(ps[_] for _ in range(i))
        return 1

    ys = [testFY(i) for i in xx]
    ax3.plot([XS[i/2] for i in range(K*2-1)], [ys[(i+1)/2] for i in range(K*2-1)], color='orange')

    fy_theoretic_xs = [YFROM + i * (YTO - YFROM) * 1.0 / RESOLUTION for i in range(0, RESOLUTION)]
    ps = [fY(x) for x in fy_theoretic_xs]
    ax.plot(fy_theoretic_xs, ps, color='green')



ga = fig.add_subplot(131)
ax = fig.add_subplot(132)
ax3 = fig.add_subplot(133)
measure_homogenity(Yi, ax, ga, ax3, 'green')

fig.show()
while True:
    fig.waitforbuttonpress()
