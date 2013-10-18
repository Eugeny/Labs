#include <stdio.h>

#define uint unsigned long 

#define MAN_GET(x) (x | (1 << 23))
#define MAN_SET(x) (x & ~(1 << 23))
#define EXP_DECODE(x) (x - 127)
#define EXP_ENCODE(x) (x + 127)
#define MAN_LIMIT_U (1 << 24)
#define MAN_LIMIT_D (1 << 23)


#ifdef TRACE
    #define DOPAUSE pause();
#else
    #define DOPAUSE
#endif

typedef struct {
    uint man : 23;
    uint exp : 8;
    uint sgn : 1;
} parts_t;

typedef struct reg {
    union {
        float fp;
        parts_t parts;
    };
} reg_t;




void pause() {
    char buf[1024];
    printf("...\n");
    gets(buf);
}

void store(reg_t* x, float f) {
    x->fp = f;
}

void dump(reg_t x) {
    printf("%16.10f   S=%1i - E=%3i - M=%i\n", x.fp, x.parts.sgn, x.parts.exp, x.parts.man);
}

void normalize(uint* m, reg_t* c) {
    while (*m > MAN_LIMIT_U) {
        c->parts.exp++;
        *m /= 2;
    }
    while (*m < MAN_LIMIT_D) {
        c->parts.exp--;
        *m *= 2;
    }
}

void add(reg_t a, reg_t b, reg_t* c) {
    int de = a.parts.exp - b.parts.exp;

    uint m1 = MAN_GET(a.parts.man), m2 = MAN_GET(b.parts.man);

    if (de > 0) {
        printf("E1 > E2, increasing E2\n");
        DOPAUSE;
        for (int i = 0; i < de; i++) {
            b.parts.exp++;
            m2 /= 2;
        }
    } else if (de < 0) {
        printf("E1 < E2, increasing E1\n");
        DOPAUSE;
        for (int i = 0; i < -de; i++) {
            a.parts.exp++;
            m1 /= 2;
        }
    }

    c->parts.exp = a.parts.exp;

    printf("E3 = %i\n", c->parts.exp);
    long int m3 = 0;

    if (a.parts.sgn != b.parts.sgn) {
        int k1 = a.parts.sgn ? -1 : 1;
        int k2 = b.parts.sgn ? -1 : 1;
        m3 = k1 * m1 + k2 * m2;
        if (m3 < 0) {
            m3 = -m3;
            c->parts.sgn = 1;
        } else {
            c->parts.sgn = 0;
        }
    } else {
        m3 = m1 + m2;
        c->parts.sgn = a.parts.sgn;
    }

    printf("M3 = %li\n", m3);
    DOPAUSE;

    normalize(&m3, c);

    printf("Normalized M3 = %li\n", m3);
    DOPAUSE;

    c->parts.man = MAN_SET(m3);
}


void mul(reg_t a, reg_t b, reg_t* c) {
    c->parts.sgn = a.parts.sgn != b.parts.sgn;
    printf("S3 = %i\n", c->parts.sgn);
    DOPAUSE;

    c->parts.exp = EXP_ENCODE(EXP_DECODE(a.parts.exp) + EXP_DECODE(b.parts.exp));
    printf("E3 = %i\n", c->parts.exp);
    DOPAUSE;
    
    uint m = MAN_GET(a.parts.man);
    m *= MAN_GET(b.parts.man);
    m >>= 23;

    printf("M3 = %li\n", m);
    DOPAUSE;

    normalize(&m, c);

    printf("Normalized M3 = %li\n", m);
    DOPAUSE;
    
    c->parts.man = MAN_SET(m);
}


void div(reg_t a, reg_t b, reg_t* c) {
    c->parts.sgn = a.parts.sgn != b.parts.sgn;
    printf("S3 = %i\n", c->parts.sgn);
    DOPAUSE;

    c->parts.exp = EXP_ENCODE(EXP_DECODE(a.parts.exp) - EXP_DECODE(b.parts.exp));
    printf("E3 = %i\n", c->parts.exp);
    DOPAUSE;
    
    uint m = MAN_GET(a.parts.man);
    m <<= 23;
    m /= MAN_GET(b.parts.man);

    printf("M3 = %li\n", m);
    DOPAUSE;

    normalize(&m, c);

    printf("Normalized M3 = %li\n", m);
    DOPAUSE;
    
    c->parts.man = MAN_SET(m);
}

