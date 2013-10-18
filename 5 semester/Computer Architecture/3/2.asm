section .data
string  db 13,10,13,10,"test", 13, 10, 0

section .text
    global main        
    extern printf



proc:
    push string
    
    ;call printf
    push _call_lbl2
    jmp printf
    _call_lbl2:


    add     esp, 4

    ; ret
    pop ebx
    jmp ebx


main:

    ; call proc
    push _call_lbl
    jmp proc
    _call_lbl:

    mov eax, 0

    ; ret
    pop ebx
    jmp ebx
