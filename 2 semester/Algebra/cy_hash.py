import random

SIZE = 6

def gen_key():
    k = []
    for i in range(0,SIZE):
        k.append([0]*SIZE)
    
    for i in range(SIZE*SIZE/4):
        x,y = -1,-1
        while x < 0 or k[x][y] != 0:
            x = random.randint(0,SIZE-1)
            y = random.randint(0,SIZE-1)            
        k[x][y] = 2
        k[SIZE-1-x][y] = 1
        k[SIZE-1-x][SIZE-1-y] = 1        
        k[x][SIZE-1-y] = 1
    return k
    
    
    
k = gen_key()

print 'KEY:'
for y in range(0,SIZE-1):
    for x in range(0,SIZE-1):
        print ' ' if k[x][y] == 2 else 'X',
    print
    
    
print '\nString > ',
s = raw_input()    
print


r = []
for i in range(0,SIZE):
    r.append([0]*SIZE)

i = 0

def write(k, m, i, fx,fy):
    for y in range(0,SIZE-1):
        for x in range(0,SIZE-1):
            if i<len(s):
                if k[fx(x)][fy(y)] == 2:
                    m[x][y] = s[i]
                    i += 1
    return i
        
i = write(k, r, i, lambda x:x, lambda y:y)        
i = write(k, r, i, lambda x:SIZE-1-x, lambda y:y)        
i = write(k, r, i, lambda x:x, lambda y:SIZE-1-y)


for y in range(0,SIZE-1):
    for x in range(0,SIZE-1):
        if r[x][y] == 0:
            r[x][y] = chr(random.randint(ord('a'),ord('a')+25))
            

print 'CRYPTED RESULT:'
for y in range(0,SIZE-1):
    for x in range(0,SIZE-1):
        print r[x][y],
    print


def read(k, m, fx,fy):
    for y in range(0,SIZE-1):
        for x in range(0,SIZE-1):
            if k[fx(x)][fy(y)] == 2:
                print m[x][y],
            else:
                print '.',
        print
    return i
    

            
print '\nDECRYPTION:'
for y in range(0,SIZE-1):
    for x in range(0,SIZE-1):
        print r[x][y],
    print
    

print '> 1'    
read(k, r, lambda x:x, lambda y:y)        
print '> 2'    
read(k, r, lambda x:SIZE-1-x, lambda y:y)        
print '> 3'    
read(k, r, lambda x:x, lambda y:SIZE-1-y)
    
