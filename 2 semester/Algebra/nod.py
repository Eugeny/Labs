from math import *

def xnod(a,b):
    if a*b == 0:
        return a+b
    if abs(a)>abs(b):
        return xnod(a%b,b)
    return xnod(b%a,a)

a = [int(z) for z in raw_input().split(' ')]
l = len(a)

nok = a[0]
nod = a[0]
for i in range(1, l):
    nok = nok * a[i] / xnod(nok,a[i])
    nod = xnod(nod,a[i])

print 'NOD = %i\nNOK = %i' % (nod,nok)
