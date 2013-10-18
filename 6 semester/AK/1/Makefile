all:
	gcc -g -std=c99 -m32 1.c
	./a.out

2:
	gcc -g -std=c99 -m32 2.c
	./a.out

3:
	gcc -g -std=c99 -m32 3.c
	./a.out

4:
	gcc -g -std=c99 -O0 4.c
	./a.out

5:
	gcc -g -std=c99 -O0 5.c
	./a.out

6:
	gcc -g -std=c99 -O0 6.c
	./a.out

7:
	nasm -f bin -o bootsector 7.s
	dd if=bootsector of=disk.img bs=512
	bochs -f bochsrc -q

8:
	nasm -f bin -o bootsector 8.s
	dd if=bootsector of=disk.img bs=512
	bochs -f bochsrc -q

clean:
	rm *.o *.out || true	
