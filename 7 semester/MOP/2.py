import time
import numpy as np


A = np.matrix([
    [1, -1, 3, -2],
    [1, -5, 11, -6],
])

beta = np.matrix([1, 9]).T

C = np.matrix([1, 1, -2, -3]).T


Y = np.matrix([1.5,-0.5]).T
Jb = [0,1]


def raw_simplex(A, beta, C, X, Jb):
    print '======= RAW SIMPLEX'
    print 'A', A
    print 'B', beta
    print 'C', C
    print 'X', X
    print 'Jb', Jb
    print '==================='

    M, N = A.shape
    J = range(0, N)

    past_states = []
    step = 0
    while True:
        Xvs = [float(x) for x in X.A]
        if (Xvs,Jb) in past_states:
            print 'Loop!'
            return None, None

        past_states.append((Xvs, Jb[:]))

        step += 1
        print '\n-------------------'
        print 'Step %i' % step
        print 'A:', A
        print 'X:', X
        print 'Jb:', Jb
        print 'C:', C
        print 'Fx=', C.T * X


        Ab = A[:, Jb]
        B = Ab.I

        Cb = C[Jb]
        u = Cb.T * B
        delta = [
            u * A[:, j] - C[j]
            for j in J
        ]

        print 'delta:', delta
        if all(
            delta[i] >= 0
            for i in J
            if i not in Jb
        ):
            print 'Done!'
            return X, Jb

        j0 = 0
        for j in range(N):
            if delta[j] < 0 and j not in Jb:
                j0 = j
                break

        print 'j0:', j0
        z = B * A[:,j0]

        if all(z[i] <= 0 for i in range(M)):
            print 'Unlimited'
            return None, None

        xz = [(i, X[Jb[i]] / z[i]) for i in range(M) if z[i] > 0]
        s, theta = min(xz, key=lambda x: x[1])
        print 'sel:',s
        print 'z:', z
        print 'theta:', theta

        nX = np.matrix([0.0] * N).T
        nX[j0] = theta
        for i in range(M):
            nX[Jb[i]] = X[Jb[i]] - theta * z[i]
        Jb[s] = j0
        X = nX

        time.sleep(0.1)


def raw_duplex(A, beta, C, Y, Jb):
    print '======= RAW DUPLEX'
    print 'A', A
    print 'B', beta
    print 'C', C
    print 'Y', Y
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
        print 'Y:', Y
        print 'Jb:', Jb
        print 'C:', C
        #print 'Fx=', C.T * X

        Ab = A[:, Jb]
        B = Ab.I
        NN = [0] * N
        for i in range(M):
            NN[Jb[i]] = (B * beta)[i]

        if all(x >= 0 for x in NN):
            return NN, Jb

        s = 0
        for s in range(M):
            if NN[Jb[s]] < 0:
                break

        print 's:', s
        dy = (E[:, s].T * B).T

        mu = [0 if j in Jb else dy.T * A[:, j] for j in J]
        if all(mu[j] >= 0 for j in J if not j in Jb):
            print 'Conditions not compatible'
            return None, None

        oj = [(C[j] - A[:, j].T * Y) if j not in Jb and mu[j] < 0 else 9999999 for j in J]
        o0 = min(oj)
        j0 = 0
        for j in J:
            if j not in Jb and mu[j] < 0:
                if oj[j] == o0:
                    j0 = j
                    print 'j0:', j0
                    break

        Y += dy * o0
        Jb[s] = j0
        #return

def full_duplex(A, beta, C):
    M, N = A.shape
    C_ = np.matrix([0] * N + [-1] * M).T
    E_ = np.identity(M)
    X_ = np.matrix([0.0] * N + [b * 1.0 for b in beta]).T
    A_ = np.concatenate([A, E_], 1)
    Jb_ = [N + i for i in range(M)]

    X_, Jb_ = raw_simplex(A_, beta, C_, X_, Jb_)
    print 'Helper plan solution'
    print X_, Jb_

    for i in range(N, N+M):
        if X_[i][0] != 0:
            print 'Conditions incompatible (x%i)!' % i

    k = 0
    while k is not None:
        k = None
        for Jb_i in Jb_:
            if Jb_i >= N:
                k = Jb_i

        if k is not None:
            Ab_ = A_[:,Jb_]
            alpha = [-1 * Ab_.I * A_[:,j] for j in range(N)]
            j0 = None
            print alpha
            for i in range(N):
                if not i in Jb_ and alpha[i][0] > 0:
                    j0 = i
                    break
            if j0 is None:
                print 'Linearly dependent: %i' % (k - M)
                return None, None
            else:
                Jb_.remove(k)
                Jb_ = sorted(Jb_ + [j0])

    print 'Helper plan postprocessed'
    print X_, Jb_
    X_ = X_[:N]
    return raw_duplex(A, beta, C, X_, Jb_)

#X, Jb = full_duplex(A, beta, C)
print raw_duplex(A, beta, C, Y, Jb)
