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

LEVELS = [0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 0.95, 0.99]
Z_LAPLAS = {
    0.1: 0.14,
    0.2: 0.26,
    0.3: 0.4,
    0.4: 0.52,
    0.5: 0.68,
    0.6: 0.86,
    0.7: 1.04,
    0.8: 1.32,
    0.9: 1.68,
    0.95: 1.98,
    0.99: 2.55,
}
KSI = {
    0.9: {19: 11},
    0.85: {19: 11.5},
    0.8: {19: 12.5, 9: 5, 29: 19},
    0.75: {19: 14.3},
    0.7: {19: 14.5},
    0.65: {19: 15},
    0.6: {19: 16},
    0.55: {19: 18},
    0.45: {19: 18.5},
    0.4: {19: 19},
    0.35: {19: 19.5},
    0.3: {19: 20.5},
    0.25: {19: 22.7},
    0.2: {19: 23, 9: 10, 29: 42},
    0.15: {19: 24},
    0.1: {19: 27},
}

def ksi(a, n):
    if a < 0.5:
        d = -2.0637 * (math.log(1/a) - 0.16) ** (0.4274) + 1.5774
    else:
        d = 2.0637 * (math.log(1/(1-a)) - 0.16) ** (0.4274) - 1.5774
    A = d * math.sqrt(2)
    B = 2 / 3 * (d * d - 1)
    C = d * (d*d - 7) / (9 * math.sqrt(2))
    D = (6 * (d ** 4) + 14 * d * d - 32) / (405)
    E = d * (9 * (d**4) + 256 * d * d  - 433) / (4860 * math.sqrt(2))
    sqn = math.sqrt(n)
    return n + A * sqn + B + C / sqn + D / n + E / (n*sqn)

def get_values(n):
    Xi = []
    Yi = []

    for i in range(0, n):
        rnd = random.random()
        Xi.append(FROM + (TO - FROM) * rnd)

    for x in Xi:
        Yi.append(Phi(x))

    Yi = sorted(Yi)
    return Yi


def get_mx(xs):
    return sum(xs) / len(xs)


def get_dx(xs):
    m = get_mx(xs)
    return get_mx([abs(x - m) ** 2 for x in xs])


def Fy_theoretic(y):
    if y > 0:
        return (y ** (1.0/3) + 1) / 6
    else:
        return (-((-y) ** (1.0/3)) + 1) / 6


def evaluate_mx(Xs):
    return get_mx(Xs)

def evaluate_dx(Xs):
    return get_dx(Xs)

def get_interval_mx(lvl, Xs, dx):
    return 2 * math.sqrt(dx) * Z_LAPLAS[lvl] / math.sqrt(len(Xs))

def get_interval_dx(lvl, Xs, mx):
    print (1+lvl)/2, (1-lvl)/2, len(Xs) - 1
    #if ((1+lvl)/2 not in KSI) or ((1-lvl)/2 not in KSI):
    #        return 0
    return abs(\
        (mx ** 2) * (len(Xs) - 1) / ksi((1+lvl)/2, len(Xs) - 1) \
        - (mx ** 2) * (len(Xs) - 1) / ksi((1-lvl)/2, len(Xs) - 1))


def plot_vs(ax, xs, fx, color, **kwargs):
    ys = [fx(x, **kwargs) for x in xs]
    ax.plot(xs, ys, color=color)


fig = plt.figure()

true_mx = evaluate_mx(get_values(100000))
true_dx = evaluate_dx(get_values(100000))

#-------------
ax = fig.add_subplot(221)
Xs = get_values(20)

ys = [get_interval_mx(l, Xs, evaluate_dx(Xs)) for l in LEVELS]
ax.plot(LEVELS, ys, color='red')

ys = [get_interval_mx(l, Xs, true_dx) for l in LEVELS]
ax.plot(LEVELS, ys, color='green')


#-------------
ax = fig.add_subplot(222)
ys = []
for i in range(1, 100):
    n = i * 10
    Xs = get_values(n)
    ys.append(get_interval_mx(LEVELS[5], Xs, true_dx))
ax.plot(range(10, 1000, 10), ys, color='green')


#-------------
ax = fig.add_subplot(223)
Xs = get_values(20)

ys = [get_interval_dx(l, Xs, evaluate_mx(Xs)) for l in LEVELS[:5]]
ax.plot(LEVELS[:5], ys, color='red')

ys = [get_interval_dx(l, Xs, true_mx) for l in LEVELS[:5]]
ax.plot(LEVELS[:5], ys, color='green')


#-------------
ax = fig.add_subplot(224)
ys = []
xs = []
for i in range(1, 100):
    n = i * 10
    Xs = get_values(n)
    xs.append(n)
    ys.append(get_interval_dx(LEVELS[5], Xs, true_mx))
ax.plot(xs, ys, color='green')



fig.show()
while True:
    fig.waitforbuttonpress()