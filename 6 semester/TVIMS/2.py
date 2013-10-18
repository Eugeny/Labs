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
K = 10


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

ga = fig.add_subplot(131)
ga.grid()


def table(t, xs, ys):
    print '-----------------------------'
    print t
    print '-----------------------------'

    for i in range(0, len(xs)):
        print '%5.5s : %5.5s' % (xs[i], ys[i])


def measure_homogenity(xs, ax, ga, ax3, color, offset=0):
    L = min(xs)
    R = max(xs)

    step = lambda x: L + (R - L) / K * x

    ns = [
        sum(
            (x >= step(i)) and (x < step(i+1))
            for x in xs
        )
        for i in range(0, K)
    ]
    ps = [ni * 1.0 / len(xs) for ni in ns]

    xx = [step(i) for i in range(0, K)]
    ax.bar(xx, ps, width=(R-L)/K, color='red')

    ys = [ps[i] / ((R-L)/K) for i in range(K)]
    ax3.plot([xx[(i+1)/2] for i in range(K*2-1)], [ys[(i)/2] for i in range(K*2-1)] , color='orange')

    ax3.plot([x - (R-L)/K/2 for x in xx], ps, color='blue')

    def testFY(y):
        for i in range(K):
            if y >= step(i) and y < step(i+1):
                return sum(ps[_] for _ in range(i))
        return 0.5

    ys = [testFY(i) for i in xx]
    ga.plot([xx[i/2] for i in range(K*2-1)], [ys[(i+1)/2] for i in range(K*2-1)] , color='orange')



    fy_theoretic_xs = [YFROM + i * (YTO - YFROM) * 1.0 / RESOLUTION for i in range(0, RESOLUTION)]
    ps = [fY(x) for x in fy_theoretic_xs]
    ax3.plot(fy_theoretic_xs, ps, color='green')


fy_empiric_xs = [YFROM + i * (YTO - YFROM) * 1.0 / RESOLUTION for i in range(0, RESOLUTION)]
fy_empiric_ys = [Fy_empiric(y) for y in fy_empiric_xs]
ga.plot(fy_empiric_xs, fy_empiric_ys, color='red')

table('Empiric F(y)', fy_empiric_xs, fy_empiric_ys)

fy_theoretic_xs = [YFROM + i * (YTO - YFROM) * 1.0 / RESOLUTION for i in range(0, RESOLUTION)]
fy_theoretic_ys = [Fy_theoretic(y) for y in fy_theoretic_xs]
ga.plot(fy_theoretic_xs, fy_theoretic_ys, color='green')

table('Theoretic F(y)', fy_theoretic_xs, fy_theoretic_ys)


ax = fig.add_subplot(132)
ax3 = fig.add_subplot(133)
measure_homogenity(Yi, ax, ga, ax3, 'green')

fig.show()
while True:
    fig.waitforbuttonpress()
