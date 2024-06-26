#!/bin/sh
Help(){
	echo "This script can compile:"
	echo "	univmc - Assembler"
	echo "	univm - Generic VM"
	echo "To customize build options, you can set environments."
	echo "	CC: c compiler to use"
	echo "	C_FLAGS: c compiler flags share among builds"
	echo "	CORE_SRC: c files of vm core"
	echo "	VMRT_SRC: c files of univm runtime"
	echo "	VMRT_EXE: output file name of univm runtime"
	echo "	SKIP_VMRT: When defined, skip build univm runtime"
	echo "	SKIP_COPYSAMPLES: When defined, skip copy samples"
	echo "	OUTPUT_DIR: output folder"
}

echo "UniVM Build Script"
while getopts ":h" option; do
   case $option in
      h) # display Help
         Help
         exit 0
   esac
done

if [ -z "$CC" ];
then
	CC=cc
fi

if [ -z "$CORE_SRC" ];
then
	CORE_SRC="./src/core/*.c ./src/core/dispatch/*.c"
fi

if [ -z "$FUNC_TEST_SRC" ];
then
	FUNC_TEST_SRC="./src/functest/*.c"
fi

if [ -z "$VMRT_SRC" ];
then
	VMRT_SRC="./src/univm/*.c"
fi

if [ -z "$FUNC_TEST_EXE" ];
then
	FUNC_TEST_EXE="functest"
fi
if [ -z "$VMRT_EXE" ];
then
	VMRT_EXE="univm"
fi

if [ -z "$C_FLAGS" ];
then
	C_FLAGS="-o3 -std=c99"
fi

if [ -z "$OUTPUT_DIR" ];
then
	OUTPUT_DIR="./bin"
fi
C_FLAGS="$C_FLAGS -lpthread"
INDEX=0
mkdir $OUTPUT_DIR -p
if [ -z "$SKIP_FUNC_TEST" ];
then
	COMPILE="$CC $C_FLAGS $CORE_SRC $FUNC_TEST_SRC -o $OUTPUT_DIR/$FUNC_TEST_EXE"
	echo "[$INDEX]$COMPILE"
	$COMPILE
	if [  $? != 0 ];
	then 
		echo "\e[91mCompilation Failed!\e[0m"
		exit 1
	fi
	INDEX=$(($INDEX+1))
	export NO_OUTPUT=1 
	FUNCTEST_EXEC="$OUTPUT_DIR/$FUNC_TEST_EXE "
	echo "[$INDEX]Start Test:$FUNCTEST_EXEC"
	$FUNCTEST_EXEC
	if [ $?  != 0 ] ;
	then
		echo "\e[91mFunction Test Failed!\e[0m"
		exit 1
	fi
	
fi
INDEX=$(($INDEX+1))
if [ -z "$SKIP_VMRT" ];
then
	COMPILE="$CC $C_FLAGS $CORE_SRC $VMRT_SRC -o $OUTPUT_DIR/$VMRT_EXE"
	echo "[$INDEX]$COMPILE"
	$COMPILE
	if [  $? != 0 ];
	then 
		echo "\e[91mCompilation Failed!\e[0m"
		exit 1
	fi
fi
INDEX=$(($INDEX+1))
if [ -z "$SKIP_COPYSAMPLES" ];
then
	OUTPUT_SAMPLE="$OUTPUT_DIR/samples/"
	mkdir -p $OUTPUT_SAMPLE 
	CMD="cp -r ../samples/* $OUTPUT_SAMPLE"
	echo "[$INDEX]$CMD"
	$CMD
fi
