.model small
.stack 100h

.data
    counts  db 256 dup (0)
    buffer  db 30 dup (0)
    _print_buffer       db  11 dup (0)
                        db  '$'
    _print_bufsize      dw  10
    newline             db  13, 10, '$'

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
; Print number from AX
;-------------------------------------
printn proc
    test    ax, 8000h

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
; Print string from AX to $
;-------------------------------------
print proc
    mov     dx, ax
    mov     ah, 9h
    int     21h
    ret
print endp


;-------------------------------------
; Read string to [AX]
;-------------------------------------

read proc
    mov     bx, ds
    mov     es, bx
    mov     di, ax

    _read_cont:
        call    getch
        cmp     al, 13
        call    putc
        je      _read_done
        stosb
        jmp     _read_cont

    _read_done:
    mov     al, 13
    stosb
    mov     al, 10
    stosb
    call    putc
    mov     al, '$'
    stosb
    ret
read endp

    
main:
    ; Set data segment
    mov     ax, @data
    mov     ds, ax

    mov     ax, offset buffer
    call    read

    mov     si, offset buffer
    _main_loop:
        mov     di, offset counts
        xor     dx, dx
        mov     dl, byte ptr [si]
        add     di, dx
        inc     byte ptr [di]
        cmp     byte ptr [si], '$'
        je      _main_loop_ex
        inc     si
        jmp     _main_loop
    _main_loop_ex:

    mov     si, offset counts
    mov     cx, 256
    mov     bx, 0
    mov     di, 0
    _main_loop_2:
        cmp     byte ptr [si], bl
        jb      _main_loop_2_cont
            mov     di, si
            mov     byte ptr bl, [di]
        _main_loop_2_cont:
        inc     si
        dec     cx
        cmp     cx, 0
        jne     _main_loop_2

    push    di
    mov     ax, di
    call    printn

    mov     ax, offset newline
    call    print

    pop     ax
    call    putc


    call    exit
END main    
