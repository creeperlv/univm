.text:
str0 "Hello, World!\n"
.code:
set32 $t0 0
set32 $t1 10
qtext $a2 @str0
set32 $a1 1
set32 $a3 ?str0
START:
syscall 0 4
syscall 0 95
addi $t0 $t0 1
blt $t0 $t1 2
cja >START
coredumptext
