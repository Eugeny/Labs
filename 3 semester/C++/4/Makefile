CC = g++
CFLAGS = -c -I .

SOURCES = \
	main.o \
	document.o \
	headline.o \
	element.o \
	line.o \
	text.o \

all: $(SOURCES) link

%.o: %.cc
	$(CC) $(CFLAGS) $< -o $@

link:
	$(CC) *.o -o app    