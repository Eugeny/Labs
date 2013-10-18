.model small
.stack 100h

.data

    a           dw 0
    b           dw 0
    c           dw 0
    d           dw 0
    e           dw 0


    _readn_buffer       dw 0
    _readn_backspace    db 8, 32, 8, '$'

    _print_buffer       db  11 dup (0)
                        db  '$'
    _print_bufsize      dw  10
    _print_minus        db  '-$'

    prompt              db 10, 13, '>$'
    res                 db 10, 13, '=$'
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
    test    ax, 8000h
    jz      _printn_plus
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

    
main:
    ; Set data segment
    mov     ax, @data
    mov     ds, ax

    mov     cx, 6
    mov     bx, 0
    _main_loop_1:
        dec     cx
        cmp     cx, 0
        je      _main_break

        mov     ax, offset prompt
        call    print

        push    cx
        push    bx
        call    readn
        pop     bx
        pop     cx

        cmp     ax, 100
        jb      _main_loop_1
        cmp     ax, 1000
        ja      _main_loop_1
        add     bx, ax

        mov     al, 13
        push    cx
        push    bx
        call    putc
        pop     bx
        pop     cx

        jmp     _main_loop_1

    _main_break:

    mov     ax, offset res
    call    print
    mov     ax,bx
    call    printn
    call    exit
END main    
