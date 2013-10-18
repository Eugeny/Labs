.model small
.stack 100h

.data
    db 102 dup (0)
    buffer dw 0
    db 1024 dup (0)


    _readn_buffer       dw 0

    _readn_backspace    db 8, 32, 8, '$'

    _print_buffer       db  11 dup (0)
                        db  '$'
    _print_bufsize      dw  10
    _print_minus        db  '-$'

    newline             db 10, 13, '$'
    s1                  db 10, 13, 'Normal addressing$'
    s2                  db 10, 13, 'x8 addressing$'
    str_ds              db 10, 13, 'DS: $'
    str_si              db 10, 13, 'SI: $'
    str_phy             db 10, 13, 'Physical: $'
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


addr8 proc
    mov     dx, ds
    shl     dx, 1
    mov     ax, si
    shr     ax, 3
    add     dx, ax
    mov     ax, si
    and     ax, 7
    mov     es, dx
    mov     si, ax
    ret
addr8 endp


main:
    ; Set data segment
    mov     ax, @data
    mov     ds, ax

    mov     si, offset buffer

    mov     ax, offset s1
    call    print

    mov     ax, offset str_ds
    call    print
    mov     ax, ds
    call    printn

    mov     ax, offset str_si
    call    print
    mov     ax, si
    call    printn

    mov     ax, offset str_phy
    call    print
    mov     ax, ds
    mov     bx, 10h
    mul     bx
    add     ax, si
    call    printn


    mov ax, offset newline
    call print


    call    addr8 

    mov     ax, offset s2
    call    print

    mov     ax, offset str_ds
    call    print
    mov     ax, es
    call    printn

    mov     ax, offset str_si
    call    print
    mov     ax, si
    call    printn

    mov     ax, offset str_phy
    call    print
    mov     ax, es
    mov     bx, 8
    mul     bx
    add     ax, si
    call    printn

    mov ax, offset newline
    call print

    call    exit
END main    
