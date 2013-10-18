SOURCES = main.o
BINARY = app

all: $(BINARY)

$(BINARY): $(SOURCES) link

link:	
	gcc $(CFLAGS) $(SOURCES) -lc -lpthread -o $(BINARY)

.o:
	gcc -c $< -o $@

clean:
	rm $(SOURCES) $(BINARY) || true	
