.text:
str0 "Hello, World!\n"
.code:
qtext $a2 @str0
set32 $a1 1
set32 $a3 ?str0
syscall 0 4
syscall 0 95
