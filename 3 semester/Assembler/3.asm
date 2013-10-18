.model small
.stack 100h

.data

    op_add              db '+'
    op_mul              db '*'
    op_sub              db '-'
    op_div              db '/'
    op_inv              db '!'

    num_1               dw 0

    msg_current_1       db 10, 13, '----------', 10, 13, '= ', '$'
    msg_current_2       db 10, 13, '----------', 10, 13, '$'
    msg_prompt          db '[+] [-] [*] [/] [!] [ESC]', 10, 13, '> ', '$'
    msg_prompt2         db 10, 13, 'operand > $'

    _readn_buffer       dw 0
    _readn_backspace    db 8, 32, 8, '$'

    _print_buffer       db  11 dup (0)
                        db  '$'
    _print_bufsize      dw  10
    _print_minus        db  '-$'


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

    

prep_op proc
    mov     ax, offset msg_prompt2
    call    print
    call    readn
    mov     bx, ax
    mov     ax, [num_1]
    ret
prep_op endp
    
main:
    ; Set data segment
    mov     ax, @data
    mov     ds, ax

    _main_loop:
        mov     ax, offset msg_current_1
        call    print
        mov     ax, [num_1]
        call    printn
        mov     ax, offset msg_current_2
        call    print

        mov     ax, offset msg_prompt
        call    print

        call    getch
        call    putc    

        cmp     al, op_inv
        je      _main_op_inv

        cmp     al, op_add
        je      _main_op_add

        cmp     al, op_sub
        je      _main_op_sub

        cmp     al, op_mul
        je      _main_op_mul

        cmp     al, op_div
        je      _main_op_div

        cmp     al, 27
        je      _main_exit

        jmp     _main_op_done

        _main_op_inv:
            xor     [num_1], 0FFFFh
            add     [num_1], 1
            jmp     _main_op_done

        _main_op_add:
            call    prep_op
            add     ax, bx
            jmp     _main_op_done_2

        _main_op_sub:
            call    prep_op
            sub     ax, bx
            jmp     _main_op_done_2

        _main_op_mul:
            call    prep_op
            mul     bx
            jmp     _main_op_done_2

        _main_op_div:
            call    prep_op
            test    [num_1], 8000h
            jz      _div_cont
            xor     ax, 0FFFFh
            add     ax, 1
            
            _div_cont:
            div     bx

            test    [num_1], 8000h
            jz      _main_op_done_2
            xor     ax, 0FFFFh
            add     ax, 1

            jmp     _main_op_done_2
            
        _main_op_done_2:
        mov     [num_1], ax

        _main_op_done:

        jmp     _main_loop

    _main_exit:
    call    exit
END main    
