all:
	nasm -g -f elf32 1.asm
	gcc -g  1.o 
	./a.out

2:
	nasm -g -f elf32 2.asm
	gcc -g -m32  2.o 
	./a.out

clean:
	rm *.o *.out || true
	
