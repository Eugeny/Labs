from math import *

def mmult(a,b,n):
    r = [0] * len(a)
    s = int(sqrt(len(a)))
    for y in xrange(0,s):
        for x in xrange(0,s):
            e = 0
            for i in xrange(0,s):
                e += a[i*s+x]*b[y*s+i]
            e %= n
            r[y*s+x] = e
    return r

def determ(a, n):
    d = a[0]*(a[4]*a[8]-a[5]*a[7])-a[1]*(a[3]*a[8]-a[5]*a[6])+a[2]*(a[3]*a[7]-a[4]*a[6])
    return d % n

def negative(a, n):
    r = [0]*9
    r[0] = a[4]*a[8]-a[5]*a[7]
    r[3] = a[3]*a[8]-a[5]*a[6]
    r[6] = a[3]*a[7]-a[4]*a[6]
    r[1] = a[1]*a[8]-a[2]*a[7]
    r[4] = a[0]*a[8]-a[2]*a[6]
    r[7] = a[0]*a[7]-a[1]*a[6]
    r[2] = a[1]*a[5]-a[2]*a[4]
    r[5] = a[0]*a[5]-a[2]*a[3]
    r[8] = a[0]*a[4]-a[1]*a[3]
    r = [(x%n)/determ(a,n) for x in r]
    return r
        
def ord(a, n):
    e = [1,0,0,0,1,0,0,0,1]
    x = e
    for i in xrange(0,100):
        x = mmult(x,a,n)
        if x == e:
            return i+1
    
def conforms(a, n):
    x = [1,0,0,0,1,0,0,0,1]
    for i in xrange(0,10):
        if determ(x, n) == 0:
            return False
        x = mmult(x,a,n)
    x = [1,0,0,0,1,0,0,0,1]
    for i in xrange(0,10):
        if determ(x, n) == 0:
            return False
        x = mmult(x,negative(a,n),n)
    return True
        
def subgr(a, n):
    x = [1,0,0,0,1,0,0,0,1]
    r = []
    for i in xrange(0,ord(a,n)):
        if determ(x, n) == 0:
            return False
        x = mmult(x,a,n)
        r.append(x)
    return r
        
def pr(a):
    s = int(sqrt(len(a)))
    for y in xrange(0,s):
        for x in xrange(0,s):
            print a[y*s+x],
        print
    print
    

nz = 2    
a = \
[1,1,0,
 0,1,1,
 0,0,1]
print 'Order:', ord(a,nz)
print 'Cyclic:', conforms(a,nz)

print 'Subgroup:'
for x in subgr(a,nz):
    pr(x)
