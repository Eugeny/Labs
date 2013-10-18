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


D = [[None] * len(A) for i in range(0,len(A))]
S = [[None] * len(A) for i in range(0,len(A))]

D[0][0] = abs(A[0][0]) / A[0][0]
S[0][0] = math.sqrt(abs(A[0][0]))

def getD(y,x):
	if D[y][x] == None:
		d = A[x][x] - sum(getS(p,x)*getS(p,x)*getD(p,p) for p in range(0, y))
		D[y][x] = abs(d) / d
	return D[y][x]

def getS(y,x):
	if S[y][x] == None:
		if y == x:
			d = A[x][x] - sum(getS(p,x)*getS(p,x)*getD(p,p) for p in range(0, y))
			d = math.sqrt(abs(d))
		else:
			d = A[y][x] - sum(getS(p,y)*getD(p,p)*getS(p,x) for p in range(0, y))
			d /= getD(y,y) * getS(y,y)
		S[y][x] = d 
	return S[y][x]
			

for i in range(0,len(A)):
	for j in range(0,i):
		S[i][j] = 0
	for j in range(i,len(A)):
		S[i][j] = getS(i,j)

for i in range(0,len(A)):
	D[i][i] = getD(i,i)
	for j in range(0,len(A)):
		if D[i][j] == None:
			D[i][j] = 0

print '[S]'
mprint(S)

#print '[D]'
#mprint(D)

B = mmmul(mtr(S),D)

#print '[B]'
#mprint(B)



print 'Solving'
Y = [0] * len(A)


Y[0] = F[0] / B[0][0]

for i in range(1, len(A)):
	Y[i] = (F[i] - sum(B[i][s]*Y[s] for s in range(0,i))) / B[i][i]

print '\nY =', ', '.join('%.4f'%x for x in Y)



X = [0] * len(A)
n = len(A)-1
X[n] = Y[n] / S[n][n]

for i in range(n, -1, -1):
	X[i] = (Y[i] - sum(S[i][p]*X[p] for p in range(i+1,len(A)))) / S[i][i]

print '\nX =', ', '.join('%.4f'%x for x in X)


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