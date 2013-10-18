from math import *

def euclid(a,b):
    aa,bb = a,b
    a,b = abs(a), abs(b)
    x,y,d = 0,0,0
    if b == 0:
        return a,1,0

    x2,x1,y2,y1,r = 1,0,0,1,0
    while b > 0:
        q, r = a / b, a % b
        x = x2 - q * x1
        y = y2 - q * y1
        
        a,b = b,r
        x2,x1 = x1,x
        y2,y1 = y1,y

        d = a
        x,y = x2,y2
        
        if b>a: a,b=b,a
    
    if aa!=0: x = abs(aa)/aa*x
    if bb!=0: y = abs(bb)/bb*y

    return d,x,y
    
a = [int(z) for z in raw_input().split(' ')]
c = [0] * len(a)

xx,yy=0,0
for i in range(0, len(a)-1):
    yy = a[i]
    d,x,y = euclid(xx,yy)
    for j in range(0, i):
        c[j] *= x
    c[i] = y
    xx = d


s = a[0]*c[0]
print '%i * %i' % (a[0], c[0]),
for i in range(1, len(a)):
    s += a[i]*c[i]
    print ' + %i * %i' % (a[i],c[i]),
    
print ' =', s

print 'GCD =', d


