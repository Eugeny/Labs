import numpy as np
from sympy import *
from sympy.solvers import solve
import math
import matplotlib
import matplotlib.path
import matplotlib.nxutils
matplotlib.use('GTK')
import matplotlib.pyplot as plt

SX = Symbol('x')
abs2 = Piecewise([-SX, SX <= 0], [SX, SX > 0])


G = SX
F = 5

U0 = 0
U1 = 0


L = 0
R = 1.0
STEPS = 50
H = (R-L) / STEPS

Xs = list(np.arange(L, R+H, H))
Ks = range(0, STEPS+1)

Vk = lambda x, k: 1.0 - abs2.subs(SX, Xs[k] - x) / H

Vs = [Vk(SX, k) for k in Ks]


plt.ion()
fig = plt.figure()
ax = fig.add_subplot(111)
ax.grid()


Us = [Symbol('U_%i' % i) for i in Ks]
dscheme = [
    Us[0] - U0,
    Us[-1] - U1,
]

for k in Ks[1:-1]:
    xs = [Xs[k-1], Xs[k], Xs[k+1]]
    ax.plot(xs, [Vs[k].subs(SX, _) for _ in xs], color='blue')

    dscheme.append(
        -Us[k-1] * integrate(G / (H * H), (SX, Xs[k-1], Xs[k]))
        + Us[k] * integrate(G / (H * H), (SX, Xs[k-1], Xs[k+1]))
        - Us[k+1] * integrate(G / (H * H), (SX, Xs[k], Xs[k+1]))
        + integrate(F * Vs[k], (SX, Xs[k-1], Xs[k+1]))
    )

solution = solve(dscheme)
ys = [solution[Us[k]] for k in Ks]
print Xs, ys
ax.plot(Xs, ys, color='red')

fig.show()
while fig.waitforbuttonpress():
    pass
