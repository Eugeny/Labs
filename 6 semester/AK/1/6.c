#define TRACE
#include "common-f.c"


int main() {
    reg_t a, b, c;

    printf("Numbers A, B:\n");
    if (!scanf("%f %f", &(a.fp), &(b.fp))) {
        printf("Invalid input\n");
        return 1;
    } else if (b.fp == 0) {
        printf("Division by zero\n");
        return 2;
    }

    dump(a);
    dump(b);

    div(a, b, &c);

    printf("--------------------\n");
    printf("Result:    \n");
    dump(c);

    return 0;
}
