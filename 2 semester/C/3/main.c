#include <stdlib.h>
#include <stdio.h>
#include <string.h>

struct Matrix {
    char *data;
    int width, height;
};

void resizeMatrix(Matrix *m) {
    m->data = (char*)malloc(m->width * m->height);
}

void disposeMatrix(Matrix *m) {
    free(m->data);
}

Matrix loadMatrix(FILE* file) {
    struct Matrix r;
    fscanf(file, "%d %d\n", &r.width, &r.height);
    resizeMatrix(&r);
    char buf[100];
    for (int y = 0; y < r.height; ++y) {
        fgets(buf, 100, file);
        for (int x = 0; x < r.width; ++x) {
            r.data[x+y*r.width] = (buf[x]=='x')?1:0;
        }
     }
    return r;
}

void printMatrix(Matrix m) {
    for (int y = 0; y < m.height; ++y) {
        for (int x = 0; x < m.width; ++x)
            printf((m.data[x+y*m.width]==0)?" ":"x");
        printf("\n");
    }
}

int matchMatrix(Matrix subject, Matrix pattern, int dx, int dy) {
    for (int y = 0; y < pattern.height; ++y)
        for (int x = 0; x < pattern.width; ++x)
            if (pattern.data[x+y*pattern.width] * subject.data[x+dx+(y+dy)*subject.width] == 1)
                return 0;
    return 1;
}

int matchMatrix(Matrix subject, Matrix pattern) {
    for (int y = 0; y <= subject.height-pattern.height; ++y)
        for (int x = 0; x <= subject.width-pattern.width; ++x)
            if (matchMatrix(subject, pattern, x, y)) 
                return 1;
    return 0;                
}

void rotateMatrix(Matrix *m) {
    char *b = m->data;
    int t = m->height; m->height = m->width; m->width = t;
    resizeMatrix(m);

    for (int y = 0; y < m->height; ++y)
        for (int x = 0; x < m->width; ++x)
            m->data[x + y * m->width] = b[x * m->height + m->height - 1 - y];
    free(b);
}

int matchMatrixRotated(Matrix *subject, Matrix *pattern) {
    for (int i = 0; i < 4; ++i) {
        if (matchMatrix(*subject, *pattern))
            return 1;
        rotateMatrix(pattern);
    }
    return 0;
}

int main() {
    struct Matrix key, hole;
    FILE* file = fopen("data", "r");
    hole = loadMatrix(file);
    key = loadMatrix(file);
    fclose(file);
    
    printf("Hole:\n");
    printMatrix(hole);
    printf("\nKey:\n");
    printMatrix(key);
    printf("\n");
    
    printf(matchMatrixRotated(&hole,&key) ? "Matches\n" : "Doesn't match\n");
    disposeMatrix(&hole);
    disposeMatrix(&key);
}
