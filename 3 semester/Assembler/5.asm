.model small
.stack 1000h

.data
    xx      dw 0
    yy      dw 0
    a       dw 0
    d       dw 0
    x       dw 0
    y       dw 0
    color   dw 0


.code

;-------------------------------------
; Set a pixel
;-------------------------------------
setpx proc
    ; Video segment
    mov     di, 0A000h
    mov     es, di

    ; Get offset
    mov     dx, 320
    mul     dx
    mov     di, ax
    add     di, bx

    ; Set to white
    mov     ax, [color]
    stosb
    ret
setpx endp


;-------------------------------------
; Draw 4 elliptic points
;-------------------------------------
ell_parts proc
    mov     ax, [xx]
    add     ax, [x]
    mov     bx, [yy]
    add     bx, [y]
    call    setpx

    mov     ax, [xx]
    sub     ax, [x]
    mov     bx, [yy]
    add     bx, [y]
    call    setpx

    mov     ax, [xx]
    add     ax, [x]
    mov     bx, [yy]
    sub     bx, [y]
    call    setpx

    mov     ax, [xx]
    sub     ax, [x]
    mov     bx, [yy]
    sub     bx, [y]
    call    setpx

    ret
ell_parts endp


;-------------------------------------
; Draws a circle at ax-bx with size cx and [color]
;-------------------------------------
ellipse proc
    mov     [xx], ax
    mov     [yy], bx
    mov     [a], cx

    mov     [y], cx
    mov     [x], 0

    mov     bx, 8000h
    add     bx, 3
    sub     bx, [a]
    sub     bx, [a]
    mov     [d], bx

ell_rep:
    call    ell_parts
    mov     ax, [x]
    mov     bx, [y]
    mov     [x], bx
    mov     [y], ax
    call    ell_parts
    mov     ax, [x]
    mov     bx, [y]
    mov     [x], bx
    mov     [y], ax

    ; if d < 0
    cmp     [d], 8000h
    ja      ell_gt

    ; then
    mov     ax, [x]
    shl     ax, 2
    add     ax, 6
    add     [d], ax
    jmp     ell_dn
ell_gt:
    ; else
    mov     ax, [x]
    shl     ax, 2
    add     [d], ax

    mov     ax, [y]
    shl     ax, 2
    sub     [d], ax

    add     [d], 10

    dec     [y]
ell_dn:
    ; endif
    inc     [x]

    ; if x > y exit
    mov     ax, [x]
    mov     bx, [y]
    cmp     ax, bx
    ja      ell_exit
    jmp     ell_rep

ell_exit:
    ret
ellipse endp



;-------------------------------------
; Vertical line
;-------------------------------------
vline proc
    mov     [x], ax
    mov     [y], bx
rep_vl:
    inc     [x]
    mov     ax, [x]
    mov     bx, [y]
    call    setpx
    dec     cx
    cmp     cx, 0
    ja      rep_vl
    ret
vline endp



;-------------------------------------
; Draw a torus
;-------------------------------------
main:
    ; Set data segment
    mov     ax, @data
    mov     ds, ax

    mov     ax, 13h
    int     10h

    mov     bx, 160
    mov     ax, 100
    mov     cx, 90
    mov     [color], 07h
    call    ellipse

    mov     bx, 160
    mov     ax, 100
    mov     cx, 50
    mov     [color], 07h
    call    ellipse

    mov     bx, 160
    mov     ax, 85
    mov     cx, 70
    mov     [color], 0Fh
    call    ellipse

    mov     bx, 160
    mov     ax, 115
    mov     cx, 70
    mov     [color], 08h
    call    ellipse

    mov     bx, 90
    mov     ax, 100
    mov     cx, 20
    mov     [color], 07h
    call    ellipse

    mov     bx, 230
    mov     ax, 100
    mov     cx, 20
    mov     [color], 07h
    call    ellipse

    mov     bx, 160
    mov     ax, 10
    mov     cx, 40
    call    vline

    mov     bx, 160
    mov     ax, 150
    mov     cx, 40
    call    vline

halt:
    hlt
    jmp     halt

END main
