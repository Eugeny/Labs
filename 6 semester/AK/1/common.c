#include <stdio.h>
#include <string.h>

#ifndef SIZE
    #define SIZE 16
#endif
#define REGISTER(x) char x[SIZE]
#define DOPAUSE pause();

//#define TRACESUM
#define PAUSE



typedef char* reg_p;

void pause() {
    #ifdef PAUSE
    char buf[1024];
    printf("...\n");
    gets(buf);
    #endif
}

void store(reg_p r, int d) {
    if (d < 0)
        d = ((long int)1 << (SIZE)) + d;

    memset(r, 0, SIZE);

    int idx = 0;
    while (d > 0) {
        r[idx++] = d % 2;
        d /= 2;
    }
}

int extract(reg_p r) {
    int v = 0;

    for (int i = SIZE-1; i >= 0; i--)
        v = v * 2 + r[i];

    if (r[SIZE-1] == 1)
        v = v - ((long int)1 << SIZE);

    return v;
}

void dump(reg_p r) {
    for (int i = SIZE-1; i >= 0; i--) {
        printf("%i", r[i]);
    }
    printf(" == %i\n", extract(r));
}

void shl(reg_p R) {
    for (int i = SIZE-1; i >= 1; i--)
        R[i] = R[i-1];
    R[0] = 0;
}

void shr(reg_p R) {
    for (int i = 0; i < SIZE - 1; i++)
        R[i] = R[i+1];
    R[SIZE - 1] = 0;
}

void shls(reg_p R, int sh) {
    for (int i = SIZE-1; i >= sh; i--)
        R[i] = R[i - sh];
    for (int i = sh-1; i >= 0; i--)
        R[i] = 0;
    R[0] = 0;
}

void shrs(reg_p R, int sh) {
    for (int i = 0; i < SIZE - 1; i++)
        R[i] = R[i + sh];
    for (int i = SIZE-1; i >= SIZE - sh; i--)
        R[i] = R[SIZE - 1 - sh];
}


void copy(reg_p RA, reg_p RB) {
    store(RB, extract(RA));
}



void add(reg_p RA, reg_p RB, reg_p RC) {
    int idx = 0, carry = 0;
    store(RC, 0);
    for (int i = 0; i < SIZE; i++) {
        RC[i] = RA[i] + RB[i] + carry;
        
        carry = 0;
        if (RC[i] > 1) {
            carry = 1;
            RC[i] %= 2;
        }

        #ifdef TRACESUM
            printf("Step %i:\n", i);
            dump(RA);
            dump(RB);
            dump(RC);
            for (int j = 0; j < SIZE - i - 1; j++) printf(" ");
                printf("^\n");
            printf("carry = %i\n", carry);
            DOPAUSE
        #endif
    }
}