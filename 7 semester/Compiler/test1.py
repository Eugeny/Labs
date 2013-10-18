#from random import random

r = 0.5
total = 0
inside = 0

for _ in range(1000):
    x = random() - 0.5
    y = random() - 0.5
    if ((x * x) + (y * y)) < (r * r):
        inside = inside + 1
    total = total + 1

pi = (1.0 * inside) / total
pi = pi / (r * r)

print ("%.5f" % pi)
