# -*- coding: utf-8 -*-
"""
Mathlib by Tomasz Nowacki
"""
import random 

_random = random.SystemRandom()

_small_odd_primes = [
             3,     5,     7,    11,    13,    17,    19,    23,    29,
     31,    37,    41,    43,    47,    53,    59,    61,    67,    71,
     73,    79,    83,    89,    97,   101,   103,   107,   109,   113,
    127,   131,   137,   139,   149,   151,   157,   163,   167,   173,
    179,   181,   191,   193,   197,   199,   211,   223,   227,   229,
    233,   239,   241,   251,   257,   263,   269,   271,   277,   281,
    283,   293,   307,   311,   313,   317,   331,   337,   347,   349,
    353,   359,   367,   373,   379,   383,   389,   397,   401,   409,
    419,   421,   431,   433,   439,   443,   449,   457,   461,   463,
    467,   479,   487,   491,   499,   503,   509,   521,   523,   541,
    547,   557,   563,   569,   571,   577,   587,   593,   599,   601,
    607,   613,   617,   619,   631,   641,   643,   647,   653,   659,
    661,   673,   677,   683,   691,   701,   709,   719,   727,   733,
    739,   743,   751,   757,   761,   769,   773,   787,   797,   809,
    811,   821,   823,   827,   829,   839,   853,   857,   859,   863,
    877,   881,   883,   887,   907,   911,   919,   929,   937,   941,
    947,   953,   967,   971,   977,   983,   991,   997,  1009,  1013,
   1019,  1021,  1031,  1033,  1039,  1049,  1051,  1061,  1063,  1069,
   1087,  1091,  1093,  1097,  1103,  1109,  1117,  1123,  1129,  1151,
   1153,  1163,  1171,  1181,  1187,  1193,  1201,  1213,  1217,  1223,
   1229,  1231,  1237,  1249,  1259,  1277,  1279,  1283,  1289,  1291,
   1297,  1301,  1303,  1307,  1319,  1321,  1327,  1361,  1367,  1373, 
   1381,  1399,  1409,  1423,  1427,  1429,  1433,  1439,  1447,  1451, 
   1453,  1459,  1471,  1481,  1483,  1487,  1489,  1493,  1499,  1511, 
   1523,  1531,  1543,  1549,  1553,  1559,  1567,  1571,  1579,  1583, 
   1597,  1601,  1607,  1609,  1613,  1619,  1621,  1627,  1637,  1657, 
   1663,  1667,  1669,  1693,  1697,  1699,  1709,  1721,  1723,  1733, 
   1741,  1747,  1753,  1759,  1777,  1783,  1787,  1789,  1801,  1811, 
   1823,  1831,  1847,  1861,  1867,  1871,  1873,  1877,  1879,  1889, 
   1901,  1907,  1913,  1931,  1933,  1949,  1951,  1973,  1979,  1987, 
   1993,  1997,  1999,  2003,  2011,  2017,  2027,  2029,  2039,  2053  
]

_miller_rabin_rounds = { 150:27, 200:18, 250:15, 300:12, 350:9, 400:8,
                        450:7, 550:6, 650:5, 850:4, 1250:3}

def bit_len(n):
    """Return size of n in bits."""
    if n < 0: 
        raise ValueError('n < 0')
    
    bits = 0 
    while n > 256:  
        bits = bits + 8
        n = n >> 8
    while n > 0: 
        bits = bits + 1
        n = n >> 1
    return bits
    
def get_random_range(a, b):
    """Return random number between a and b."""
    if a > b:
        a, b = b, a
    a_bits = bit_len(a)
    b_bits = bit_len(b)
    while True:
        k = _random.randrange(a_bits, b_bits)
        n = _random.getrandbits(k)
        if (a < n < b):
            return n
        
def _get_mr_rounds(k):
    """Return number of Miller-Rabin rounds for k-bits length."""
    rounds = 2
    for b_len in _miller_rabin_rounds.keys():
        if (k < b_len):
            rounds = _miller_rabin_rounds[b_len]
    return rounds

def miller_rabin_test(n, rounds):
    """Rabin-Miller primality test. 
    
    Return True if n is probably prime.
    
    """
    if n < 3 or rounds < 1:
        raise ValueError('n < 3 or rounds < 1')
    
    r = n - 1
    s = 0
    while (not r & 1): #r % 2 == 0
        r = r >> 1 #r = r / 2
        s = s + 1
    for _ in xrange(rounds):
        a = random.randint(2, n - 1)
        y = pow(a, r, n)
        if (y != 1 and y != n - 1):
            for _ in xrange(s - 1):
                if (y != n - 1):
                    y = y * y % n
                    if (y == 1):
                        return False #complex
            if (y != n-1):
                return False #complex
    return True #probably prime

