#include <stdio.h>
#include "crypt.h"

#define BUFSIZE 1024
#define STDIN 0
#define STDOUT 1

int main() {
    char buffer[BUFSIZE];
    int c;

    while (c = read(STDIN, buffer, BUFSIZE)) {
        int i;
        for (i = 0; i < BUFSIZE; i++)
            buffer[i] = crypt(buffer[i]);
        write(STDOUT, buffer, c);
    }

    return 0;
}
