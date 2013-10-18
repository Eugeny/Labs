%define DATA_SELECTOR (1 << 3)
%define CODE_SELECTOR (2 << 3)
%define RM_DATA_SELECTOR (3 << 3)
%define RM_CODE_SELECTOR (4 << 3)

%define OFFSET_RM   (80 * 2 * 2)
%define OFFSET_PM   (80 * 2 * 3)
%define OFFSET_RM2  (80 * 2 * 4)


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

    ; Output message
    mov     si, message_rm
    mov     ax, 0xb800
    mov     es, ax
    mov     di, OFFSET_RM
    _loop_msg_rm:
        lodsb
        stosb
        inc     di
        test    al, al
        jnz _loop_msg_rm

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

    ; Output message
    mov     esi, message_pm
    mov     edi, 0xb8000
    add     edi, OFFSET_PM
    _loop_msg_pm:
        lodsb
        stosb
        inc     edi
        test    al, al
        jnz _loop_msg_pm

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

    ; Output message
    mov     si, message_rm2
    mov     ax, 0xb800
    mov     es, ax
    mov     di, OFFSET_RM2
    _loop_msg_rm2:
        lodsb
        stosb
        inc     di
        test    al, al
        jnz _loop_msg_rm2

    sti
    jmp $


message_pm:
    db "Hello Protected World", 0

message_rm:
    db "Hello Real World", 0

message_rm2:
    db "Hello Real World Again", 0


GDTR:
    dw 5 * 8 - 1
    dq GDT

GDT:
    dq 0                            
    db 0xff, 0xff, 0, 0, 0, 0x92, 0x8f, 0 ; PM DATA
    db 0xff, 0xff, 0, 0, 0, 0x9a, 0xcf, 0 ; PM CODE
    db 0xff, 0xff, 0, 0, 0, 0x92, 0x0f, 0 ; RM DATA
    db 0xff, 0xff, 0, 0, 0, 0x9a, 0x0f, 0 ; RM CODE

times 510-($-$$) db 0
    db 0x55
    db 0xAA