.model small
.stack 100h

.data
    format	db	'Result: ', '$'
    a	    dw	2
    b	    dw	2
    c	    dw	27
    d	    dw	2
    buffer	db	11 dup (0)
            db  13, 10, '$'
    bufsize	dw	10



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
; Print string from stack to $
;-------------------------------------
print proc
    pop     cx
    pop     dx
    push    cx
    mov     ah, 9h
    int     21h
    ret
print endp

;-------------------------------------
; Print number from stack
;-------------------------------------
printn proc
    ; Pop number
    pop     cx
    pop     ax
    push    cx
    
    ; Prepare
    mov     cx, 10
    mov     si, offset buffer
    add     si, [bufsize]
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
; Loop done
       
    ; Now print
    push    si
    call    print
    ret
printn endp

    
main:
    ; Set data segment
    mov     ax, @data
    mov     ds, ax
    
    ; Print text
    push    offset format
    call    print
    
    
    ; Condition 1
    mov     ax, [a]
    xor     ax, [b]
    xor     ax, [c]
    
    mov     bx, [c]
    and     bx, [b]
    
    cmp     ax, bx
    je      equals
    jmp     nope


equals:
    mov     ax, [c]
    mul     [d]
    mov     bx, [b]
    add     bx, [a]
    div     bx
    push    ax
    jmp     done


nope:
    mov     ax, [c]
    mul     [b]
    mov     cx, ax
    
    mov     bx, [a]
    add     bx, [b]
    
    mov     ax, bx
    mul     bx
    mul     bx

    cmp     ax, cx
    je      equals2
    jmp     nope2


equals2:
    mov     ax, [a]
    and     ax, [d]
    push    ax
    jmp     done

nope2:
    mov     ax, [c]
    shr     ax, 3
    push    ax
    jmp     done
    
    
done:
    ; Result is in stack
    call    printn

    call    exit
    
END main    
