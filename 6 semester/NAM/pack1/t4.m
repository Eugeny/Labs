syms x u Du DDu 

k = 0.5 * x;
q = 1.5% * x ^ 2;
f = 2.5 * x ^ 3;

a = 1.5;
b = 5.5;

eq = -(diff(k) * Du + DDu * k) + q * u - f;

steps = 100;
step = (b-a) / steps;
xs = a:step:b-step;
us = sym('u', [1 steps]);

dscheme = sym('d', [1 steps]);
dscheme(1) = -subs(k, x, a) * (us(2) - us(1)) / step + 0.5 * us(1);
dscheme(steps) = -subs(k, x, b) * (us(steps) - us(steps-1)) / step + 0.5 * us(steps);

for i = 2:(steps-1)
    dscheme(i) = subs(eq, {x, u, Du, DDu}, {
        xs(i), us(i), (us(i+1) - us(i-1)) / step, (us(i+1) + us(i-1) - 2 * us(i)) / (step ^ 2)
    });
end

sol = solve(dscheme);

ys = zeros(steps);
for i = 1:steps
    ys(i) = getfield(sol, char(us(i)));
end

plot(xs, ys);

 