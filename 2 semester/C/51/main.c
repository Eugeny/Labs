#include <stdio.h>
#include <stdlib.h>
#include <memory.h>


struct listitem {
    struct listitem* next;
    struct listitem* prev;
    int value;
};

typedef struct listitem LISTITEM;

LISTITEM* litemcreate() {
    LISTITEM* r = (LISTITEM*)malloc(sizeof(LISTITEM));
    r->next = 0;
    r->prev = 0;
    r->value = 0;
    return r;
}

LISTITEM* listcreate() {
    return litemcreate();
}

int listsize(LISTITEM* list) {
    int sz = 0;
    LISTITEM* t = list;
    while (list != 0) {
        list = list->next;
        sz++;
    }
    if (sz == 1 && t->value == 0)
        return 0;
    return sz;
}

int listget(LISTITEM* list, int pos) {
    int i = 0;
    LISTITEM* c = list;
    for (i = 0; i < pos && c!=0; ++i)   
        c = c->next;
    if (c == 0)
        return 0;
    return c->value;
}

void listadd(LISTITEM* list) {
    while (list->next != 0)
        list = list->next;
    list->next = litemcreate();
    list->next->prev = list;
}

void listset(LISTITEM* list, int pos, int val) {
    int i = 0;
    LISTITEM* c = list;
    for (i = 0; i < pos; ++i) {
        if (c->next == 0)
            listadd(c);
        c = c->next;
    }
    c->value = val;
}


typedef struct number {
    LISTITEM* data;
} NUMBER;


void nfree(NUMBER* x);
NUMBER* ncreate();
NUMBER* ncopy(NUMBER* x);
void nprint(NUMBER* x);


void nfree(NUMBER* x) {
    free(x->data);
}

NUMBER* ncreate() {
    NUMBER* x = (NUMBER*)malloc(sizeof(NUMBER));
    x->data = listcreate();
    return x;
}

NUMBER* ncopy(NUMBER* x) {
    NUMBER* n = ncreate();
    int i;
    for (i = 0; i < listsize(x->data); ++i)
        listset(n->data, i, listget(x->data, i));
    return n;
}

void nprint(NUMBER* x) {
    int i;
    for (i = listsize(x->data)-1; i>=0; --i)
        printf("%d", listget(x->data, i));
    printf("\n");
}

//--------------------

void npush(NUMBER *x, int n) {
    for (; n > 0; listset(x->data, listsize(x->data), n % 10), n /= 10);
}

void nmultiply(NUMBER* x, int c) {
    int i;
    int sz = listsize(x->data);
        
    for (i = 0; i < sz; ++i)
        listset(x->data, i, listget(x->data, i) * c);

    for (i = 0; i <= sz; ++i)
        if (listget(x->data, i) > 9) {
            listset(x->data, i+1, listget(x->data, i+1) + listget(x->data, i) / 10);
            listset(x->data, i, listget(x->data, i) % 10);
        }
}

NUMBER* nmultiplyn(NUMBER* x, NUMBER* a) {
    NUMBER* t = ncreate();
    int i, j;
    
    for (i = 0; i < listsize(x->data); ++i)
        for (j = 0; j < listsize(a->data); ++j)
            listset(t->data, i+j, listget(t->data, i+j) + listget(x->data, i) * listget(a->data, j));

    for (i = 0; i <= listsize(t->data); ++i)
        if (listget(t->data, i) > 9) {
            listset(t->data, i+1, listget(t->data, i+1) + listget(t->data, i) / 10);
            listset(t->data, i, listget(t->data, i) % 10);
        }

    return t;
}

void nadd(NUMBER* x, NUMBER* a) {
    int i, l = listsize(x->data);
    if (l < listsize(a->data))
        l = listsize(a->data);
        
    for (i = 0; i < l; ++i)
        listset(x->data, i, listget(x->data, i) + listget(a->data, i));

    for (i = 0; i <= l; ++i)
        if (listget(x->data, i) > 9) {
            listset(x->data, i+1, listget(x->data, i+1) + listget(x->data, i) / 10);
            listset(x->data, i, listget(x->data, i) % 10);
        }
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
