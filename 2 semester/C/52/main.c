#include <stdio.h>
#include <string.h>
#include <stdlib.h>

struct node {
    struct node* l;
    struct node* r;
    int val;
};

typedef struct node NODE;


NODE* nodeInit(int v) {
    NODE* r = (NODE*)malloc(sizeof(NODE));
    r->val = v;
    r->l = 0;
    r->r = 0;
    return r;
}

void treeAdd(NODE** tree, int v) {
    NODE* r = *tree;
    
    if (r == 0) {
       *tree = nodeInit(v);
       return;
    }
    
    if (v < r->val) {
        treeAdd(&(r->l), v);
    } else {
        treeAdd(&(r->r), v);
    }
}

void treeIter(NODE* r, int d) {
    if (r == 0)
        return;
        
    if (d > 0)
        treeIter(r->l, d);
    else
        treeIter(r->r, d);
        
    printf("%d ", r->val);
    
    if (d < 0)
        treeIter(r->l, d);
    else
        treeIter(r->r, d);
}

void treeFree(NODE* r) {
    if (r == 0)
        return;
    treeFree(r->l);
    treeFree(r->r);
    free(r);
}

void fill(char* s, NODE** tree) {
    int c = 0, i, l = strlen(s);
    for (i = 0; i <= l; ++i) {
        if (s[i] < '0' || s[i] > '9') {
            if (c > 0)
                treeAdd(tree, c);
            c = 0;
        } else 
            c = (c*10) + (s[i] - '0');
    }
}

int i;
NODE* r;
char s[1024];

int main() {
    gets(s);
    fill(s, &r);
    treeIter(r, 1);        
    printf("\n");
    treeIter(r, -1);        
    printf("\n");
    treeFree(r);
    return 0;
}
