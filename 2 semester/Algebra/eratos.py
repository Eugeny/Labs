from math import *

n = int(raw_input())
a = range(n+1)

for x in range(2, n/2+1):
    for i in range(2, n/x+1):
        a[i*x] = 0
        
print ' '.join(str(y) for y in filter(lambda x: x>0, a[1:]))
