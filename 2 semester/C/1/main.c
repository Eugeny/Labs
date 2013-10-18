#include <stdio.h>

int main(int argc, char** argv) {
    int n;
    int c = 100;
    
    scanf("%d", &n);
    n--;
    
    c += n / 3;
    n %= 3;
    
    for (; n < 2; n++)
        c /= 10;
        
    printf("%d\n", c % 10);
    return 0;
}
