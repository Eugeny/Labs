%define DATA_SELECTOR (1 << 3)
%define CODE_SELECTOR (2 << 3)
%define RM_DATA_SELECTOR (3 << 3)
%define RM_CODE_SELECTOR (4 << 3)
%define TSS_1_SELECTOR (5 << 3)
%define TSS_2_SELECTOR (6 << 3)

%define TSS_1_BASE (0x2000)
%define TSS_2_BASE (0x2200)

%define DELAY 3000000


bits 16
org 0x7c00


start:
    cli

    ; Clear screen
    mov     cx, 80 * 25 * 2
    mov     ax, 0xb800
    mov     es, ax
    mov     di, 0
    _loop_clear_screen:
        mov     ax, 0x00
        stosb
        mov     ax, 0x0f
        stosb
        loop    _loop_clear_screen

    ; Entering PM
    lgdt    [cs:GDTR]
    
    mov     eax, cr0
    bts     eax, 0
    mov     cr0, eax
    
    jmp     CODE_SELECTOR:pm_start


bits 32

pm_start:
    ; PROTECTED MODE --------------------
    mov     ax, DATA_SELECTOR
    mov     ds, ax
    mov     es, ax
    mov     ss, ax

    call    main

    ; Leaving PM
    jmp     RM_CODE_SELECTOR:pm_exiting

pm_exiting:
    mov     ax, RM_DATA_SELECTOR
    mov     ds, ax
    
    mov     eax, cr0
    btc     eax, 0
    mov     cr0, eax

    jmp     0:pm_exit

bits 16

pm_exit:
    ; REAL MODE --------------------
    mov     ax, 0
    mov     ds, ax
    sti
    jmp $



bits 32

main:
    mov     edi, GDTEND
    mov     ebx, TSS_1_BASE
    call    make_tdesc

    mov     edi, GDTEND + 8
    mov     ebx, TSS_2_BASE
    call    make_tdesc


    mov     eax, task_1
    mov     edi, TSS_1_BASE
    call    make_tss

    mov     eax, task_2
    mov     edi, TSS_2_BASE
    call    make_tss

    jmp     TSS_1_SELECTOR:0

    ret


; edi = descriptor addr
; ebx = base
make_tdesc:
    mov     word [edi], 0x100 ; limit 0-15
    mov     word [edi + 2], bx ; base 0-15
    mov     word [edi + 4],   1000100100000000b
    mov     word [edi + 6], 0x4
    ret


; edi = base
; eax = EIP
make_tss:
    mov     dword [edi + 0x8], DATA_SELECTOR
    mov     dword [edi + 0x20], eax

    mov     dword [edi + 0x48], DATA_SELECTOR ; ES
    mov     dword [edi + 0x4c], CODE_SELECTOR ; CS
    mov     dword [edi + 0x50], DATA_SELECTOR ; SS
    mov     dword [edi + 0x54], DATA_SELECTOR ; CS
    mov     dword [edi + 0x58], DATA_SELECTOR
    mov     dword [edi + 0x5c], DATA_SELECTOR
    mov     dword [edi + 0x64], 0x1000000
    ret


task_1:
    mov     esi, message1
    mov     edi, 0xb80a0

    .lpmsg:
        lodsb
        stosb
        inc     edi
        test    al, al

        mov     ecx, DELAY
        loop    $

        jmp     TSS_2_SELECTOR:0
        jnz .lpmsg

    mov     eax, [tasks_done]
    bts     eax, 0
    mov     [tasks_done], eax

    .done
        jmp     TSS_2_SELECTOR:0
        jmp     .done


task_2:
    mov     esi, message2
    mov     edi, 0xb8140
    .lpmsg2:
        lodsb
        stosb
        inc     edi
        test    al, al

        mov     ecx, DELAY
        loop    $

        jmp     TSS_1_SELECTOR:0
        jnz .lpmsg2

    mov     eax, [tasks_done]
    bts     eax, 1
    mov     [tasks_done], eax


    ; wait task one
    .wait
        jmp     TSS_1_SELECTOR:0
        mov     eax, [tasks_done]
        test    eax, eax
        jz     .wait

    mov     esi, message3
    mov     edi, 0xb81e0

    .lpmsg:
        lodsb
        stosb
        inc     edi
        test    al, al
        jnz .lpmsg


message1:
    db "First message", 0

message2:
    db "Message from task two; it's quite long!", 0

message3:
    db "Tasks done!", 0


tasks_done:
    dw 0


GDTR:
    dw 8 * 8 - 1
    dq GDT


GDT:
    dq 0                            
    db 0xff, 0xff, 0, 0, 0, 0x92, 0x8f, 0 ; PM DATA
    db 0xff, 0xff, 0, 0, 0, 0x9a, 0xcf, 0 ; PM CODE
    db 0xff, 0xff, 0, 0, 0, 0x92, 0x0f, 0 ; RM DATA
    db 0xff, 0xff, 0, 0, 0, 0x9a, 0x0f, 0 ; RM CODE

GDTEND:

times 510-($-$$) db 0
    db 0x55
    db 0xAA
