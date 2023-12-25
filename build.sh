#!/bin/sh
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
