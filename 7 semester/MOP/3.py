import numpy as np


A = np.matrix([10, 10, 10]).T
B = np.matrix([15, 15]).T
C = np.matrix([
    [1, 3],
    [999, 4],
    [2, 999],
])

"""
A = np.matrix([30, 40, 20]).T
B = np.matrix([20, 30, 30, 10]).T
C = np.matrix([
    [2, 3, 2, 4],
    [3, 2, 5, 1],
    [4, 3, 2, 6],
])


"""
def transport(A, B, C):
    M, N = C.shape
    I1 = range(M)
    I2 = range(N)
    U = []
    Ub = []
    x = np.zeros(C.shape)
    for j in I2:
        for i in I1:
            U += [(i, j)]
            rx = sum(x[ii, j] for ii in I1)
            tx = sum(x[i, jj] for jj in I2)
            x[i, j] = min(A[i, 0] - tx, B[j, 0] - rx)
            if x[i, j] != 0:
                Ub.append((i, j))

    step = 0
    while True:
        step += 1
        print '-------------'
        print 'Step', step
        print 'Base', Ub
        print 'X:\n', x
        print 'Cost:', np.sum(np.multiply(C, x))

        u = [0] * M
        v = [0] * N

        u[0] = 0
        v[0] = C[0, 0] - v[0]
        i = 0
        j = 0

        def potentialize(i, j):
            if i < M - 1 and x[i + 1, j] > 0:
                u[i + 1] = C[i + 1, j] - v[j]
                potentialize(i + 1, j)
            if j < N - 1 and x[i, j + 1] > 0:
                v[j + 1] = C[i, j + 1] - u[i]
                potentialize(i, j + 1)

        potentialize(0, 0)

        print 'UV', u, v
        delta = np.zeros(C.shape)
        for i in I1:
            for j in I2:
                delta[i, j] = u[i] + v[j] - C[i, j]
        

        print 'Delta:\n', delta

        if all(delta[i,j] <= 0 for i,j in U if (i, j) not in Ub):
            print 'Done'
            return x

        i0 = j0 = 0
        for i, j in U:
            if not (i, j) in Ub:
                if delta[i,j] > delta[i0, j0]:
                    i0, j0 = i, j

        all_directions = [(1,0), (-1,0), (0,1), (0,-1)]
        def findloop(i, j, path, directions):
            path = path + [(i, j)]
            for dx,dy in directions:
                ii = i
                jj = j
                for d in range(0, max(M, N)):
                    ii += dy
                    jj += dx
                    if ii < 0 or jj < 0 or ii == M or jj == N:
                        break

                    if (ii, jj) == path[0]:
                        return path

                    if x[ii, jj] > 0:
                        dirs = all_directions[:]
                        dirs.remove((dx, dy))
                        dirs.remove((-dx, -dy))
                        r = findloop(ii, jj, path, dirs)
                        if r:
                            return r
                        break

        loop = findloop(i0, j0, [], [(1,0), (-1,0)])
        print 'Loop from', i0, j0
        disp = '\n'.join(''.join('*' if (i,j) in loop else '.' for j in I2) for i in I1)
        print disp
        theta = min(x[i, j] for i, j in loop if x[i, j] > 0 and loop.index((i,j)) % 2 == 1)
        for k in range(len(loop)):
            if x[loop[k]] == 0:
                Ub.append(loop[k])
            print loop[k], ('+=' if k % 2 == 0 else '-='), theta
            x[loop[k]] += theta * (1 if k % 2 == 0 else -1)
            if x[loop[k]] == 0 and loop[k] in Ub:
                Ub.remove(loop[k])


print transport(A, B, C)