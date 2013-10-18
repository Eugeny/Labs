import numpy as np
import time
import math
import matplotlib
matplotlib.use('GTK')
import matplotlib.pyplot as plt
import matplotlib.lines as lines
import matplotlib.transforms as mtransforms
import matplotlib.text as mtext
from scipy.special import erf, erfinv
import sys
import random


N = 2000
K = 20

D = 1
M = 0

fX = lambda x: math.exp(-(x - M) ** 2 / (2 * D * D)) / (D * math.sqrt(math.pi * 2))
FX = lambda x: (1 + erf((x - M) / math.sqrt(2) / D)) / 2
IFX = lambda y: -erfinv(y * 2 - 1) * math.sqrt(2) * D + M

#print list(IFX(FX(float(x))) for x in np.arange(0, 1, 0.1))
#while True:
#    pass


def measure_homogenity(xs, ax, color, offset=0):
    L = min(xs)
    R = max(xs)

    step = lambda x : L + (R - L) / K * x

    ns = [
        sum(
            (x > step(i)) and (x < step(i+1))
            for x in xs
        )
        for i in range(0, K)
    ]
    ps = [ni * 1.0 / len(xs) for ni in ns]

    xx = [step(i) for i in range(0, K)]
    ax.bar(xx, ps, width=(R-L)/K, color='red')

    ps = [fX(x) * (R-L) / K for x in xx]
    ax.plot(xx, ps, color='green')


def measure_Fx(xs):
    L = min(xs)
    R = max(xs)

    step = lambda x : L + (R - L) / K * x

    fs = [
        sum(
            x < step(i)
            for x in xs
        )
        for i in range(0, K)
    ]
    fs = [f * 1.0 / len(xs) for f in fs]
    xx = [step(i) for i in range(0, K)]

    ax.plot(xx, fs, color='red')
    
    fs = [FX(x) for x in xx]
    ax.plot(xx, fs, color='green')


def get_mx(xs):
    return sum(xs) / len(xs)


def get_dx(xs):
    m = get_mx(xs)
    return get_mx([abs(x - m) ** 2 for x in xs])



plt.ion()
fig = plt.figure()

ax = fig.add_subplot(121)
ax.grid()

xs = [random.random() for x in range(0, N)]
ys = [IFX(x) for x in xs]

measure_homogenity(ys, ax, '#ff0000')

ax = fig.add_subplot(122)
ax.grid()
measure_Fx(ys)

print 'M[x] = %.3f' % get_mx(ys)
print 'd[x] = %.3f' % get_dx(ys)

fig.show()
while True:
    fig.waitforbuttonpress()
