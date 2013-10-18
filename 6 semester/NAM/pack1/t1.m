% Data
clear
syms x u C1 C2

ua_ = 3;
ub_ = 4;
kx_ = x ^ 2;
 
v_c = [1, 2, 0.1, 1, 1, 1, 1];
v_kx = [kx_, v_c(2) * kx_, v_c(3) * kx_, 1 / kx_, kx_, kx_, kx_];
v_ua = [ua_, ua_, ua_, ua_, -ua_, ua_, -ua_];
v_ub = [ub_, ub_, ub_, ub_, ub_, -ub_, ub_];
a = 0.1;
b = 2;

f = x ^ (1/4) + 4;

figure
hold on

sols = sym('a', [1 7]);

for v = 1:7
    eq = int(-(int(f, x) + C1) / v_kx(v)) + C2;
    b1 = subs(eq, x, a) - v_ua(v);
    b2 = subs(eq, x, b) - v_ub(v);
    
    [CC1, CC2] = solve(b1, b2, C1, C2);
    eq = subs(eq, 'C1', CC1);
    eq = subs(eq, 'C2', CC2);
    
    uu = solve(eq - u, u);
    sols(v) = char(uu);
    
end

xs = a:0.01:b;

subplot(1,3,1);
hold on;
for i = 1:3
    plot(xs, subs(sols(i), x, xs));
end

subplot(1,3,2);
hold on;
for i = [1 4]
    plot(xs, subs(sols(i), x, xs));
end

subplot(1,3,3);
hold on;
for i = [5 6 7]
    plot(xs, subs(sols(i), x, xs));
end

display(sols);