a = int(raw_input())
b = int(raw_input())

if a>b:
    a,b=b,a
    
def rx(x):
    print 'rx', x
    if x == -2:
        return a*b
    if x == -1:
        return a
    if x == 0:
        return b
    return rx(x-2) % rx(x-1)

def qx(x):
    print 'qx', x
    return rx(x-1) / rx(x)
    

x = 1
while rx(x) > 0 and (a % rx(x)) + (b % rx(x)) > 0:
    x += 1
    
s = -qx(x-1)
s = 1 + qx(x-1) * qx(x-2)   


t = (rx(x) - a*s)/b

    
print rx(x)
print a, '*', s, '+', b, '*', t, '=', a*s+b*t
