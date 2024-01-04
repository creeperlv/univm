; Sample Program of UNIVM and UNIVM ISA v.1.0
; This program is licensed under the MIT License
.text:
TextIdentifier0 "Hello, World!"
constants:
sys 0
write 64
.code:
								; SPL: sp_lower_half(32bit)
rrsize sp 24					; Expand Stack Pointer by 20 bytes: 4: return value; 4: File Pointer
addi spl spl 4					; Skip first 4 byte.
add64i r5 ?stdout 0				; Set r5 and r6 to (stdout)
s64 r5 sp 						; Store r5 and r6 to sp
addi spl spl 8					; SP offset by 8 bytes
qtext r5 @TextIdentifier0		; Query TextIdentifier0 and store pointer to r5 and r6
s64 r5 sp 						; Store r5 and r6 to sp
addi spl spl 8					; SP offset by 8 bytes
addi r5 @TextIdentifier0 0		; Put the length of TextIdentifier0 to r5
s32 r5 sp 						; Store r5 to sp
syscall %sys %write			; Call write syscall
subi spl spl 24					; Offset back to previous status
rrsize sp -24 					; Shink down stack size
LBL:
nop
j :LBL
exit 0							; End Program