def sieve_test(n):
    """Check if n is divisible by small primes."""
    for prime in _small_odd_primes:
        if (n % prime == 0):
            return False or n == prime #complex or simple prime
    return True #test passed

def combined_sieve_test(n):
    """Check if n and p = 2 * n + 1 are good prime candidates."""
    p = n << 1 | 1 #p = 2 * n + 1
    # n and p must be congruent to 2 modulo 3 (p = 7 is an exception)
    if ((p % 3 == 2 and n % 3 == 2) or p == 7):
        for prime in _small_odd_primes:
            if (n % prime == 0 and n % prime == (prime-1)>>1 % prime) :
                return False or n == prime # n or p are complex
        return True # n and p are prime
    return False # n or p are complex

def primality_test(n):
    """Perform primality test for n."""
    if (n & 1): # n % 2 != 0
        rounds = _get_mr_rounds(bit_len(n))
        if(sieve_test(n) and miller_rabin_test(n, rounds)):
            return True
    return False
        
def primality_test_for_sg_prime(n):
    """Primality test for Sophie Germain prime n such that p=2*n+1 is also 
    a prime.
    """
    if (n & 1): # n % 2 != 0
        if (combined_sieve_test(n)):
            p = n << 1 | 1 #p = 2 * n + 1
            n_rounds = _get_mr_rounds(bit_len(n))
            p_rounds = _get_mr_rounds(bit_len(p))
            if(miller_rabin_test(n, n_rounds) \
               and miller_rabin_test(p, p_rounds)):
                return True
    return False

def get_prime(k):
    """Generate k-bit random prime number"""
    while True:
        p = _random.getrandbits(k)
        p = p | (1<< (k-1) | 1) # setting lower and higher bit to 1
        if(primality_test(p)):
            return p

def get_sg_prime(k):
    """Generate k-bit random Sophie Germain prime number n such that p=2*n+1 
    is also a prime"""
    while True:
        n = _random.getrandbits(k)
        n = n | (1<< (k-1) | 1) # setting lower and higher bit to 1
        if (primality_test_for_sg_prime(n)):
            return n
        
def get_consecutive_primes(n, k):
    """Generate n consecutive primes starting from k-bit prime"""
    m = get_prime(k)
    yield m
    n = n - 1
    while n > 0:
        m = m + 2
        if (primality_test(m)):
            yield m
            n = n - 1

def get_consecutive_sg_primes(n, k):
    """Generate n consecutive Sophie Germain primes starting from k-bit prime"""
    m = get_sg_prime(k)
    yield m
    n = n - 1
    while n > 0:
        m = m + 6
        if (primality_test_for_sg_prime(m)):
            yield m
            n = n - 1



def extended_gcd(a, b):
    """Extended Euclidean algorithm."""
    u, u1 = 1, 0
    v, v1 = 0, 1
    g, g1 = a, b
    while g1:
        q = g // g1
        u, u1 = u1, u - q * u1
        v, v1 = v1, v - q * v1
        g, g1 = g1, g - q * g1
    return u, v, g # (a**-1 mod b), (b**-1 mod a), gcd(a,b)

def exgcd(a,b):
    d = a
    x = 1
    y = 0
    if b==0:
        return (d,x,y)
    x2 = 1
    x1 = 0
    y2 = 0
    y1 = 1
    while b > 0:
        q = a//b
        r = a - q*b
        x = x2 - q*x1
        y = y2 - q*y1
        a = b
        b = r
        x2 = x1
        x1 = x
        y2 = y1
        y1 = y
        print "q = %s, r = %s, x= %s, y= %s, a = %s, b= %s, x2 = %s, x1= %s, y2= %s,y1= %s" %(q, r, x, y, a, b, x2, x1, y2, y1)
    d = a
    d = a
    x = x2
    y = y2
    return (d,x,y)
    
def multiplicative_inverse(a, b):
    """Calculate multiplicative inverse of a modulo b."""
    m, _, _ = extended_gcd(a, b)
    if (m < 0):
        m = m % b
    return m

def garner_algorithm(v, m):
    """Garner algorithm for calculating CRT."""
    C = [0]*len(m)
    for i in xrange(1, len(m)):
        C[i] = 1
        for j in xrange(0, i):
            u = multiplicative_inverse(m[j], m[i])
            C[i] = (u * C[i]) % m[i]
    u = v[0]
    x = u
    for i in xrange(1, len(m)):
        u = ((v[i] - x) * C[i]) % m[i]
        s = 1
        for j in xrange(0, i):
            s = s * m[j]
        x = x + u * s
    return x
