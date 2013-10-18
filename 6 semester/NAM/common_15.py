import numpy as np
from sympy import *
from sympy.solvers import solve
import math
import matplotlib
import matplotlib.path
import matplotlib.nxutils
matplotlib.use('GTK')
import matplotlib.pyplot as plt

import mpl_toolkits.mplot3d


USE3D = True  # False

A = 1
B = 1
MU = 1

FX = lambda x, y, t: sin(x) * sin(y) * (MU * cos(MU * t) + (A + B) * sin(MU * t))
BORDERX0 = lambda x, y, t: 0
BORDERX1 = lambda x, y, t: -sin(y) * sin(MU * t)
BORDERY0 = lambda x, y, t: 0
BORDERY1 = lambda x, y, t: -sin(x) * sin(MU * t)
INITIAL = lambda x, y, t: 0
IDEAL = lambda x, y, t: sin(x) * sin(y) * sin(MU * t)

VMIN = -1
VMAX = 1
DT = 0.2


plt.ion()
fig = plt.figure()

if USE3D:
    ax = fig.add_subplot(111, projection='3d')
else:
    ax = fig.add_subplot(111)
ax.grid()


size = 3.14
res = 10
step = size * 1.0 / res
xs = np.arange(0, size, step)
ys = np.arange(0, size, step)


def check_err(matrix, t):
    e = 0
    for x in range(1, res-1):
        for y in range(1, res-1):
            e += abs(IDEAL(xs[x], ys[y], t) - matrix[x][y]) / abs(IDEAL(xs[x], ys[y], t))

    e /= res * res
    print 'Err = %.5f' % (e / 10)
