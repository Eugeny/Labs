.model small
.stack 256

.code

oldh    dw 0
oldh2   dw 0

map db 'QBCEDRTYKUIOMNL;AFWGJVSXHZ[\]^_`qbcedrtykuiomnl;afwgjvsxhz{|}~ '

;-------------------------------------
; TSR
;-------------------------------------
tsr proc
    mov     ax, 3100h
    mov     dx, 1000
    int     21h
    ret
tsr endp


;-------------------------------------
; Interrupt handler
;-------------------------------------
inth:
    sti ; Allow interrupts
    ; Catch input commands
    cmp     ah, 08
    je      new
    cmp     ah, 07
    je      new
    cmp     ah, 01
    je      new
    jmp     old

new:
    ; Call old int21h
    int 22h
    mov ah, 0
    cmp al, 65
    jb exit

    ; Replace characters
    push ds
    push cs
    pop ds
    sub al, 65
    mov bx, offset map
    add bx, ax
    mov al, [bx]
    pop ds
exit:
    iret

old:
    ; Call old one
    push    [cs:oldh]
    push    [cs:oldh2]
    retf
    
rec:    
    iret
inthend:


main:
    ; Load DS
    mov     ax, @code
    mov     ds, ax

    ; Store old handler
    mov     dx, offset inth
    mov     ax, 3521h
    int     21h
    mov     ax, es
    mov     [oldh], es
    mov     [oldh2], bx

    ; Set new handler
    mov     ax, @code
    mov     ds, ax
    mov     dx, offset inth
    mov     ax, 2521h
    int     21h

    ; Store old handler in int22h
    mov     ax, [oldh]
    mov     ds, ax
    mov     dx, [oldh2]
    mov     ax, 2522h
    int     21h

    ; Read some keys
    mlp:
        mov     ah, 7
        int     21h
        mov     dl, al
        mov     ah, 2
        cmp     al, 32
        int     21h
        jne     mlp

    call    tsr
END main
