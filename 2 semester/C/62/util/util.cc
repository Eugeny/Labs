#include "util/util.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>


int Util::readInt(FILE* f) {
    int r;
    fread(&r, sizeof(r), 1, f);
    return r;
}

void Util::writeInt(FILE* f, int x) {
    fwrite(&x, sizeof(x), 1, f);
}

char* Util::readString(FILE* f) {
    int l = Util::readInt(f);
    char* data = new char[l];
    fread(data, 1, l, f); 
    return data;
}
    
void Util::writeString(FILE* f, char* data) {
    Util::writeInt(f, strlen(data)+1);
    fwrite(data, 1, strlen(data)+1, f);
}

int Util::slen(char* s) {
    return mbstowcs(NULL,s,0);
}

char* Util::itoa(int i) {
    char* a = new char[32];
    sprintf(a, "%i", i);
    return a;
}
