import math
import numpy as np
import matplotlib.pyplot as plt
import matplotlib.lines as lines
import matplotlib.transforms as mtransforms
import matplotlib.text as mtext


def fx(x):
	return math.sin(x)/x# (x*2)**2-3*x

L = 1
R = 2

S = 0.05

fig = plt.figure()


# Trapezium ---------------------
r = 0

for i in range(0, int((R-L)/S)-1):
	r += (fx(L+S*i) + fx(L+S*i+S)) / 2 * S

ga = fig.add_subplot(221)
xs = np.arange(L, R, S/3)
ga.plot(xs, [fx(x) for x in xs])

for i in range(0, int((R-L)/S)-1):
	x1 = L+S*i
	x2 = x1 + S
	y = (fx(x1) + fx(x2)) / 2

	l = lines.Line2D([x1,x1,x2,x2],[0,y,y,0])
	ga.add_line(l)

ga.grid()
fig.text(0.1,0.2,'Trapezium integral = %.7f'%r)


# Parabolic -------------------------

r = 0

ga = fig.add_subplot(222)
xs = np.arange(L, R, S/3)
#ga.plot(xs, [fx(x) for x in xs])

for i in range(0, int((R-L)/S)-1):
	x1 = L+S*i
	x3 = x1 + S
	x2 = (x1+x2)/2
	y1 = fx(x1)
	y2 = fx(x2)
	y3 = fx(x3)

	#a*x1**2+b*x1+c=y1
	#a*x2**2+b*x2+c=y2
	#a*x3**2+b*x3+c=y3

	#y1-a*x1**2-b*x1 = y2 - a*x2**2 - b*x2
	#y1-y2 = a*(x1**2-x2**2)+b*(x1 - x2)
	#a = (y1-y2 - b*(x1 - x2)) / (x1**2-x2**2)
	#c = y3-a*x3**2 - b*x3 = y2-a*x2**2-b*x2
	#y3-y2-((y1-y2 - b*(x1 - x2)) / (x1**2-x2**2)) * (x3**2 + x2**2) - b*(x3-x2) = 0

	b = -(y3-y2-(y1-y2) / (x1**2-x2**2) * (x3**2 + x2**2)) / ((x1 - x2) / (x1**2-x2**2) * (x3**2 + x2**2)-(x3-x2))
	a = (y1-y2 - b*(x1 - x2)) / (x1**2-x2**2)
	c = y1-a*(x3**2) - b*x1

	pxs = np.arange(x1,x3,S/15)

	r += a*(x3**3)/3 + b*x3**2/2 + c*x3 - a*(x1**3)/3 - b*x1**2/2 - c*x1

	l = lines.Line2D([x1,x1],[0,y1])
	ga.add_line(l)
	l = lines.Line2D([x3,x3],[y3,0])
	ga.add_line(l)

	ga.plot(pxs, [a*(x**2)+b*x+c for x in pxs])


ga.grid()
fig.text(0.1,0.3,'Parabolic integral = %.7f'%r)


fig.show()
fig.waitforbuttonpress()

