import sys

a = int(raw_input())
b = int(raw_input())

def nod(a,b):
    while a * b > 0:
        if a > b:
            a %= b
        else:
            b %= a
    return a + b

n = nod(a, b)
    
for s in range(-1000,2000):
    for t in range(-1000,2000):
        if a*s+b*t == n:
            print a, '*', s, '+', b, '*', t, '=', a*s+b*t        
            sys.exit(0)
            
