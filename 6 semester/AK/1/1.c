#include "common.c"


void sub(reg_p RA, reg_p RB, reg_p RC) {
    REGISTER(RT);
    int v = extract(RB);
    store(RT, -v);
    add(RA, RT, RC);
}

int main() {
    REGISTER(RA);
    REGISTER(RB);
    REGISTER(RC);

    int a, b;
    printf("Numbers A, B:\n");
    scanf("%i %i", &a, &b);
    
    store(RA, a);
    store(RB, b);

    add(RA, RB, RC);

    printf("\n\n");
    dump(RA);
    dump(RB);
    printf("--------------------\n");
    dump(RC);

    return 0;   
}
