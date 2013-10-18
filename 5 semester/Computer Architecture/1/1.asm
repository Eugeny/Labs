section	.data
	float_print_buffer 		dq 0;
	float_print_format 		db '%f', 0
	int_print_format 		db "%i", 0
	int_read_buffer			dw 0
	ws_read_format 			db " ", 0
	int_read_format 		db "%i", 0
	float_read_buffer  		dq 0;
	float_read_format  		db "%lf",  0
	newline 				db 0xa, 0
	read_buffer 			TIMES 100 db 0
;--------------------
	n 						dd 0
	m 						dd 0
	s 						dq 0
	t 						dq 0
	xs 						TIMES (8*100) db 0
	ys 						TIMES (8*100) db 0
;--------------------
	str_ans					db "Answer: ", 0
	str_req_n				db "Enter N: ", 0
	str_req_m				db "Enter M: ", 0
	str_req_x_1				db "Enter X", 0
	str_req_x_2				db ": ", 0
	str_req_y_1				db "Enter Y", 0
	str_req_y_2				db ": ", 0


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


read_float:
	pusha

	push 	dword read_buffer
	call	gets
	add		esp, 4

	push 	dword ws_read_format
	call	sscanf
	add		esp, 4

	push 	dword float_read_buffer
	push 	dword float_read_format
	push 	dword read_buffer
	call	sscanf
	add		esp, 12
	
	fld 	qword 	[float_read_buffer]

	popa
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


print_float:
	pusha

	fst 	qword [float_print_buffer]

	push 	dword [float_print_buffer+4]
	push 	dword [float_print_buffer]
	push 	dword float_print_format
	call	printf		
    add     esp, 12

	popa
	ret


main:
	mov 	eax, str_req_n
	call	print_str
	call 	read_int
	mov		[n], eax

	mov 	eax, str_req_m
	call	print_str
	call 	read_int
	mov		[m], eax
	
	mov 	ecx, 0
	_loop_read_x:
		mov 	eax, str_req_x_1
		call	print_str

		mov		eax, ecx
		call	print_int

		mov 	eax, str_req_x_2
		call	print_str

		call 	read_float
		mov 	ebx, [float_read_buffer]
		mov 	dword [xs + 8 * ecx], ebx
		mov 	ebx, [float_read_buffer + 4]
		mov 	dword [xs + 8 * ecx + 4], ebx

		inc 	ecx
		cmp		ecx, [n]
		jne		_loop_read_x


	mov 	ecx, 0
	_loop_read_y:
		mov 	eax, str_req_y_1
		call	print_str

		mov		eax, ecx
		call	print_int

		mov 	eax, str_req_y_2
		call	print_str

		call 	read_float
		mov 	ebx, [float_read_buffer]
		mov 	dword [ys + 8 * ecx], ebx
		mov 	ebx, [float_read_buffer + 4]
		mov 	dword [ys + 8 * ecx + 4], ebx

		inc 	ecx
		cmp		ecx, [n]
		jne		_loop_read_y



	fldz					; 0
	fstp	qword [s]		; ---
	mov		ecx, 0
	_loop_x:
		mov		ebx, 0

		_loop_y:
			fld1				; 1
			fld 	qword [ys + 8 * ecx] ; 1 yn
			fmul 	qword [xs + 8 * ebx] ; 1 yn*xn
			fyl2x							; log()
			fadd 	qword [s]				; log()+s
			fstp	qword [s]				; ---

			inc 	ebx
			cmp		ebx, [m]
			jne		_loop_y

		inc 	ecx
		cmp		ecx, [n]
		jne		_loop_x

	mov 	eax, str_ans
	call 	print_str
	fld		qword [s]
	call 	print_float
	mov 	eax, newline
	call 	print_str

	mov		eax, 0
	ret


