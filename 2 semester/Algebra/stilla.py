LEN = 4

s = raw_input()
m = [None] * 100

for i in range(0,100):
    m[i] = [0] * LEN

x,y=0,0
for i in range(len(s)):
    m[x][y] = s[i]
    y += 1
    if y == LEN:
        y = 0
        x += 1

for y in range(0,LEN):
    for x in range(0,len(s)/LEN+1):
        print m[x][y],
    print 
