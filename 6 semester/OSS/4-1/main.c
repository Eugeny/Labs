#include <stdio.h>
#include <signal.h>
#include <unistd.h>

#define CONTROL_FILE "../3/control.txt"


int delay = 1;

void set(int d) {   
    printf("Setting delay: %i\n", d);
    FILE* cf = fopen(CONTROL_FILE, "w");
    fprintf(cf, "%i", d);
    delay = d;
    fclose(cf);
}

void on_up() {
    set(delay + 1);
}

void on_down() {
    if (delay > 0)
        set(delay - 1);
}


int main() {
    signal(SIGUSR1, on_up);
    signal(SIGUSR2, on_down);

    while (1) {
        usleep(1000000);
    }
    
    return 0;
}
