import matplotlib.pyplot as plt
import math

from sympy import diff
from sympy.abc import x
from sympy.core import Symbol
from sympy.solvers import solve

xmin = -1.0
xmax = 1.0

a = math.sin(5)
b = math.cos(5)

ya = 0
yb = 0


def approximation(steps):
    basis = [(x ** i * (1 - x ** 2)) for i in range(0, steps + 1)]
    basis[0] = 0

    h = (xmax - xmin) / steps
    xs = [xmin + h * i for i in range(0, steps + 1)]
    ys = [Symbol('y%i' % i) for i in range(0, steps + 1)]

    matrix = [
        (ys[i+1] - 2 * ys[i] + ys[i-1]) / (h ** 2) - (-1 - (1 + b*xs[i]**2) * ys[i]) / a
        for i in range(1, steps)
    ]

    matrix += [ys[0] - ya]
    matrix += [ys[steps] - yb]

    print ''
    for k in matrix:
        print k
        
    values = solve(matrix)
    return [values[k] for k in ys]


def plot_approximation(steps, ga, **kwargs):
    ys = approximation(steps)
    xs = [xmin + (xmax - xmin) / steps * i for i in range(0, steps + 1)]
    ga.plot(xs, ys, **kwargs)


fig = plt.figure()

ga = fig.add_subplot(111)
ga.grid()


plot_approximation(3, ga, color='red')
plot_approximation(10, ga, color='green')
plot_approximation(50, ga, color='blue')

#ga.plot(*plot_euler(step_rounge_coutte), color='green')


fig.show()
while True:
    fig.waitforbuttonpress()
