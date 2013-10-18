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


def get_values(n):
    Xi = []
    Yi = []

    for i in range(0, N):
        rnd = random.random()
        Xi.append(FROM + (TO - FROM) * rnd)

    for x in Xi:
        Yi.append(Phi(x))

    Xi = sorted(Xi)
    Yi = sorted(Yi)
    return Xi, Yi


def Fy_empiric(Yi, y):
    c = 0
    for yi in Yi:
        if yi < y:
            c += 1
    return 1.0 * c / N


def Fy_theoretic(Yi, y):
    if y > 0:
        return (y ** (1.0/3) + 1) / 6
    else:
        return (-((-y) ** (1.0/3)) + 1) / 6


def Fy_gauss(Yi, y):
    return math.exp(-(y - 0.5) ** 2 / (2 * 25)) / math.sqrt(2 * math.pi) / 5


def Fy_uniform(Yi, y):
    return y


fig = plt.figure()

ga = fig.add_subplot(111)
ga.grid()


def do_pirson(fx, s):
    N = 200
    Xi, Yi = get_values(N)
    steps = 10

    ksi2 = 0
    for i in range(steps):
        l = YFROM + (YTO - YFROM) / steps * i
        r = l + (YTO - YFROM) / steps
        ksi2 += ((Fy_empiric(Yi, r) - Fy_empiric(Yi, l)) - (fx(Yi, r) - fx(Yi, l))) ** 2 / (fx(Yi, r) - fx(Yi, l) + 0.0000001)

    print 'Ksi^2 (%10s) = %.2f' % (s, ksi2 * N)


def do_kolm(fx, s):
    N = 30
    Xi, Yi = get_values(N)

    dn = 0
    for y in Yi:
        y += 0.001
        dn = max(dn, abs(Fy_empiric(Yi, y) - fx(Yi, y)))

    dn *= math.sqrt(N)

    print 'Dn (%10s) =    %.2f' % (s, dn)


def do_mises(fx, s):
    N = 50
    Xi, Yi = get_values(N)

    nw = 0
    ylast = 0
    for y in Yi[1:]:
        y += 0.001
        nw += (Fy_empiric(Yi, y) - fx(Yi, y)) ** 2 * (y - ylast)
        ylast = y
    #nw *=
    print 'nw2 (%10s) =   %.2f' % (s, nw)


for method in [do_pirson, do_kolm, do_mises]:
    for fy, s in [(Fy_theoretic, 'correct'), (Fy_gauss, 'gauss'), (Fy_uniform, 'uniform')]:
        method(fy, s)
