#!/usr/bin/env python
import binascii
import mathlib
import random
import sys


class AsmuthBloom(object):
    def __init__(self, threshold):
        self.threshold = threshold
        self.shares = None
        self._m_0 = 0
        self._y = 0
        self._secret = 0
        
    def _find_group_for_secret(self, k):
        while True:
            m_0 = mathlib.get_prime(k)
            if (mathlib.primality_test(m_0)):
                return m_0

    def _check_base_condition(self, m):
        t, n = self.threshold
        left = 1
        right = m[0]
        for i in xrange(1, t+1):
            left = left*m[i]
        for i in xrange(1, t):
            right = right*m[n-i+1]
        return False or left > right
    
    def _get_m(self, k, h):
        if (h < k):
            raise Exception('Not enought bits for m_1')
        _, n = self.threshold
        m_0 = self._find_group_for_secret(k)
        while True:
            m = [m_0]
            for prime in mathlib.get_consecutive_primes(n, h):
                m.append(prime)
            if (self._check_base_condition(m)):
                return m
            
    def _get_M(self, m):
        M = 1
        t, _ = self.threshold
        for i in xrange(0,t):
            M = M * m[i]
        return M
    
    def _get_y(self, secret, M):
        while True:
            A = mathlib.get_random_range(1, (M - secret) / self._m_0)
            y = secret + A * self._m_0
            if (0 <= y < M):
                break
        return y
    
    def generate_shares(self, secret, k, h):
        if (mathlib.bit_len(secret) > k):
            raise ValueError("Secret is too long")

        m = self._get_m(k, h)
        self._m_0 = m.pop(0)
        
        M = self._get_M(m)
        self._y = self._get_y(secret, M)
        
        self.shares = []
        for m_i in m:
            self.shares.append((self._y % m_i, m_i))
        return self.shares
            
    def combine_shares(self, shares):
        y_i = [x for x, _ in shares]
        m_i = [x for _, x in shares]
        y = mathlib.garner_algorithm(y_i, m_i)
        d = y % self._m_0
        return d


def stringToLong(s):
    return long(binascii.hexlify(s), 16)

    


if len(sys.argv) < 4:
    print 'Usage: ./bloom.py (--random <bits> | <path>) <M> <N>'
    print ' --random <bits>     - generate random secret'
    print ' <path>              - read secret from file'
    print ' <M>                 - number of shares'
    print ' <N>                 - number of shared needed for recovery'
    sys.exit(1)

source = sys.argv[1]
if source == '--random':
    random = random.SystemRandom()
    secret = random.getrandbits(int(sys.argv.pop(2)))
else:
    try:
        secret = stringToLong(open(source).read())
    except:
        print 'Could not read the source file'
        sys.exit(1)

try:
    m = int(sys.argv[3])
except:
    print 'Invalid M'

try:
    n = int(sys.argv[2])
except:
    print 'Invalid N'

if n > m:
    print 'N should be less or equal than M'
    sys.exit(1)

threshold = (m, n)
m_0_bits = 512
m_1_bits = 512
    
print '------------'
print "Secret: %s" % secret
    
ab = AsmuthBloom(threshold)

try:
    shares = ab.generate_shares(secret, m_0_bits, m_1_bits)
except ValueError, e:
    print 'Cannot generate shares: ' + str(e)
    sys.exit(1)
    
print "Secret shares:"
for i in xrange(0,n):
    print " :: %s: %s\n" % (i+1, shares[i])
    
print '------------'

print 

print '------------'
print 'Self-testing'
d = ab.combine_shares(shares[0:m])
print "Recombined secret: %s" % d
print "Self-test %s" % ('successful' if d == secret else 'failed')
print '------------'
