#include <stdio.h>
#include <unistd.h>

#define MAXGEN 100
#define CONTROL_FILE "control.txt"


int main() {
    int generation;
    generation = 0;

    while (1) {
        printf("PID %i, generation %i\n", getpid(), generation);

        FILE* cf = fopen(CONTROL_FILE, "r");
        int delay;
        fscanf(cf, "%i", &delay);
        fclose(cf);

        if (delay == 0)
            return;

        usleep(1000000 * delay);

        if (fork() != 0) {
            return 0;
        }

        generation++;

        if (generation > MAXGEN)
            return 0;
    }
    
    return 0;
}
