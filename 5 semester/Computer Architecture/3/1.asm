section .data
    stack         TIMES 100 db 0
    stackp        dd 0


section .text
    global main        

    ; mov XX, YY ---------
    mov XX, 0
    not XX
    and XX, YY

    ; or XX, YY ---------
    mov eax, XX
    not eax
    mov ebx, YY
    not ebx
    and eax, ebx
    not eax
    mov XX, eax
    and eax, eax ; ZF

    ; add XX, YY ---------
    mov edx, XX
    mov ecx, YY
    mov ebx, 0
    _loop:
        mov eax, edx
        and eax, ecx
        or  edx, ecx
        mov ecx, eax

        shl ecx, 1
        jc  _carry
        jmp _no_carry
        _carry:
           mov ebx, 1
        _no_carry:

        and ecx, ecx ; ZF
        jz  _out
        jmp _loop
    _out:
    mov XX, edx
    and ebx, ebx
    jz  _set_c
    jmp _no_c
    _set_c:
        shl ebx, 32 ; CF
    _no_c:
    and edx, edx ; ZF

    ; cmp XX, YY ---------
    mov edx, YY
    mov ecx, XX
    not YY
    add ecx, edx ; ZF, CF

    ; inc XX ---------
    mov edx, 1
    add XX, edx

    ; dec XX ---------
    mov edx, 1
    not edx
    add XX, edx

    ; sub XX, YY ---------
    mov edx, YY
    not YY
    add XX, edx

    ; mul XX, YY ---------
    mov eax, 0
    mov ecx, YY
    _loop:
        add eax, XX
        dec ecx
        jnz _loop

    ; div XX, YY ---------
    mov edx, XX
    mov al, 0
    _loop:
        cmp eax, YY
        jc  _end
        sub eax, XX
        inc al
        jmp _loop
    _end:
    mov ah, eax

    ; push XX ---------
    mov edx, 0
    add edx, dword [stackp]
    mov dword [stack + edx], 0
    add dword [stack + edx], XX
    mov edx, 1
    shl edx, 2
    add dword [stackp], edx
 
    ; pop XX ---------
    mov edx, 0
    add edx, dword [stackp]
    mov XX, 0
    add XX, dword [stack + edx]
    mov edx, 1
    shl edx, 2
    not edx
    add dword [stackp], edx

    ; call XX ---------
    push eip
    jmp XX

    ; ret ---------
    pop XX
    jmp XX


    mov    eax, 0
    ret


