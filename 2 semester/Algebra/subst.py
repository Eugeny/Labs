def mult(a,b):
    n = [0]*len(b)
    for i in xrange(0,len(b)):
        n[i] = a[b[i]-1]
    return n
    
def pr(x):
    print '[',
    for i in xrange(0,len(x)):
        if x[i] != i+1:
            print i+1,
    print ']\n[',
    for i in xrange(0,len(x)):
        if x[i] != i+1:
            print x[i],
    print ']'

def make(l):
    return list(xrange(0,l+1))[1:]

def empty(x):
    for i in xrange(0,len(x)):    
        if x[i] != i+1:
            return False
    return True            
            
def mkcycle(x):
    s = 0
    while x[s]==s+1:
        s += 1
       
    r = [x[s]]
    c = x[s]-1
    while c != s:
        r.append(x[c])
        c = x[c]-1
    return r
    
def splitcycle(x):
    c = mkcycle(x)
    r = []
    for i in xrange(0, len(c)-1):
        n = make(len(x))
        n[c[0]-1] = c[-1-i]
        n[c[-1-i]-1] = c[0]
        r.append(n)
    return r
        
def split(x):
    u = [0]*len(x)
    r = []
    while sum(u) < len(x):
        c = 0
        while u[c] != 0:
            c += 1
        n = make(len(x))
        p = x[c]-1
        n[c] = x[c]
        u[c] = 1
        while p != c:
            u[p] = 1
            n[p] = x[p]
            p = x[p]-1
        
        if not empty(n):
            r.append(n)            
    return r
            
                
a=[3,8,4,5,6,1,7,2]
#a=[4,5,1,6,7,8,9,3,2]
a = [int(x) for x in raw_input().split()]


print 'Source:'
pr(a)
print

print 'Cycles:'
for x in split(a):
    print mkcycle(x),
print '\n'

ss = []
print 'Full split:'
for x in split(a):
    for y in splitcycle(x):
        print mkcycle(y),
        ss.append(y)
print '\n'

print 'Recovering:'
x = make(len(a))
for y in ss:
    x = mult(x,y)
pr(x)

