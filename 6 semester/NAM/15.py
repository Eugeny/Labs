from common_15 import *


def do_step(old, t, dt, fx):
    symbols = [[0] * res for _ in range(res)]
    for x in range(0, res):
        for y in range(0, res):
            s = Symbol('U_%i_%i' % (x, y))
            #s.x = xs[x]
            #s.y = xs[y]
            symbols[x][y] = s

    dscheme = []

    for i in range(res):
        dscheme.append(
            symbols[0][i] - BORDERX0(0, ys[i], t)
        )
        dscheme.append(
            symbols[res-1][i] - BORDERX1(size, ys[i], t)
        )
        dscheme.append(
            symbols[i][0] - BORDERY0(xs[i], 0, t)
        )
        dscheme.append(
            symbols[i][res-1] - BORDERY1(xs[i], size, t)
        )

    for x in range(1, res - 1):
        for y in range(1, res - 1):
            dscheme.append(
                + A / (step ** 2) * (symbols[x+1][y] - 2 * (symbols[x][y]) + symbols[x-1][y])
                + B / (step ** 2) * (old[x+1][y] - 2 * (old[x][y]) + old[x-1][y])
                + fx(xs[x], ys[y])
                - (symbols[x][y] - old[x][y]) / (dt / 2)
            )

    solution = solve(dscheme)

    new = np.zeros((res, res))
    for x in range(0, res):
        for y in range(0, res):
            new[x][y] = solution[symbols[x][y]]

    return new


matrix = np.zeros((res, res))
for x in range(0, res):
    for y in range(0, res):
        matrix[x][y] = INITIAL(xs[x], ys[y], 0)


t = 0


def big_step(matrix, dt):
    matrix = do_step(matrix, t, 0.1, lambda x, y: FX(x, y, t + dt / 2))
    matrix = do_step(matrix, t + dt / 2, 0.1, lambda x, y: FX(x, y, t + dt / 2))
    return matrix

fig.show()

dt = DT
ctr = 0

coords3d = np.meshgrid(xs, ys)
while True:
    print 'Step %i t = %.3f' % (ctr, t)
    ctr += 1

    matrix = big_step(matrix, dt)
    t += dt
    check_err(matrix, t)

    ax.clear()

    values = np.zeros((res, res))
    for x in range(0, res):
        for y in range(0, res):
            values[x][y] = matrix[y][x]

    if USE3D:
        ax.plot_surface(
            coords3d[0],
            coords3d[1],
            values,
            rstride=1,
            cstride=1,
            cmap=matplotlib.cm.jet,
            vmin=VMIN,
            vmax=VMAX,
            shade=True,
        )

        ax.plot([0], [0], [VMIN])
        ax.plot([0], [0], [VMAX])
    else:
        ax.pcolormesh(xs, ys, matrix)

    fig.canvas.draw()

    while fig.waitforbuttonpress(timeout=0.01) is not None:
        pass
