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




def raw_simplex1(A, beta, C, X, Jb):
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

        try:
            B = Ab.I
        except:
            print 'Conditions are not compatible'
            return None, None

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




def full_simplex(A, B, C, D):
    M, N = A.shape
    C_ = np.matrix([0] * N + [-1] * M).T
    E_ = np.identity(M)
    X_ = np.matrix([0.0] * N + [b * 1.0 for b in B]).T
    A_ = np.concatenate([A, E_], 1)

    Jb_ = [N + i for i in range(M)]

    X_, Jb_ = raw_simplex1(A_, B, C_, X_, Jb_)
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
    return raw_simplex(A, B, C, D, X_, Jb_, Jb_[:])


print full_simplex(A, B, C, D)