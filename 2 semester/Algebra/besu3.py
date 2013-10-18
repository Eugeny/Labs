def euclid(a,b):
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
    return d,x,y
    
a,b = [int(z) for z in raw_input().split(' ')]
d,x,y = euclid(a,b)

print 'GCD =', d
print '%i * %i + %i * %i = %i' % (a,x,b,y,d)


