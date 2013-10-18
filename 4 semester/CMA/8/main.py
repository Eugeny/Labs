import math
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.lines as lines
import matplotlib.transforms as mtransforms
import matplotlib.text as mtext


def fx(x,y):
	return -y

def y0(x):
	return 1


fig = plt.figure()


ga = fig.add_subplot(111)

R = 1
S = 0.1

x = 0
y = y0(x)

xs = []
ys = []

while x < R:
	xs += [x]
	ys += [y]
	y = y + S*fx(x,y)
	x += S

ga.plot(xs, ys)

ga.grid()


fig.show()
fig.waitforbuttonpress()

