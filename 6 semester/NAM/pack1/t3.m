syms x u Du DDu dkxs kxs

a = 0.5;
b = 1.5;
ua = 0;
ub = 0;

k0 = @(x) (1);
k2 = @(x) (0.05 + heaviside(x - 1) * 100);
k3 = @(x) (0.15 + heaviside(x - 0.5 - 1/3) * 5 + heaviside(x - 0.5 - 2/3) * 25);

f0 = @(x) (1);
f1 = 1 / (abs(x - 1) * 5);
f2 = 1 / (abs(x - 0.75) * 25) + 1 / (abs(x - 1.25) * 25);
f2d = 1 / (abs(x - 0.6) * 25) + 1 / (abs(x - 1.4) * 125);

v_f = [5, 1, f1, f2, f2d];
v_kx = {k2, k3, k0, k0 };

for v = 4:4
    figure
    
    if v == 1
        kx = k2;
    elseif v == 2
        kx = k3;
    elseif v == 3
        kx = k0;
    elseif v == 4
        kx = k0;
    elseif v == 5
        kx = k0;
    end
        
    eq = -(dkxs * Du + kxs * DDu) - v_f(v);
    eq = -(kxs * DDu) - v_f(v);

    steps = 25;
    step = (b-a) / steps;
    xs = a:step:b-step;
    us = sym('u', [1 steps]);

    dscheme = sym('d', [1 steps]);
    dscheme(1) = us(1) - ua;
    dscheme(steps) = us(steps) - ub;

    for i = 2:(steps-1)
        dscheme(i) = subs(eq, {x, u, dkxs, kxs, DDu}, {
            xs(i), us(i), (kx(xs(i+1)) - kx(xs(i-1))) / step, kx(xs(i)), (us(i+1) + us(i-1) - 2 * us(i)) / (step ^ 2)
        });
    end

    sol = solve(dscheme);

    ys = zeros(steps);
    for i = 1:steps
        ys(i) = getfield(sol, char(us(i)));
    end

    plot(xs, ys);
end
