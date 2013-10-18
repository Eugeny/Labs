SOURCES = main.o
BINARY = app

all: $(BINARY)

$(BINARY): $(SOURCES) link

link:	
	gcc $(CFLAGS) -g $(SOURCES) -lc -lrt -lpthread -o $(BINARY)

.c.o:
	gcc -c -g $< -o $@

clean:
	rm $(SOURCES) $(BINARY) || true	

test: all
	./app testfile outfile