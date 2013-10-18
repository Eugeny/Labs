SOURCES = main.o crypt.o
BINARY = app

all: $(BINARY)

$(BINARY): $(SOURCES) link

link:	
	gcc $(SOURCES) -lc -o $(BINARY)

.o:
	gcc -c $< -o $@

test: app
	@echo "Test" > in.txt
	cat in.txt

	@cat in.txt | ./$(BINARY) > out.txt
	hexdump out.txt

	@cat out.txt | ./$(BINARY) > in-new.txt
	@#echo "123" >> in-new.txt

	diff in.txt in-new.txt	
	@rm *.txt

	@echo "Test OK"

clean:
	rm $(SOURCES) $(BINARY) || true	
