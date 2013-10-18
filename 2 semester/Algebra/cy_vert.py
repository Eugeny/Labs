import random
SIZE = 6

s = raw_input()

k = []
for i in range(0,len(s)):
    k.append(random.randint(0,SIZE-2))

r = []
for i in range(0,len(s)):
    r.append([0]*SIZE)

for i in range(0,len(s)):
    r[i][k[i]] = s[i]

for y in range(0,SIZE-1):
    for x in range(0,len(s)):
        if r[x][y] == 0:
            r[x][y] = chr(random.randint(ord('a'),ord('a')+25))

print '\nKEY:'
for y in range(0,len(s)):
    print k[y],
print

print '\nRESULT:'
for y in range(0,SIZE-1):
    for x in range(0,len(s)):
        print r[x][y],
    print
    
