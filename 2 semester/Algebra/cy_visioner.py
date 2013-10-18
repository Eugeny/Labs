print 'TEXT>',
s = raw_input().lower()
print 'KEY>',
k = raw_input().lower()
print

r = ''
for i in range(0,len(s)):
    if s[i] >= 'a' and s[i] <= 'z':
        r += chr(ord('a')+((ord(s[i])-ord('a')) + (ord(k[i%len(k)])-ord('a')))%26)
    else:
        r += s[i]
    
print 'RESULT:'
print r    
