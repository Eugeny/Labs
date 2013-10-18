#include <stdio.h>
#include <unistd.h>
#include <fcntl.h>
#include <string.h>
#include <time.h>

#define CHILDREN 3


void child(fd) {
    printf("[child %i] started, pipe FD %i\n", getpid(), fd);

    while (1) {
        char buffer[1024];
        sprintf(buffer, "[child %i] reporting, t = %i!\n", getpid(), time(0));
        write(fd, buffer, strlen(buffer) + 1);
        usleep(1000000);
    }
}


int main() {
    int i;
    int fds[CHILDREN];

    for (i = 0; i < CHILDREN; i++) {
        int fd[2];
        pipe2(fd, O_NONBLOCK);
        //pipe(fd);

        if (fork() == 0) {
            child(fd[1]);
            return 0;
        } else {
            printf("[parent] Started child %i, pipe FD %i\n", i, fd[0]);
            fds[i] = fd[0];
        }
    }

    while (1) {
        for (i = 0; i < CHILDREN; i++) {
            int l;
            char buffer[1024];
            usleep(10000);
            l = read(fds[i], buffer, 1024);
            if (l > 0) {
                printf(buffer);
            }
        }
    }

    return 0;
}
