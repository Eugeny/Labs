DISTR_MTX = [
    [0.0,  0.1,  0.1],
    [0.15, 0.0,  0.1],
    [0.2,  0.15, 0.2],
]

NUM = 100000


import numpy as np
from sympy import *
import time
import math
import matplotlib
import matplotlib.path
import matplotlib.nxutils
matplotlib.use('GTK')
import matplotlib.pyplot as plt
import matplotlib.lines as lines
import matplotlib.transforms as mtransforms
import matplotlib.text as mtext
import random


def gen_rnd(FX):
    x = random.random()
    for i in range(0, len(FX)):
        if FX[i] > x:
            return i - 1
    return len(FX) - 1


SZ = len(DISTR_MTX)
PXi = [sum(DISTR_MTX[y][x] for y in range(0, SZ)) for x in range(0, SZ)]
PYi = [sum(DISTR_MTX[y][x] for x in range(0, SZ)) for y in range(0, SZ)]
FXi = [sum(PXi[i] for i in range(0, j)) for j in range(0, SZ)]
FYi = [sum(PYi[i] for i in range(0, j)) for j in range(0, SZ)]

RNDX = [gen_rnd(FXi) for i in range(0, NUM)]
RNDY = []
for i in range(0, NUM):
    PYiXi = [DISTR_MTX[j][RNDX[i]] / PXi[RNDX[i]] for j in range(0, SZ)]
    FYiXi = [sum(PYiXi[i] for i in range(0, j)) for j in range(0, SZ)]
    RNDY += [gen_rnd(FYiXi)]

NEW_DISTR_MTX = np.zeros((SZ, SZ))
for i in range(0, NUM):
    NEW_DISTR_MTX[RNDY[i]][RNDX[i]] += 1.0 / NUM


mxy = sum((x + y) * NEW_DISTR_MTX[y][x] for x in range(0, SZ) for y in range(0, SZ))
dxy = sum(((x + y) - mxy) ** 2 * NEW_DISTR_MTX[y][x] for x in range(0, SZ) for y in range(0, SZ))

print "M[x] =", mxy
print "D[x] =", dxy

plt.ion()
fig = plt.figure()

ax = fig.add_subplot(222)

ax.bar(np.arange(SZ), PXi, width=0.25, color='green')
ax.bar(np.arange(0.5, SZ + 0.5), PYi, width=0.25, color='blue')


def draw_dmtx(ax, mtx):
    xs = ys = np.arange(SZ + 1)
    fig.colorbar(ax.pcolormesh(xs, ys, np.array(mtx)))
    for x in range(0, SZ):
        for y in range(0, SZ):
            plt.text(x + 0.5, y + 0.5, '%.4f' % mtx[y][x],
                     horizontalalignment='center',
                     verticalalignment='center',
                     )

ax = fig.add_subplot(221)
draw_dmtx(ax, DISTR_MTX)

ax = fig.add_subplot(223)
draw_dmtx(ax, NEW_DISTR_MTX)


fig.show()
while True:
    fig.waitforbuttonpress()
