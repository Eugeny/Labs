import numpy as np
import time

A = np.matrix([
    [1., 0., 2., 1.],
    [0., 1., -1., 2.],
])

B = np.matrix([2., 3.]).T

C = np.matrix([-8., -6., -4., -6.]).T
D = np.matrix([
    [2.,1.,1.,0.],
    [1.,1.,0.,0.],
    [1.,0.,1.,0.],
    [0.,0.,0.,0.],
])

X = np.matrix([2.,3.,0.,0.]).T
Jstar = [0,1]
Jb = [0,1]


def raw_simplex(A, B, C, D, X, Jb, Jstar):
    print '======= RAW SIMPLEX'
    print 'A', A
    print 'B', B
    print 'C', C
    print 'D', D
    print 'X', X
    print 'Jb', Jb
    print '==================='

    M, N = A.shape
    J = range(0, N)
    E = np.identity(M)

    step = 0
    while True:
        step += 1
        print '\n-------------------'
        print 'Step %i' % step
        print 'A:', A
        print 'D:', D
        print 'X:', X
        print 'Jb:', Jb
        print 'J*:', Jstar

        Ab = A[:, Jb]
        AbI = Ab.I
        cx = C + D * X
        cxb = cx[Jb]
        ux = -cxb.T * AbI

        delta = [ux * A[:, j] + cx[j] for j in J]
        print 'delta', delta
        if all(
            delta[i] >= 0
            for i in J
            if i not in Jb
        ):
            print 'Done!'
            return X, Jb

        j0 = 0
        for j in J:
            if delta[j] < 0 and j not in Jb:
                j0 = j
                break

        print 'j0:', j0

        while True:
            Astar = A[:, Jstar]
            Dstar = D[Jstar, :][:, Jstar]

            H = np.concatenate([np.concatenate([Dstar, Astar]), np.concatenate([Astar.T, np.zeros([M, M])])], 1)
            hj0 = np.concatenate([D[Jstar, j0], A[:, j0]])
            ly = -H.I * hj0
            lstar = ly[:len(Jstar)]
            y = ly[len(Jstar):]

            print lstar, y

            beta = D[Jstar, j0].T * lstar + A[:, j0].T * y + D[j0, j0]

            theta = [
                (-X[j] / lstar[Jstar.index(j)] if lstar[Jstar.index(j)] < 0 else float('inf'))
                    if j in Jstar
                    else None
                for j in J
            ]
            theta[j0] = abs(delta[j0] / beta) if beta != 0 else float('inf')
            theta0 = min(theta[j] for j in J if j in Jstar or j == j0)
            print 'beta', beta
            print 'theta', theta
            print 'theta0', theta0
            jstar = theta.index(theta0)
            print 'j*', jstar
            if theta0 == float('inf'):
                print 'Conditions not compatible!'
                return None, None
            
            X_ = [
                float(X[j] + theta0 * lstar[Jstar.index(j)])
                if j in Jstar
                else (
                    float(X[j0] + theta0)
                    if j == j0
                    else
                        0
                )
                for j in J
            ]
            X = np.matrix(X_).T

            if jstar == j0:
                Jstar.append(j0)
                break
            elif jstar in Jstar and jstar not in Jb:
                Jstar.remove(jstar)
                delta[j0] += theta0 * beta
                continue
            elif jstar in Jb and any(
                    E[:, jstar].T * A.I[:, Jb] * A[:, jplus] != 0
                    for jplus in Jstar
                    if jplus not in Jb
                ):
                for jplus in Jstar:
                    if jplus not in Jb:
                        if E[:, jstar].T * A.I[:, Jb] * A[:, jplus] != 0:
                            print 'j+', jplus
                            break
                Jb.remove(jstar)
                Jb.append(jplus)
                Jstar.remove(jstar)
                delta[j0] += theta0 * beta
                continue
            else:
                Jb.remove(jstar)
                Jb.append(j0)
                Jstar.remove(jstar)
                Jstar.append(j0)
                break
        time.sleep(0.5)

raw_simplex(A, B, C, D, X, Jb, Jstar)