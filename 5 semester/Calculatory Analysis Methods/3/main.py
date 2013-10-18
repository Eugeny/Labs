import matplotlib.pyplot as plt
import math
import time

from sympy import *
from sympy.abc import x
from sympy.core import Symbol
from sympy.solvers import solve

xmin = -1.0
xmax = 1.0

a = math.sin(5)
b = math.cos(5)
#a=1;b=1


def mkbasis(steps):
    basis = [(x ** (i*2-2) * (1 - x ** 2)) for i in range(0, steps + 1)]
    #basis = [(x ** i * (1 - x ** 2)) for i in range(0, steps + 1)]
    basis[0] = 0
    return basis

def mkxs(steps):
    return [xmin + (xmax - xmin) / steps * i for i in range(0, steps + 1)]

def colocations(steps):
    basis = mkbasis(steps)
    xs = mkxs(steps)
    ax = [1] + [Symbol('a%i' % i) for i in range(1, steps + 1)]

    yapprox = sum(ax[i] * basis[i] for i in range(0, steps + 1))
    error = a * diff(diff(yapprox, 'x'), 'x') + (1 + b * x ** 2) * yapprox + 1

    matrix = [error.subs(x, xs[i]) for i in range(0, steps)]
    #matrix += [yapprox.subs(x, xmin)]
    #matrix += [yapprox.subs(x, xmax)]
    #print '---------------'
    #print 'xs', xs
    #print xmin, xmax, steps
    #print 'matrix', matrix
    sol = solve(matrix)
    #print 'sol', sol
    y = yapprox.subs(sol)
    return y


def mnk_int(steps):
    basis = mkbasis(steps)
    #xs = mkxs(steps)
    ax = [1] + [Symbol('a%i' % i) for i in range(1, steps + 1)]

    yapprox = sum(ax[i] * basis[i] for i in range(0, steps + 1))
    error = a * diff(diff(yapprox, 'x'), 'x') + (1 + b * x ** 2) * yapprox + 1

    #print '---------------'
    ediffs = [integrate(error * diff(error, ax[i]), (x, xmin, xmax)) for i in range(1, steps + 1)]
    #print ediffs
    sol = solve(ediffs)
    #print sol
    y = yapprox.subs(sol)
    return y


def mnk_dis(steps):
    basis = mkbasis(steps)
    xs = mkxs(steps)
    ax = [1] + [Symbol('a%i' % i) for i in range(1, steps + 1)]

    yapprox = sum(ax[i] * basis[i] for i in range(0, steps + 1))
    error = a * diff(diff(yapprox, 'x'), 'x') + (1 + b * x ** 2) * yapprox + 1

    #print '---------------'
    ediffs = [
        sum((error * diff(error, ax[i])).subs(x, xi) for xi in xs)
        for i in range(1, steps + 1)
    ]

    #print ediffs
    sol = solve(ediffs)
    #print sol
    y = yapprox.subs(sol)
    return y


def galerkin(steps):
    basis = mkbasis(steps)
    #xs = mkxs(steps)
    ax = [1] + [Symbol('a%i' % i) for i in range(1, steps + 1)]

    yapprox = sum(ax[i] * basis[i] for i in range(0, steps + 1))
    error = a * diff(diff(yapprox, 'x'), 'x') + (1 + b * x ** 2) * yapprox + 1

    ediffs = [integrate(error * basis[i], (x, xmin, xmax)) for i in range(1, steps + 1)]
    #print ediffs
    sol = solve(ediffs)
    #print sol
    y = yapprox.subs(sol)
    return y


def plot(fx, steps, ga, **kwargs):
    t0 = time.time()
    y = fx(steps)
    dt = time.time() - t0

    gsteps = 100
    xs = [xmin + (xmax - xmin) / gsteps * i for i in range(0, gsteps + 1)]
    ys = [y.subs(x, xi) for xi in xs]

    error = a * diff(diff(y, 'x'), 'x') + (1 + b * x ** 2) * y + 1
    #emax = max(abs(error.subs(x, xi)) for xi in xs)
    eavg = sum(abs(error.subs(x, xi)) for xi in xs) / len(xs)
    print '-- %12s @ %2i steps: err = %.10f, time = %.5fs' % (fx.__name__, steps, eavg, dt)
    ga.plot(xs, ys, **kwargs)


fig = plt.figure()
fig.show()

print
ga = fig.add_subplot(221)
ga.grid()

plot(colocations, 2, ga, color='red')
plot(colocations, 5, ga, color='green')

print
ga = fig.add_subplot(222)
ga.grid()

plot(mnk_int, 2, ga, color='red')
plot(mnk_int, 5, ga, color='green')

print
ga = fig.add_subplot(223)
ga.grid()

plot(mnk_dis, 2, ga, color='red')
plot(mnk_dis, 5, ga, color='green')


print
ga = fig.add_subplot(224)
ga.grid()

plot(galerkin, 2, ga, color='red')
plot(galerkin, 5, ga, color='green')


while True:
    fig.waitforbuttonpress()
