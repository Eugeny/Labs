#include <stdio.h>
#include <math.h>

int main(int argc, char** argv) {
    float x;
    
    scanf("%f", &x);
    
    
    printf("%d рубл", int(x));
    int r = int(x) % 10;
    if (int(x) > 9 && int(x) < 21)
        printf("ей");
    else if (r == 1)
        printf("ь");
    else if (r > 1 && r <5)
        printf("я");
    else 
        printf("ей");
        
    printf(" %d коп\n", int(round(x*100)) % 100);
    return 0;
}
