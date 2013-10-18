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
	[0.01, 0, -0.02, 0, 0],
	[0.01, 0.01, 0, -0.02, 0],
	[0, 0.01, 0.01, 0, -0.02],
	[0, 0, 0.01, 0.01, 0],
	[0, 0, 0, 0.01, 0.01],
]

D = [
	[-1.33, 0.21, 0.17, 0.12, -0.13],
	[-0.13, -1.33, 0.21, 0.17, 0.12],
	[0.12, -0.13, -1.33, 0.21, 0.17],
	[0.17, 0.12, -0.13, -1.33, 0.21],
	[0.21, 0.17, 0.12, -0.13, -1.33],
]

B = [
	1.2, 2.2, 4.0, 0.0, -1.2
]
#B = [4,8,12]

K = 10

try:
	mode = sys.argv[1]
except:
	mode = 'jacobi'

try:
	nmode = sys.argv[2]
except:
	nmode = '1'


print 'Using mode:', mode

try:
	ss = open('data.txt').read().split('\n')
	A = [[float(x) for x in s.split()] for s in ss]
	print 'Loaded from data.txt'
except:
	A = msum(mmul(C, K), D)
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

A = mcombine(A, B)
_A = deepcopy(A) #backup

print '[A B]:'
mprint(A)


print 'Checking input'
D = [[0 if i!=j else A[i][j] for j in range(0,len(A))] for i in range(0,len(A))]
L = [[0 if i<=j else A[i][j] for j in range(0,len(A))] for i in range(0,len(A))]
U = [[0 if i>=j else A[i][j] for j in range(0,len(A))] for i in range(0,len(A))]

if mode == 'seidel':
	nmode = '2'
	N = mmmul(mmul(minv(msum(L,D)), -1), U)
	AN = eval('normm%s(N)'%nmode)
	#AN = max([max(N[i]) for i in range(0,len(N))])

	if AN > 1:
		print 'Solution will not converge!'
		sys.exit(0)
else:
	BB = [[-A[i][j]/A[i][i] if i != j else 0 for j in range(0,len(A))] for i in range(0,len(A))]
	mprint(BB)
	BN = eval('normm%s(BB)'%nmode)
	print BN
	#BN = max([max(BB[i]) for i in range(0,len(BB))])

	CC = BN/(1-BN)
	print "CC: %.4f"%CC

	if BN > 1:
		print 'Solution will not converge!'
		sys.exit(0)


print 'Solving',
X = [0] * len(A)
X_ = deepcopy(X)

step = 0
iter = 0
while True:
	iter += 1
	print '.',
	step += 1
	if step > 20:
		print '\nSolution does not converge!'
		sys.exit(1)

	for i in range(0, len(X)):
		s = 0
		for j in range(0, len(X)):
			if j != i:
				if mode == 'seidel':
					s += A[i][j] * X[j]
				else:
					s += A[i][j] * X_[j]
		X_[i] = X[i]
		X[i] = (B[i] - s) / A[i][i]
	
	dx = [X_[i]-X[i] for i in range(0,len(X))]
	conv = eval('normv%s(dx)'%nmode) < 0.000001

	if conv:
		break

print 
print
print 'Iterations: %i' % iter
print 'Result:\n', '\n'.join('X%i = %.5f'%(i, X[i]) for i in range(0,len(A)))

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