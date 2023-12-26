#!/bin/sh
Help(){
echo "This script can compile:"
echo "	univmc - Assembler"
echo "	univm - Generic VM"
echo "To customize build options, you can set environments."
echo "	CC: c compiler to use"
echo "	C_FLAGS: c compiler flags share among builds"
echo "	CORE_SRC: c files of vm core"
echo "	ASMC_SRC: c files of univmc"
echo "	ASMC_EXE: output file name of univmc"
echo "	VMRT_SRC: c files of univm runtime"
echo "	VMRT_EXE: output file name of univm runtime"
echo "	SKIP_ASMC: When defined, skip build assembler"
echo "	SKIP_VMRT: When defined, skip build univm runtime"
echo "	SKIP_COPYSAMPLES: When defined, skip copy samples"
echo "	OUTPUT_DIR: output folder"
}

echo "UNIVM Build Script"
while getopts ":h" option; do
   case $option in
      h) # display Help
         Help
         exit;;
   esac
done

if [ -z "$CC" ];
then
	CC=cc
fi

if [ -z "$CORE_SRC" ];
then
	CORE_SRC="./src/core/*.c"
fi

if [ -z "$ASMC_SRC" ];
then
	ASMC_SRC="./src/univmc/*.c"
fi

if [ -z "$VMRT_SRC" ];
then
	VMRT_SRC="./src/univm/*.c"
fi

if [ -z "$ASMC_EXE" ];
then
	ASMC_EXE="univmc"
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

INDEX=0

if [ -z "$SKIP_ASMC" ];
then
	COMPILE="$CC $C_FLAGS $CORE_SRC $ASMC_SRC -o $OUTPUT_DIR/$ASMC_EXE"
	echo "[$INDEX]$COMPILE"
	$COMPILE
fi
INDEX=$(($INDEX+1))
if [ -z "$SKIP_VMRT" ];
then
	COMPILE="$CC $C_FLAGS $CORE_SRC $VMRT_SRC -o $OUTPUT_DIR/$VMRT_EXE"
	echo "[$INDEX]$COMPILE"
	$COMPILE
fi
INDEX=$(($INDEX+1))
if [ -z "$SKIP_COPYSAMPLES" ];
then
	OUTPUT_SAMPLE="$OUTPUT_DIR/samples/"
	mkdir -p $OUTPUT_SAMPLE 
	CMD="cp -r ./samples/* $OUTPUT_SAMPLE"
	echo "[$INDEX]$CMD"
	$CMD
fi
