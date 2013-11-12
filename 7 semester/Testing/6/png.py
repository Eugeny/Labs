#!/usr/bin/env python
import binascii
import random
import sha
import sys

def stringToLong(s):
    return long(binascii.hexlify(s), 16)

def longToString(n):
    s = "%x" % n
    if len(s) % 2 == 1:
        s = '0' + s
    return binascii.unhexlify(s)

def is_prime(n, rounds=10):
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


def rng(k):
    return random.getrandbits(k)


def sha1(x):
    s = longToString(x)
    return stringToLong(sha.new(s).digest())



L = 1024
n = 6
b = 63

while True:
    q = 4

    while not is_prime(q):
        seed = rng(256)
        g = len(longToString(seed))
        u = sha1(seed) ^ sha1((seed + 1) % (2 ** g))
        q = u | (2 ** 159) | 1

    counter = 0
    offset = 2

    while True:
        vk = [0] * (n+1)
        for k in range(0, n + 1):
            vk[k] = sha1((seed + offset + k) % (2 ** g))

        w = vk[0] + vk[1] * (2 ** 160) + vk[2] * (2 ** 320) + vk[3] * (2 ** 480) + vk[4] * (2 ** 640) + vk[5] * (2 ** 800) + (vk[6] % (2 ** 63)) * (2 ** 960)

        x = w + 2 ** 1023
        c = x % (2 * q)
        p = x - (c - 1)

        if p >= (2 ** 1023):
            if is_prime(p):
                print 'P:', p
                print 'Q:', q
                #print 'P is prime:', is_prime(p)
                #print 'Q is prime:', is_prime(q)
                sys.exit(0)

        counter += 1
        offset += n + 1
        if counter >= 4 * L - 1:
            break
        else:
            continue


