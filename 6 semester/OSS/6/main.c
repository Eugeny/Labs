#include <errno.h>
#include <stdio.h>
#include <unistd.h>
#include <fcntl.h>
#include <string.h>
#include <time.h>
#include <sys/types.h>
#include <sys/mman.h>
#include <sys/shm.h>
#include <pthread.h>
#include <sys/stat.h>
#include <sys/ipc.h>
#include <semaphore.h>


sem_t __SEM_TEST;
#define BUFSIZE 32000
#define SEM_SIZE (sizeof(__SEM_TEST))
#define SHM_SIZE (SEM_SIZE * 2 + sizeof(int) + BUFSIZE)


int main(int argc, char** argv) {
    char* file_in = argv[1];
    char* file_out = argv[2];

    FILE* fin = fopen(file_in, "r");
    FILE* fout = fopen(file_out, "w");

    int shm = shmget(22222, SHM_SIZE, IPC_CREAT | 0666);
    void* shma = shmat(shm, 0, 0);

    sem_t* sem_write = shma;
    sem_t* sem_read = shma + SEM_SIZE;
    int*   size = shma + SEM_SIZE * 2;
    void*  buffer = size + sizeof(int);

    sem_init(sem_write, 1, 0);
    sem_init(sem_read, 1, 0);

    if (fork() == 0) {
        int len = 0;
        while (len = fread(buffer, 1, BUFSIZE, fin)) {
            printf("[%i] read %i bytes\n", getpid(), len);
            *size = len;
            sem_post(sem_read);
            sem_wait(sem_write);
        }
        *size = 0;
        printf("[%i] done reading, exiting\n", getpid());
        fclose(fin);
        sem_post(sem_read);
        return 0;    
    } else {
        while (1) {
            sem_wait(sem_read);
            if (*size == 0)
                break;
            fwrite(buffer, 1, *size, fout);
            printf("[%i] wrote %i bytes\n", getpid(), *size);
            sem_post(sem_write);
        }
        fclose(fout);
        printf("[%i] done writing, exiting\n", getpid());
    }

    return 0;
}
