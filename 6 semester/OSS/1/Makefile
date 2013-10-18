all:
	gcc -g -m32 1.c
	./a.out

o:                                                  
	gcc -c -g 1.c -o 1.o
	gcc 1.o -lc -o a.out
	./a.out

clean:
	rm *.o *.out || true	
