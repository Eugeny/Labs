CC = g++
CFLAGS = -c -I .

SOURCES = \
	main.o \

all: $(SOURCES) link

%.o: %.cc
	$(CC) $(CFLAGS) $< -o $@

link:
	$(CC) *.o -o app    