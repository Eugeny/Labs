.model small
.stack 100h

.data
    newline             db  13, 10, '$'
    p_width             db  'Matrix width: $'
    p_height            db  'Matrix height: $'
    p_e1                db  '[$'
    p_e2                db  ', $'
    p_e3                db  '] = $'
    _readn_buffer       dw 0
    _readn_backspace    db 8, 32, 8, '$'
    _print_buffer       db  11 dup (0)
                        db  '$'
    _print_bufsize      dw  10

    mw                  dw  0
    mh                  dw  0
    m                   dw  256 dup (0)
    m2                  dw  256 dup (0)

    x                   dw 0
    y                   dw 0
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


printm proc
    push    ax
    mov     [y], 0
loopmy:
    mov     [x], 0

    loopmx:
        pop     ax
        push    ax
        mov     di, ax
        mov     ax, [y]
        mul     [mw]
        shl     ax, 1
        add     di, [x]
        add     di, [x]
        add     di, ax
        mov     ax, [di]
        call    printn

        inc     [x]
        mov     ax, [x]
        cmp     ax, [mw]
        jb      loopmx

    mov     ax, offset newline
    call    print

    inc     [y]
    mov     ax, [y]
    cmp     ax, [mh]
    jb      loopmy

    pop     ax
    ret
printm endp

transp proc
    mov     [y], 0
loopty:
    mov     [x], 0

    looptx:
        mov     di, offset m
        mov     ax, [y]
        mul     [mw]
        shl     ax, 1
        add     di, [x]
        add     di, [x]
        add     di, ax

        mov     bx, [mw]
        sub     bx, [x]
        sub     bx, 1

        mov     cx, [mh]
        sub     cx, [y]
        sub     cx, 1

        mov     si, offset m2
        add     si, bx
        add     si, bx
        mov     ax, cx
        mul     [mw]
        shl     ax, 1
        add     si, ax

        mov     ax, [di]
        mov     [si], ax

        inc     [x]
        mov     ax, [x]
        cmp     ax, [mw]
        jb      looptx

    inc     [y]
    mov     ax, [y]
    cmp     ax, [mh]
    jb      loopty

    ret
transp endp


    
main:
    ; Set data segment
    mov     ax, @data
    mov     ds, ax
    mov     es, ax

    mov     ax, offset p_width
    call    print
    call    readn
    mov     [mw], ax
    mov     ax, offset newline
    call    print

    mov     ax, offset p_height
    call    print
    call    readn
    mov     [mh], ax
    mov     ax, offset newline
    call    print

    mov     [y], 0
loopy:
    mov     [x], 0

    loopx:
        mov     ax, offset p_e1
        call    print

        mov     ax, [x]
        call    printn

        mov     ax, offset p_e2
        call    print

        mov     ax, [y]
        call    printn

        mov     ax, offset p_e3
        call    print

        call    readn
        push    ax

        mov     ax, offset newline
        call    print

        mov     di, offset m
        mov     ax, [y]
        mul     [mw]
        shl     ax, 1
        add     di, ax
        add     di, [x]
        add     di, [x]
        pop     ax
        mov     [di], ax

        inc     [x]
        mov     ax, [x]
        cmp     ax, [mw]
        jb      loopx

    inc     [y]
    mov     ax, [y]
    cmp     ax, [mh]
    jb      loopy

    mov     ax, offset newline
    call    print

    mov     ax, offset m
    call    printm
        
    call    transp

    mov     ax, offset newline
    call    print
    
    mov     ax, offset m2
    call    printm

    call    exit
END main    
