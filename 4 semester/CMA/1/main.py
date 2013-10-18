from copy import deepcopy
import sys

def mprint(m):
	for l in m:
		for e in l:
			print '%.4f\t'%e,
		print
	print

def mmul(m, a):
	return [[
			e * a
			for e in l
		] for l in m
	]

def msum(m, n):
	r = deepcopy(m)
	for l in range(0,len(m)):
		for e in range(0,len(m[l])):
			r[l][e] = m[l][e] + n[l][e]
	return r

def mcombine(m, b):
	r = deepcopy(m)
	for l in range(0,len(m)):
		r[l].append(b[l])
	return r

def lmul(l, a):
	return [
		e * a
		for e in l
	]

def lsum(l, ll):
	r = deepcopy(l)
	for i in range(0,len(l)):
		r[i] += ll[i]
	return r

def lswap(m, i, j):
	t = m[i]
	m[i] = m[j]
	m[j] = t

def findmax(l):
	m = 0
	for i in range(1, len(l)):
		if abs(l[i]) > abs(l[m]):
			m = i
	return m



D = [
	[2.33, 0.81, 0.67, 0.92, -0.53],
	[-0.53, 2.33, 0.81, 0.67, 0.92],
	[0.92, -0.53, 2.33, 0.81, 0.67],
	[0.67, 0.92, -0.53, 2.33, 0.81],
	[0.81, 0.67, 0.92, -0.53, 2.33],
]

C = [
	[0.2, 0, 0.2, 0, 0],
	[0, 0.2, 0, 0.2, 0],
	[0.2, 0, 0.2, 0, 0.2],
	[0, 0.2, 0, 0.2, 0],
	[0, 0, 0.2, 0, 0.2],
]

B = [
	4.2, 4.2, 4.2, 4.2, 4.2
]


K = 1

try:
	ss = open('data.txt').read().split('\n')
	A = [[float(x) for x in s.split()] for s in ss]
	print 'Loaded from data.txt'
except:
	A = msum(mmul(C, K), D)
	print 'No file found'

A = mcombine(A, B)
_A = deepcopy(A) #backup

print '[A B]:'
mprint(A)

print 'Forward pass',
for xi in range(0, len(A) - 1):
	print '.',
	idxs = [findmax(A[i][0:-2]) for i in range(xi, len(A))]
	vals = [A[i][idxs[i-xi]] for i in range(xi, len(A))]
	mi = findmax(vals) + xi
	mj = idxs[mi - xi]

	lswap(A, xi, mi)
	for l in range(xi+1, len(A)):
		try:
			A[l] = lsum(A[l], lmul(A[xi], -A[l][xi]/A[xi][xi]))
		except:
			if A[l][-1] != 0:
				print 'No solutions'
				sys.exit(0)

print 
print 
mprint(A)

X = [0] * len(A)
T = [1] * len(A)
inf = False

print 'Reverse pass',
for i in range(len(A)-1, -1, -1):
	print '.',
	try:
		X[i] = A[i][-1] / A[i][i]
		for j in range(0, i):
			A[j][-1] -= A[j][i] * X[i]
	except:
		if A[i][-1] != 0:
			print 'No solutions'
			sys.exit(0)
		X[i] = 0
		s = 0
		for j in range(0,len(A)):
			if j != i:
				s += A[0][j]
		for j in range(0,len(A)):
			if j != i:
				T[j] = s / A[0][j]
		T[i] = -1
		inf = True
		print '(Infinite solutions found!)'

if inf:
	for j in range(0,len(A)):
		X[j] = '%.4f'%X[j] + ' + t * %.4f'%T[j]

print 
print
print 'Result:\n', '\n'.join('X%i = %s'%(i+1, (X[i] if isinstance(X[i],str) else '%.4f'%X[i])) for i in range(0,len(A)))

print '\nVerification pass'
A = _A
for i in range(0, len(A)):
	s = 0
	for j in range(0, len(A)):
		try:
			s += A[i][j] * X[j]
		except:
			pass
	print 'B%i = %.4f'%(i+1,s)