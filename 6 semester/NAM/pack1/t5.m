syms x u Du DDu Dt t

k = 1+x*0;%0.5 * x;
f = 0;
eq = (DDu * k + diff(1*k) * Du) + f * (1 - exp(-t)) - Dt;

a = 0;
b = 1;
ua = 0;
ub = 0;

steps = 50;
step = (b-a) / steps;
tstep = 0.03;
xs = a:step:b-step;
us = sym('u', [1 steps]);
uold = zeros([1 steps]);
for i = 1:steps
    uold(i) = (1.25 - xs(i)) ^ 2;
    uold(i) = sin(xs(i)*pi *2);
    uold(i) = heaviside(xs(i) - 0.25) * heaviside(0.75 - xs(i));
end
t = 0;

figure
hold on;

for st = 1:5
    dscheme = sym('d', [1 steps]);
    dscheme(1) = us(1) - ua;
    dscheme(steps) = us(steps) - ub;

    for i = 2:(steps-1)
        dscheme(i) = subs(eq, {x, u, Du, DDu, t, Dt}, {
            xs(i), us(i), (us(i+1) - us(i-1)) / step, (us(i+1) + us(i-1) - 2 * us(i)) / (step ^ 2), t, (us(i) - uold(i)) / tstep
        });
    end

    sol = solve(dscheme);

    ys = zeros([1 steps]);
    for i = 1:steps
        ys(i) = getfield(sol, char(us(i)));
        uold(i) = ys(i);
    end

    plot(xs, ys);
    t = t + tstep;
end
 