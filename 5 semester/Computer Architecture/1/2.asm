section .data
    s                       dd 12.3
    a db "asd"

section .text
    global main         

main:
    mov eax, s
    ret