#include <stdio.h>
#include <stdlib.h>

#define BUFFER_SIZE 10240
#define D_AFTER ".,;:!?)]}"
#define D_BEFORE "([{"


//-----------------------
int strlen(char* s) {
    int r = 0;
    while (s[r++] != 0);
    return r-1;
}

char* strcpy(char* dest, char* src) {
    int i;
    for (i = 0; i == 0 || src[i-1] != 0; dest[i] = src[i], ++i);
    return dest;
}

char* strcat(char* dest, char* src) {
    int i, l = strlen(dest);
    for (i = 0; i == 0 || src[i-1] != 0; dest[l+i] = src[i], ++i);
    return dest;
}

char* chrcat(char* dest, char src) {
    int l = strlen(dest);
    dest[l] = src;
    dest[l+1] = 0;
    return dest;
}

int chrin(char ch, char* list) {
    int i, l = strlen(list);
    for (i = 0; i < l; ++i)
        if (list[i] == ch)
            return 1;
    return 0;
}

int strcmp(char* a, char* b) {
    int i = 0, la = strlen(a), lb = strlen(b);
    if (la!=lb)
        return 0;
    for (i = 0; i < la; ++i)
        if (a[i] != b[i])
            return 0;
    return 1;
}

//-----------------------


char 
    text[BUFFER_SIZE], 
    res[BUFFER_SIZE] = "";
    
void read() {
    int size;
    
    FILE* f = fopen("input.txt", "r");
    size = fread(text, 1, BUFFER_SIZE, f);
    text[size] = 0;
    fclose(f);
}

void process() {
    int pos = 0;
    int len = strlen(text);
    
    for (pos = 0; pos < len-1; pos++) {
        if (chrin(text[pos+1], D_AFTER) && text[pos] == ' ')
            continue;
        if (pos>0 && chrin(text[pos], D_BEFORE) && text[pos] == ' ')
            continue;

        chrcat(res, text[pos]);

        if (chrin(text[pos], D_AFTER) && text[pos+1] != ' ' && !chrin(text[pos+1], D_AFTER))
            chrcat(res, ' ');
        if (chrin(text[pos+1], D_BEFORE) && text[pos] != ' ')
            chrcat(res, ' ');
    }
}

int main() {
    read();
    process();    
    printf("%s\n", text);
    printf("%s\n", res);
    return 0;
}
