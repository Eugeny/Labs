from sympy import *
from sympy.core.mod import *
import time

EPS = 0.000001

a = -14.4621
b = 60.6959
c = -70.9238

x = Symbol('x')
fx = x**3 + a*x**2 + b*x + c

L = -10.0
R = 160.0

print "F(x):", 	fx
#fx = x

_sturm_cache = {}
def sturm_seq(f):
	if f in _sturm_cache:
		return _sturm_cache[f]

	r = []
	r += [f, diff(f)]
	i = 1

	while not r[i].is_Number:
		_, n = div(r[i-1], r[i])
		r += [-n]
		i += 1
	
	_sturm_cache[f] = r
	return r


def count_sign_changes(ss, x):
	l = None
	c = 0

	for s in ss:
		su = s.subs('x', x)
		if l is not None:
			if abs(abs(l)/l - abs(su)/su) > EPS:
				c += 1
		l = su

	return c


def count_roots_between(fx, l, r):
	ss = sturm_seq(fx)
	return count_sign_changes(ss, l) - count_sign_changes(ss, r)


def isolate_next_root(fx, l, r):
	ll = l
	while count_roots_between(fx, ll, r) > 1:
		if count_roots_between(fx, l, (l+r)/2) > 1:
			r = (l+r)/2
		elif count_roots_between(fx, l, (l+r)/2) == 0:
			l = (l+r)/2
		else:
			return (l+r)/2
	return r


def compactize_interval(fx, i, d):
	l,r = i
	while r - l > d:
		if count_roots_between(fx, l, (l+r)/2) == 1:
			r = (l+r)/2
		else:
			l = (l+r)/2
	return [l,r]

def split_roots(fx, l, r):
	res = []
	i = l
	while i < r:
		l = i
		i = isolate_next_root(fx, i, r)
		interval = [l,i]
		interval = compactize_interval(fx, interval, 0.1)
		res += [interval]
	return res


def get_arc_start(fx, l, r):
	x = r
	if fx.subs('x', x) * diff(diff(fx)).subs('x', x) > 0:
		return 'r'
	x = l
	if fx.subs('x', x) * diff(diff(fx)).subs('x', x) > 0:
		return 'l'


def solve_horde(fx, l, r):
	x0 = eval(get_arc_start(fx, l, r))
	x = r if (x0 == l) else l
	xl = 999
	itc = 0
	while abs(xl-x) > EPS:
		itc += 1
		xl = x
		x = x - fx.subs('x', x) * (x0 - x) / (fx.subs('x', x0) - fx.subs('x', x))
	return x, itc


def solve_newton(fx, l, r):
	df = diff(fx)
	x = eval(get_arc_start(fx, l, r))
	xl = 999
	itc = 0
	while abs(xl-x) > EPS:
		itc += 1
		xl = x
		x = x - fx.subs('x', x) / df.subs('x', x)
	return x, itc

print 'Exact solution:', solve(fx)
intervals = split_roots(fx, L, R)
print 'Isolated roots:', intervals

print '--- Hordes'
idx = 1
for i in intervals:
	r, i = solve_horde(fx, i[0], i[1])
	print 'Root %i: %.4f (%i iterations)' % (idx, r, i)
	idx += 1


print '--- Newton'
idx = 1
for i in intervals:
	r, i = solve_newton(fx, i[0], i[1])
	print 'Root %i: %.4f (%i iterations)' % (idx, r, i)
	idx += 1
