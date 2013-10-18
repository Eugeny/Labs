#include <stdio.h>
#include <unistd.h>
#include <fcntl.h>
#include <signal.h>
#include <string.h>
#include <time.h>
#include <sys/socket.h>
#include <sys/types.h>
#include <netinet/in.h>
#include <pthread.h>


void worker(fd) {
    #ifdef THREADS
    printf("[thread %i] started, FD %i\n", pthread_self(), fd);
    #else
    printf("[child %i] started, FD %i\n", getpid(), fd);
    #endif

    char inb[1024];
    recv(fd, inb, 1024, 0);
    strtok(inb, " ");
    char* url = strtok(NULL, " ");

    printf("GET %s - %i\n", url, fd);

    char buffer[1024];
    char content[1024];
    sprintf(content, "<html><body>You have requested: %s</body></html>", url);
    sprintf(buffer, "HTTP/1.0 200 OK\r\nConnection: close\r\nContent-Length: %i\r\n\r\n", strlen(content));
    send(fd, buffer, strlen(buffer), 0);
    send(fd, content, strlen(content), 0);
    close(fd);

    #ifdef THREADS
    pthread_exit(0);
    #endif
}


int main() {
    time_t times[1024];
    #ifdef THREADS
    pthread_t threads[1024];
    #else
    pid_t pids[1024];
    #endif

    int index = 0;

    int listener;

    listener = socket(AF_INET, SOCK_STREAM, 0);

    struct sockaddr_in address;
    address.sin_family = AF_INET;
    address.sin_port = htons(8000);
    address.sin_addr.s_addr = htonl(INADDR_ANY);
    bind(listener, (struct sockaddr *)&address, sizeof(address));
    listen(listener, 10);

    while (1) {
        int client = accept(listener, 0, 0);

        #ifdef THREADS
        pthread_create(&threads[index], 0, (void*(*)(void*)) &worker, (void*)client);
        #else
        int pid = fork();
        #endif

        int i;
        for (i = 0; i < index; i++)
            #ifdef THREADS
            //if (threads[i].thread_id != 0 && time(0) - times[i] > 3) {
            //    pthread_kill(threads[i], SIGTERM);
            //   threads[i].thread_id = 0;
            //}
            ;
            #else
            if (pids[i] != 0 && time(0) - times[i] > 3) {
                kill(pids[i], SIGTERM);
                pids[i] = 0;
            }
            #endif

        times[index] = time(0);

        #ifndef THREADS
        if (pid != 0) {
            pids[index++] = pid;
            close(client);
        } else {
            worker(client);
            return 0;
        }
        #endif
    }

    #ifdef THREADS
    pthread_exit(0);
    #endif

    return 0;
}
