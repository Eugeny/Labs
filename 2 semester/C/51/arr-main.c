#include <stdio.h>
#include <stdlib.h>
#include <memory.h>

#define BUFFER_STEP 100

typedef struct number {
    int* data;
    int bufsize;
    int size;    
} NUMBER;


void nfree(NUMBER* x);
NUMBER* ncreate();
NUMBER* ncopy(NUMBER* x);
void nexpandbuf(NUMBER* x);
void nensurebuffer(NUMBER* x, int size);
void nprint(NUMBER* x);


void nfree(NUMBER* x) {
    free(x->data);
}

NUMBER* ncreate() {
    int i;
        
    NUMBER* x = (NUMBER*)malloc(sizeof(NUMBER));
    x->size = 0;
    x->data = (int*)malloc(BUFFER_STEP * sizeof(int));
    x->bufsize = BUFFER_STEP * sizeof(int);
    for (i = 0; i < x->bufsize / sizeof(int); ++i)
        x->data[i] = 0;
    return x;
}

NUMBER* ncopy(NUMBER* x) {
    NUMBER* n = ncreate();
    *n = *x;
    nensurebuffer(n, x->size);
    memcpy(n->data, x->data, x->bufsize);
    return n;
}

void nexpandbuf(NUMBER* x) {
    int* t = (int*)malloc(BUFFER_STEP * sizeof(int) + x->bufsize);
    int i;
    
    memcpy(t, x->data, x->bufsize);
    free(x->data);
    x->data = t;
    for (i = x->bufsize; i < (x->bufsize + BUFFER_STEP * sizeof(int)); ++i)
        x->data[i] = 0;
    x->bufsize += BUFFER_STEP * sizeof(int);
}

void nensurebuffer(NUMBER* x, int size) {
    while ((x->bufsize) < size * sizeof(int))
        nexpandbuf(x);
}

void nprint(NUMBER* x) {
    int i;
    for (i = x->size-1; i>=0; --i)
        printf("%d", x->data[i]);
    printf("\n");
}

//--------------------

void npush(NUMBER *x, int n) {
    for (x->size = 0; n > 0; x->data[x->size++] = n % 10, n /= 10);
}

void nmultiply(NUMBER* x, int c) {
    int i;
    nensurebuffer(x, x->size + 1);
    
    for (i = 0; i < x->size; ++i)
        x->data[i] *= c;

    x->size++;

    for (i = 0; i <= x->size; ++i)
        if (x->data[i] > 9) {
            x->data[i+1] += x->data[i] / 10;
            x->data[i] %= 10;
        }

    while (x->data[x->size-1] == 0)
        x->size--;
}

NUMBER* nmultiplyn(NUMBER* x, NUMBER* a) {
    NUMBER* t = ncreate();
    nensurebuffer(t, x->size+a->size);
    int i, j;
    
    for (i = 0; i < x->size; ++i)
        for (j = 0; j <= a->size; ++j)
            t->data[i+j] += x->data[i] * a->data[j];
    t->size = a->size + x->size + 1;

    for (i = 0; i <= t->size; ++i)
        if (t->data[i] > 9) {
            t->data[i+1] += t->data[i] / 10;
            t->data[i] %= 10;
        }
       
    while (t->data[t->size-1] == 0)
        t->size--;

    return t;
}

void nadd(NUMBER* x, NUMBER* a) {
    int i, l = x->size;
    if (l < a->size)
        l = a->size;
        
    nensurebuffer(x, x->size + 1);
    
    for (i = 0; i < l; ++i)
        x->data[i] += a->data[i];

    for (i = 0; i <= l; ++i)
        if (x->data[i] > 9) {
            x->data[i+1] += x->data[i] / 10;
            x->data[i] %= 10;
        }
        
    x->size = l+1;
    while (x->data[x->size-1] == 0)
        x->size--;
}


NUMBER* npow(int o, int p) {
    NUMBER* x;
    if (p == 1) {
        x = ncreate();
        npush(x, o);
        return x;
    }

    if (p % 2 == 0) {
        x = npow(o, p/2);
        NUMBER* t = nmultiplyn(x,x);
        nfree(x);
        return t;
    }
    else {
        NUMBER* t = npow(o, p-1);
        nmultiply(t, o);
        return t;
    }
}

int main() {
    int p;
    
    scanf("%d", &p);
    
    NUMBER* x = npow(3, p);
    
    nprint(x);
    return 0;
}
