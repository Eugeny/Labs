#define SIZE 16
#include "common.c"
#define TRACEDIV

void div(reg_p RA, reg_p RB, reg_p RC, reg_p RR) {
    REGISTER(RTQ);
    REGISTER(RTM);
    REGISTER(RTT);
    REGISTER(RTNM);

    copy(RA, RTQ);
    copy(RB, RTM);
    store(RTNM, -extract(RB));

    shls(RTM, SIZE/2);
    shls(RTNM, SIZE/2);

    for (int i = 0; i < SIZE / 2; i++) {
        shl(RTQ);
       
        #ifdef TRACEDIV
            printf("SHL AQ\n");
            printf("-----------------------\nAQ = ");
            dump(RTQ);
            printf("-----------------------\n");
            DOPAUSE
        #endif


        if (RTQ[SIZE-1] == RTM[SIZE-1]) {
            #ifdef TRACEDIV
                printf("A -= M\n");
            #endif
            add(RTQ, RTNM, RTT); 
        } else {
            #ifdef TRACEDIV
                printf("A += M\n");
            #endif
            add(RTQ, RTM, RTT);
        }

        if (RTQ[SIZE-1] == RTT[SIZE-1] || extract(RTT) == 0) {
            #ifdef TRACEDIV
                printf("Q0 = 1\n");
            #endif
            copy(RTT, RTQ);
            RTQ[0] = 1;
        } else {
            #ifdef TRACEDIV
                printf("REVERT A, Q0 = 0\n");
            #endif
            RTQ[0] = 0;
        }


    }

    copy(RTQ, RR);
    shrs(RR, SIZE/2);

    copy(RTQ, RC);
    shls(RC, SIZE/2);
    shrs(RC, SIZE/2);

    if (RA[SIZE-1] != RB[SIZE-1])
        store(RC, -extract(RC));
}


int main() {
    REGISTER(RA);
    REGISTER(RB);
    REGISTER(RC);
    REGISTER(RR);

    int a, b;
    printf("Numbers A, B:\n");
    if (!scanf("%i %i", &a, &b) || b == 0) {
        printf("Invalid input\n");
        return 1;
    }

    store(RA, a);
    store(RB, b);

    div(RA, RB, RC, RR);

    printf("\n\n");
    dump(RA);
    dump(RB);
    printf("--------------------\n");
    printf("Result:    ");
    dump(RC);
    printf("Remainder: ");
    dump(RR);

    return 0;   
}
