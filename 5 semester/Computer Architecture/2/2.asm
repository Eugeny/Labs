.model small
.stack 100h

.data
    db 129
    db 131
    db 135

    buffer1           dw 0
    buffer2           dw 0
    buffer3           dw 0
    buffer4           dw 0
    buffer5           dw 0
    buffer6           dw 0


    _mixadd_buf1 dw 0
    _mixadd_buf2 dw 0

    _readn_buffer       dw 0

    _readn_backspace    db 8, 32, 8, '$'

    _print_buffer       db  11 dup (0)
                        db  '$'
    _print_bufsize      dw  10
    _print_minus        db  '-$'

    space                 db 32, '$'
    newline             db 10, 13, '$'
    prompt              db 10, 13, '>$'
    res                 db 10, 13, '=$'

    hex         db '0$1$2$3$4$5$6$7$8$9$a$b$c$d$e$f$'
    hexmix      db '0$4$8$c$1$5$9$d$2$6$a$e$3$7$b$f$'
.code

;-------------------------------------
; Exit to DOS
;-------------------------------------
exit proc
    mov     ax, 4c00h
    int     21h
    ret
exit endp

;-------------------------------------
; Print string from AX to $
;-------------------------------------
print proc
    mov     dx, ax
    mov     ah, 9h
    int     21h
    ret
print endp

;-------------------------------------
; Print number from AX
;-------------------------------------
printn proc
    push ax
    push cx
    push dx
    push si

    test    ax, 8000h
    jmp      _printn_plus
    ;jz      _printn_plus
    push    ax
    mov     ax, offset _print_minus
    call    print
    pop     ax

    xor     ax, 0FFFFh
    add     ax, 1

    _printn_plus:

    ; Prepare
    mov     cx, 10
    mov     si, offset _print_buffer
    add     si, [_print_bufsize]
    dec     si
       
    cont:
        mov     dx, 0
        div     cx

        ; Next digit
        dec     si

        ; Save digit
        add     dl, '0'
        mov     [si], dl
    
        cmp     ax, 0
        jne     cont
       
    ; Now print
    mov     ax, si
    call    print

    pop si
    pop dx
    pop cx
    pop ax
    ret
printn endp


;-------------------------------------
; Write character from AL
;-------------------------------------
putc proc
    mov     ah, 2h
    mov     dl, al
    int     21h
    ret
putc endp


;-------------------------------------
; Read character to AL
;-------------------------------------
getch proc
    mov     ah, 8h
    int     21h
    ret
getch endp


;-------------------------------------
; Read number to AX
;-------------------------------------

readn proc
    mov     [_readn_buffer], 0 ; Data
    mov     cx, 0 ; Length

    _readn_cont:
        call    getch
        cmp     al, 48
        jb      _readn_notnum
        cmp     al, 57
        ja      _readn_notnum

            call    putc

            xor     bx, bx
            mov     ah, 0
            add     bx, ax
            sub     bx, 48
            mov     ax, [_readn_buffer]
            mov     si, 10
            mul     si
            add     ax, bx
            mov     [_readn_buffer], ax
            inc     cx
            jmp     _readn_nne

        _readn_notnum:
        cmp     al, 8
        jne     _readn_test_enter
            
            cmp     cx, 0
            je      _readn_nne
            dec     cx
            mov     ax, offset _readn_backspace
            call    print

            mov     ax, [_readn_buffer]
            mov     si, 10
            div     si
            mov     [_readn_buffer], ax

            jmp     _readn_nne

        _readn_test_enter:
        cmp     al, 13
        jne     _readn_nne
            jmp     _readn_done

        _readn_nne:
        jmp _readn_cont

    _readn_done:

    mov     ax, [_readn_buffer]
    ret
readn endp


;-------------------------------------
; Get bit [si*8 + ax] -> ax
;-------------------------------------

getbit proc
    push    si
    push    dx
    push    bx
    push    ax
    push    cx

    add     si, 1
    mov     bx, ax
    shr     bx, 3
    add     si, bx

    and     ax, 7
    mov     bx, 7
    sub     bx, ax
    mov     ax, bx

    mov     bx, 1
    mov     cx, ax
    shl     bx, cl ; bx = mask

    mov     dx, 0
    mov     dl, byte [si]
    and     dx, bx
    shr     dx, cl ; dx = bit
    mov     ax, dx

    pop     cx
    pop     bx
    pop     bx
    pop     dx
    pop     si
    ret
getbit endp

;-------------------------------------
; Set bit [di*8 + ax] = dx
;-------------------------------------

setbit proc
    push    di
    push    dx
    push    bx
    push    ax
    push    cx

    add     di, 1
    mov     bx, ax
    shr     bx, 3
    add     di, bx

    and     ax, 7
    mov     bx, 7
    sub     bx, ax
    mov     ax, bx

    mov     bx, 1
    mov     cx, ax
    shl     bx, cl ; bx = mask, cl = offset

    mov     ax, dx ; ax = bit value
    mov     dx, 0
    mov     dl, byte [di]

    cmp     ax, 0
    je      _setbit_0
    or      dl, bl
    jmp     _setbit_done
_setbit_0:
    not     bl
    and     dl, bl
_setbit_done:

    mov     byte[di], dl

    pop     cx
    pop     ax
    pop     bx
    pop     dx
    pop     di
    ret
setbit endp
 
;-------------------------------------
; Mix copy word si -> di
;-------------------------------------
mixcpy proc
    push    ax
    push    bx
    push    cx
    push    dx
    push    si
    push    di

    mov     cx, 0
    _mixcpy_loop:
        mov     ax, cx
        call    getbit
        mov     dx, ax

        mov     bx, cx
        and     bx, 3
        mov     ax, bx
        shl     ax, 2
        mov     bx, cx
        shr     bx, 2
        add     ax, bx

        call    setbit
        inc     cx
        cmp     cx, 16
        jne     _mixcpy_loop

    pop     di
    pop     si
    pop     dx
    pop     cx
    pop     bx
    pop     ax
    ret
