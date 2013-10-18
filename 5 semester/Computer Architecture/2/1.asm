section	.data
	int_print_format 		db "%i", 0
	int_read_buffer			dw 0
	ws_read_format 			db " ", 0
	int_read_format 		db "%i", 0
	newline 				db 0xa, 0
	read_buffer 			TIMES 100 db 0
;--------------------
	x 						dw 0
	y 						dw 0
;--------------------
	str_req_x				db "Enter X: ", 0
	str_req_y				db "Enter Y: ", 0


section	.text
    global main			
    extern sscanf
  	extern printf
  	extern gets



read_int:
	pusha

	push 	dword read_buffer
	call	gets
	add		esp, 4

	push 	dword ws_read_format
	call	sscanf
	add		esp, 4

	push 	dword int_read_buffer
	push 	dword int_read_format
	push 	dword read_buffer
	call	sscanf
	add		esp, 12

	popa
	
	mov		eax, [int_read_buffer]
	ret


print_int:
	pusha

	push 	eax
	push 	dword int_print_format
	call	printf		
    add     esp, 8

	popa
	ret


print_str:
	pusha

	push 	eax
	call	printf		
    add     esp, 4

	popa
	ret



main:
	mov 	eax, str_req_x
	call	print_str
	call 	read_int
	mov		[x], eax

	mov 	eax, str_req_x
	call	print_str
	call 	read_int
	mov		[y], eax

	mov		eax, 0
	ret


