m = int(raw_input())

def table(s, fun):
    r = [];
    for x in range(s):
        r.append([fun(x, z)%m for z in range(s)])
    return r
    
def pr(t, s, h):
    print h, ' '.join(str(z) for z in range(s))
    for y in range(s):
        print y, (' '.join(str(t[x][y]) for x in range(s)))
    print        
        
t = table(10, lambda x,y:x+y)
pr(t, 10, '+')

t = table(10, lambda x,y:x*y)
pr(t, 10, '*')

