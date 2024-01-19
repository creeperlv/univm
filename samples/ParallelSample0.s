.text:
DOS "This program connot run in DOS mode.\n"
Main "Entered Main\n"
text0 "Core0\n"
text1 "Core1\n"
text2 "Core2\n"
text3 "Core3\n"
.code:
	ja >main
	set32 $a1 1
	qtext $a2 @DOS
	set32 $a3 ?DOS
	syscall 0 4
	syscall 0 95
	core0:
		set32 $t0 0
		set32 $t1 10
		set32 $a1 1
		qtext $a2 @text0
		set32 $a3 ?text0
		core0_loop:
			syscall 0 4
			syscall 0 95
			addi $t0 $t0 1
			blt $t0 $t1 2
		cja >core0_loop
	ret
	core1:
		set32 $t0 0
		set32 $t1 10
		set32 $a1 1
		qtext $a2 @text1
		set32 $a3 ?text1
		core1_loop:
			syscall 0 4
			syscall 0 95
			addi $t0 $t0 1
			blt $t0 $t1 2
		cja >core0_loop
	ret
	core2:
		set32 $t0 0
		set32 $t1 10
		set32 $a1 1
		qtext $a2 @text2
		set32 $a3 ?text2
		core2_loop:
			syscall 0 4
			syscall 0 95
			addi $t0 $t0 1
			blt $t0 $t1 2
		cja >core2_loop
	ret
	core3:
		set32 $t0 0
		set32 $t1 10
		set32 $a1 1
		qtext $a2 @text3
		set32 $a3 ?text3
		core3_loop:
			syscall 0 4
			syscall 0 95
			addi $t0 $t0 1
			blt $t0 $t1 2
		cja >core3_loop
	ret
	main:
		set32 $a1 1
		callp >core0
		callp >core1
		callp >core2
		callp >core3
		set32 $t1 0
		set32 $t2 100000
		qtext $a2 @Main
		set32 $a3 ?Main
		syscall 0 4
		syscall 0 95
		loop0:
			addi $t1 $t1 1
			blt $t1 $t2 2
		cja >loop0
	ret
