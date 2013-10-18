#define SIZE 9
#include "common.c"
#define TRACEMUL

void mul_booth(reg_p RA, reg_p RB, reg_p RC) {
    REGISTER(RTA);
    REGISTER(RTS);
    REGISTER(RTP);
    REGISTER(RTT);

    store(RC, 0);
    store(RTA, extract(RA));
    store(RTS, -extract(RA));
    shls(RTA, SIZE / 2 + 1);
    shls(RTS, SIZE / 2 + 1);

    copy(RB, RTP);
    for (int i = SIZE-1; i >= SIZE/2; i--)
        RTP[i] = 0;
    shl(RTP);

    for (int i = 0; i < SIZE / 2; i++) {
        #ifdef TRACEMUL
            printf("-----------------------\n");
            dump(RTA);
            dump(RTS);
            dump(RTP);
            DOPAUSE
        #endif

        int b0 = RTP[0];
        int b1 = RTP[1];

        if (b0 == 1 && b1 == 0) {
            add(RTP, RTA, RTT);
            copy(RTT, RTP);
        } else if (b0 == 0 && b1 == 1) {
            add(RTP, RTS, RTT);
            copy(RTT, RTP);
        }
        shrs(RTP, 1);
    }


    shrs(RTP, 1);
    
    RTP[SIZE-1] = RTP[SIZE-2];
    dump(RTP);
    copy(RTP, RC);
}

/*
void mul(reg_p RA, reg_p RB, reg_p RC) {
    REGISTER(RT);
    REGISTER(RT2);
    store(RT, 0);
    for (int i = SIZE-1; i >= 0; i--) {
        shl(RT);
        if (RA[i] == 1) {
            add(RT, RB, RT2);
            copy(RT2, RT);
        }
        #ifdef TRACEMUL
        printf("Step %i:\n", SIZE - i);
        dump(RA);
        dump(RB);
        dump(RT);
        DOPAUSE
        #endif
    }
    copy(RT, RC);

    #ifdef TRACEMUL
        store(RT2, 0);

        for (int i = 0; i < SIZE; i++) printf(" ");
        dump(RA);
        for (int i = 0; i < SIZE; i++) printf(" ");
        dump(RB);
        printf("-----------------------------------\n");

        for (int j = 0; j < SIZE; j++) {
            for (int i = 0; i < SIZE - j; i++) printf(" ");
            if (RA[j] == 1)
                dump(RB);
            else
                dump(RT2);
        }

        printf("-----------------------------------\n");
        for (int i = 0; i < SIZE; i++) printf(" ");
        dump(RC);
    #endif
}
*/


int main() {
    REGISTER(RA);
    REGISTER(RB);
    REGISTER(RC);

    int a, b;
    printf("Numbers A, B:\n");
    if (!scanf("%i %i", &a, &b)) {
        printf("Invalid input\n");
        return 1;
    }

    store(RA, a);
    store(RB, b);

    mul_booth(RA, RB, RC);

    printf("\n\n");
    dump(RA);
    dump(RB);
    printf("--------------------\n");
    dump(RC);

    return 0;   
}
