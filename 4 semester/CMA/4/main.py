from copy import deepcopy
import sys
import math


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

def minv(m):
	try:
		from numpy import asarray
		from numpy import matrix
		return asarray(matrix(m).I)
	except:
		print '\nNo solutions'
		sys.exit(1)

def mtr(m):
	try:
		from numpy import asarray
		from numpy import matrix
		return asarray(matrix(m).T)
	except:
		print '\nNo solutions'
		sys.exit(1)

def mmmul(m, n):
	from numpy import asarray
	from numpy import matrix
	return asarray(matrix(m) * matrix(n))


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



def normm1(m):
	return max(sum(m[i]) for i in range(0,len(A)))

def normm2(m):
	return sum(m[i][j]*m[i][j] for i in range(0,len(A)) for j in range(0,len(A)))

def normm3(m):
	return max(sum(m[j][i] for j in range(0,len(A))) for i in range(0,len(A)))


def normv1(v):
	return math.sqrt(sum(x * x for x in v))

def normv2(v):
	return max(abs(x) for x in v)

def normv3(v):
	return sum(abs(x) for x in v)


C = [
	[3.4,0,0,0,0,0,0,0],
	[0,1.4,0,0,0,0,0,0],
	[0,0,2.4,0,0,0,0,0],
	[0,0,0,1.4,6,7,2,1],
	[0,0,0,6,3.4,8,4,5],
	[0,0,0,7,8,2.4,0,0],
	[0,0,0,2,4,0,1.4,0],
	[0,0,0,1,5,0,0,3.4]
]

D = [
	[3.4,8,5,4,2,1,6,-7],
	[8,1.4,4,3,8,5,2,7],
	[5,4,2.4,4,-5,1,3,-8],
	[4,3,4,1.4,6,7,2,1],
	[2,8,-5,6,3.4,8,4,5],
	[1,5,1,7,8,2.4,3,6],
	[6,2,3,2,4,3,1.4,7],
	[-7,7,-8,1,5,6,7,3.4]
]

F = [
	22,38,6,28,31,33,28,14
]
#B = [4,8,12]


try:
	ss = open('data.txt').read().split('\n')
	A = [[float(x) for x in s.split()] for s in ss]
	print 'Loaded from data.txt'
except:
	A = msum(C, D)
	print 'No file found'

#B=[5,6]
#A=[[1,2],[3,4]]

if len(A) > len(A[0]):
	ex = len(A) - len(A[0])
	for i in range(0,len(A)):
		A[0] += [0] * ex

if len(A) < len(A[0]):
	ex = len(A[0]) - len(A)
	A += ([[0] * len(A[0])]) * ex
	B += [0] * len(A[0])

_A = deepcopy(A) #backup

print '[A]'
mprint(A)


for i in range(0,len(A)):
	for j in range(0,len(A)):
		if A[i][j] != A[j][i]:
			print 'Matrix is not symmetric!'
			sys.exit(1)


print 'Solving'

count = 0
E = [[0] * len(A) for i in range(0, len(A))]
for i in range(0,len(A)):
	E[i][i] = 1
V = deepcopy(E)


while True:
	print 'A'
	mprint(A)

	count += 1
	mi = 0
	mj = 1
	for i in range(0, len(A)):
		for j in range(i+1, len(A)):
			if abs(A[i][j]) > abs(A[mi][mj]):
				mi = i
				mj = j

	p = -2 * A[mi][mj] / (A[mi][mi] - A[mj][mj])
	cosf = math.sqrt(0.5 * (1 + 1/math.sqrt(1 + p*p)))
	sinf = math.sqrt(0.5 * (1 - 1/math.sqrt(1 + p*p)))
	sinf *= abs(p) / p

	_V = deepcopy(E)
	_V[mi][mi] = _V[mj][mj] = cosf
	_V[mi][mj] = -sinf
	_V[mj][mi] = sinf

	print 'V'
	mprint (_V)

	V = mmmul(V,_V)


	B = deepcopy(A)
	B[mi][mi] = cosf*cosf * A[mi][mi] - 2 * sinf*cosf * A[mi][mj] + sinf*sinf*A[mj][mj]
	B[mj][mj] = sinf*sinf * A[mi][mi] + 2 * cosf*cosf * A[mi][mj] + cosf*cosf*A[mj][mj]
	B[mi][mj] = B[mj][mi] = (cosf*cosf-sinf*sinf) * A[mi][mj] + sinf*cosf*(A[mi][mi] - A[mj][mj])

	for k in range(0,len(A)):
		if k not in [mi,mj]:
			B[mi][k] = B[k][mi] = cosf*A[mi][k] - sinf*A[mj][k]
			B[mj][k] = B[k][mj] = sinf*A[mi][k] + cosf*A[mj][k]

	A = B

	t = 0
	for i in range(0,len(A)):
		for j in range(0,len(A)):
			if i != j:
				t += A[i][j] * A[i][j]

	if t < 0.00001 or t > 10000: break

mprint(A)

print '%i iterations'%count
print 'L =', ', '.join('%.4f'%A[i][i] for i in range(0,len(A)))

for i in range(0, len(A)):
	print 'V%i ='%i, ', '.join('%.4f'%V[j][i] for j in range(0,len(A)))

