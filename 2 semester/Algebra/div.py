x,y = raw_input().split(' ')
x = int(x)
y = int(y)
d = x / y
r = x - d * y
if r < 0:
    d += 1
    r = x - d * y
print d, r