mixcpy endp


;-------------------------------------
; Mix add di += si
;-------------------------------------
mixadd proc
    push    si
    push    di
    push    ax
    push    bx
    push    cx
    push    dx

    mov     cx, si
    mov     dx, di

    mov     si, cx
    mov     di, offset _mixadd_buf1
    call    mixcpy

    mov     si, dx
    mov     di, offset _mixadd_buf2
    call    mixcpy

    mov     ax, word [_mixadd_buf1]
    add     ax, word [_mixadd_buf2]
    mov     word [_mixadd_buf1], ax

    mov     si, offset _mixadd_buf1
    mov     di, dx
    call    mixcpy

    pop     dx
    pop     cx
    pop     bx
    pop     ax
    pop     di
    pop     si
    ret
mixadd endp


;-------------------------------------
; Mix mul di *= si
;-------------------------------------
mixmul proc
    push    si
    push    di
    push    ax
    push    bx
    push    cx
    push    dx

    mov     cx, si
    mov     dx, di

    mov     si, cx
    mov     di, offset _mixadd_buf1
    call    mixcpy

    mov     si, dx
    mov     di, offset _mixadd_buf2
    call    mixcpy

    mov     ax, word [_mixadd_buf1]
    mov     bx, word [_mixadd_buf2]
    mul     bl
    mov     word [_mixadd_buf1], ax

    mov     si, offset _mixadd_buf1
    mov     di, dx
    call    mixcpy

    pop     dx
    pop     cx
    pop     bx
    pop     ax
    pop     di
    pop     si
    ret
mixmul endp

printhex proc
    push dx
    mov dx, offset hex
    add dx, ax
    add dx, ax
    mov ax, dx
    call print
    pop dx
    ret
printhex endp

printhexmix proc
    push dx
    mov dx, offset hexmix
    add dx, ax
    add dx, ax
    mov ax, dx
    call print
    pop dx
    ret
printhexmix endp

annotate proc
    push    ax
    push    dx
    push    dx

    mov     ax, offset newline
    call    print


    mov     cx, 0
_annotate_loop1:
    mov     ax, cx
    call    printhex
    mov     ax, offset space
    call    print
    mov     ax, offset space
    call    print
    inc     cx
    cmp     cx, 16
    jne     _annotate_loop1
    
    mov     ax, offset newline
    call    print

    pop dx  
    mov     cx, 0
_annotate_loop:
    mov     ax,cx
    call    getbit
    call    printn
    inc     cx
    cmp     cx, 16
    jne     _annotate_loop


    mov     ax, offset newline
    call    print
    pop dx 
    pop ax
    ret
annotate endp


annotatemix proc
    push    ax
    push    dx
    push    dx

    mov     ax, offset newline
    call    print

    mov     cx, 0
_annotatemix_loop1:
    mov     ax, cx
    call    printhexmix
    mov     ax, offset space
    call    print
    mov     ax, offset space
    call    print
    inc     cx
    cmp     cx, 16
    jne     _annotatemix_loop1
    
    mov     ax, offset newline
    call    print

    pop dx
    mov     cx, 0
_annotatemix_loop:
    mov     ax, cx
    call    getbit
    call    printn
    inc     cx
    cmp     cx, 16
    jne     _annotatemix_loop


    mov     ax, offset newline
    call    print
    pop dx
    pop ax
    ret
annotatemix endp


main:
    ; Set data segment
    mov     ax, @data
    mov     ds, ax

    mov     ax, offset prompt
    call    print
    call    readn
    mov     word [buffer1], ax
    mov     si, offset buffer1
    call    annotate

    mov     ax, offset prompt
    call    print
    call    readn
    mov     word [buffer2], ax
    mov     si, offset buffer2
    call    annotate


    mov ax, offset newline
    call print

    ;-----
    mov     si, offset buffer1
    mov     di, offset buffer3
    call    mixcpy
    mov     si, offset buffer3
    call    annotatemix

    ;-----
    mov     si, offset buffer2
    mov     di, offset buffer4
    call    mixcpy
    mov     si, offset buffer4
    call    annotatemix

    ;-----
    mov     ax, word [buffer4]
    mov     word [buffer5], ax
    mov     si, offset buffer3
    mov     di, offset buffer5
    call    mixadd
    mov     si, offset buffer5
    call    annotatemix

    mov     ax, offset res
    call    print

    mov     si, offset buffer5
    mov     di, offset buffer6
    call    mixcpy
    mov     ax, word [buffer6]
    call    printn
    ;mov si, offset buffer6
    ;call annotate

    ;-----
    mov     ax, word [buffer4]
    mov     word [buffer5], ax
    mov     si, offset buffer3
    mov     di, offset buffer5
    call    mixmul
    mov     si, offset buffer5
    call    annotatemix

    ;-----
    mov     ax, offset res
    call    print

    mov     si, offset buffer5
    mov     di, offset buffer6
    call    mixcpy
    mov     ax, word [buffer6]
    call    printn
    mov si, offset buffer6
    call annotate


    mov ax, offset newline
    call print

    call exit
    ;---- Memory dump
    mov     si, offset buffer1
    mov     cx, 0
    _loop:
        mov ax, cx
        call getbit
        call printn
        inc cx

        test cx, 7
        jnz __cont
        mov ax, offset newline
        call print

        __cont:
        cmp cx, 64
        jne _loop
    ;----

    call    exit
END main    