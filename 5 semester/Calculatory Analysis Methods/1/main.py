import math
import matplotlib
import matplotlib.pyplot as plt
import matplotlib.lines as lines
import matplotlib.transforms as mtransforms
import matplotlib.text as mtext
import sympy
import sys


a = 0.5
m = 1.0

x0 = 0
y0 = 0

e = 0.001
h0 = 0.5

minx = 0
maxx = 1.0


if len(sys.argv) > 1:
	e = float(sys.argv[1])
if len(sys.argv) > 2:
	a = int(sys.argv[2])
if len(sys.argv) > 3:
	m = int(sys.argv[3])

# Function
df = lambda x,y : (a*(1-y*y)) / ((1+m)*x*x + y*y + 1)



def step_euler_normal(x,y):
	return y + h * df(x,y)

def step_euler_ext(x,y):
	return y + h * df(x+h/2,y + h/2 * df(x, y))

def step_rounge_coutte(x,y):
	k1 = h * df(x,y)
	k2 = h * df(x + h/2, y + k1/2)
	k3 = h * df(x + h/2, y + k2/2)
	k4 = h * df(x + h, y + k3)
	return y + (k1 + 2*k2 + 2*k3 + k4) / 6

def plot_euler(stepfx):
	x = x0
	y = y0
	xs = [x] if x >= minx else []
	ys = [y] if x >= minx else []
	for i in range(0, int(maxx/h)):
		y = stepfx(x, y)
		x += h
		if x >= minx:
			xs += [x]
			ys += [y]
	return xs, ys


def converge_epsilon(stepfx):
	global h
	h = h0 * 2
	xsl, ysl = plot_euler(stepfx)
	steps = 0
	while True:
		steps += 1
		h /= 2
		ysk = ysl
		xsk = xsl

		xs,ys = plot_euler(stepfx)
		xsl, ysl = xs,ys

		maxd = 0
		for i in range(0,len(xsk)):
			for j in range(0,len(xsl)):
				if abs(xsk[i] - xsl[j]) < e:
					maxd = max(maxd, abs(ysk[i] - ysl[j]))
		#print maxd
		if maxd < e:
			break


	print '%s\th = %f\tsteps = %i' % (stepfx.__name__[5:], h, steps)
	#print '\n'.join('f(%f) = %f' % (xs[i], ys[i]) for i in range(0,len(xs)))
	return xs, ys




print ''.join(['\n']*40)

e /= 2

fig = plt.figure()

ga = fig.add_subplot(111)
ga.grid()

# Euler normal
ga.plot(*converge_epsilon(step_euler_normal), color='red')

# Euler extended
ga.plot(*converge_epsilon(step_euler_ext), color='blue')

# R-K
ga.plot(*converge_epsilon(step_rounge_coutte), color='orange')
#h = 0.5 
#ga.plot(*plot_euler(step_rounge_coutte), color='green')

#h = 0.25 
#ga.plot(*plot_euler(step_rounge_coutte), color='green')

# Euler fixed
h = 0.25
#ga.plot(*plot_euler(step_euler_normal), color='green')

h = 0.01
ga.plot(*plot_euler(step_rounge_coutte), color='green')


fig.show()
while True:
	fig.waitforbuttonpress()
