#ifndef UNIVM_INST
#define UNIVM_INST

#define UNIVM_ASM_MAJOR_VERSION 1
#define UNIVM_ASM_MINOR_VERSION 0

// Universal Virtual Machine Instruction Set V 1.0

// Pointer: unsigned 64-bit integer
//		First 32 bit: mem id
//		Second 32 bit: offset
#define NOP 0xFFFFFFFF

#define BSAE_ADD 0x00000001
#define BSAE_SUB 0x00000002
#define BSAE_MUL 0x00000003
#define BSAE_DIV 0x00000004

#define BSAE_ADD_S 0x00000005
#define BSAE_SUB_S 0x00000006
#define BSAE_MUL_S 0x00000007
#define BSAE_DIV_S 0x00000008

#define BSAE_ADD_D 0x00000009
#define BSAE_SUB_D 0x0000000A
#define BSAE_MUL_D 0x0000000B
#define BSAE_DIV_D 0x0000000C

#define BSAE_ADD_I 0x0000000D
#define BSAE_SUB_I 0x0000000E
#define BSAE_MUL_I 0x0000000F
#define BSAE_DIV_I 0x00000010

#define BSAE_ADD_IU 0x00000011
#define BSAE_SUB_IU 0x00000012
#define BSAE_MUL_IU 0x00000013
#define BSAE_DIV_IU 0x00000014

#define BSAE_ADD_U 0x00000015
#define BSAE_SUB_U 0x00000016
#define BSAE_MUL_U 0x00000017
#define BSAE_DIV_U 0x00000018

#define BSAE_ABS 0x00000019
#define BSAE_ABS_S 0x0000001A
#define BSAE_ABS_D 0x0000001B
#define BSAE_ABS_B 0x0000001C
#define BSAE_ABS_16 0x0000001D
#define BSAE_ABS_64 0x0000001E

#define BASE_ADD_B 0x0000001F
#define BASE_SUB_B 0x00000020
#define BASE_MUL_B 0x00000021
#define BASE_DIV_B 0x00000022
#define BASE_ADD_BI 0x00000023
#define BASE_SUB_BI 0x00000024
#define BASE_MUL_BI 0x00000025
#define BASE_DIV_BI 0x00000026
#define BASE_ADD_BU 0x00000027
#define BASE_SUB_BU 0x00000028
#define BASE_MUL_BU 0x00000029
#define BASE_DIV_BU 0x0000002A
#define BASE_ADD_BIU 0x0000002B
#define BASE_SUB_BIU 0x0000002C
#define BASE_MUL_BIU 0x0000002D
#define BASE_DIV_BIU 0x0000002E
#define BASE_ADD_64 0x0000002F
#define BASE_SUB_64 0x00000030
#define BASE_MUL_64 0x00000031
#define BASE_DIV_64 0x00000032
#define BASE_ADD_64I 0x00000033
#define BASE_SUB_64I 0x00000034
#define BASE_MUL_64I 0x00000035
#define BASE_DIV_64I 0x00000036
#define BASE_ADD_64U 0x00000037
#define BASE_SUB_64U 0x00000038
#define BASE_MUL_64U 0x00000039
#define BASE_DIV_64U 0x0000003A
#define BASE_ADD_64IU 0x0000003B
#define BASE_SUB_64IU 0x0000003C
#define BASE_MUL_64IU 0x0000003D
#define BASE_DIV_64IU 0x0000003E
#define BASE_ADD_16 0x0000003F
#define BASE_SUB_16 0x00000040
#define BASE_MUL_16 0x00000041
#define BASE_DIV_16 0x00000042
#define BASE_ADD_16I 0x00000043
#define BASE_SUB_16I 0x00000044
#define BASE_MUL_16I 0x00000045
#define BASE_DIV_16I 0x00000046
#define BASE_ADD_16U 0x00000047
#define BASE_SUB_16U 0x00000048
#define BASE_MUL_16U 0x00000049
#define BASE_DIV_16U 0x0000004A
#define BASE_ADD_16IU 0x0000004B
#define BASE_SUB_16IU 0x0000004C
#define BASE_MUL_16IU 0x0000004D
#define BASE_DIV_16IU 0x0000004E

// lw/sw $rs/rd $rptr(64bit)
#define BSAE_SW 0x00001000
#define BSAE_S32 0x00001000
#define BSAE_LW 0x00001001
#define BSAE_L32 0x00001001

#define BSAE_SB 0x00001002
#define BSAE_S8 0x00001002
#define BSAE_LB 0x00001003
#define BSAE_L8 0x00001003

#define BSAE_SH 0x00001004
#define BSAE_S16 0x00001004
#define BSAE_LH 0x00001005
#define BSAE_L16 0x00001005

#define BASE_SD 0x00001006
#define BASE_S64 0x00001006
#define BASE_LD 0x00001007
#define BASE_L64 0x00001007
#define BASE_SETS8 0x0000100C
#define BASE_SET16 0x00001009
#define BASE_SET32 0x0000100A
#define BASE_SET64 0x0000100B
#define BASE_SET8 0x00001008
#define BASE_SETU16 0x0000100D
#define BASE_SETU32 0x0000100E
#define BASE_SETU64 0x0000100F
#define BASE_SETS 0x00001010
#define BASE_SETD 0x00001011

#define BSAE_SD 0x00001006
#define BSAE_S64 0x00001006
#define BSAE_LD 0x00001007
#define BSAE_L64 0x00001007

#define BASE_CALL 0x00001100
#define BASE_CALLR 0x00001101
#define BASE_RET 0x00001102

#define BASE_SPADD 0x00001103
#define BASE_GETSPLEN 0x00001104
#define BASE_SETSPLEN 0x00001105
#define BASE_CALLE 0x00001106
#define BASE_CALLER 0x00001107

// Jump to relative address
#define BASE_J 0x00001110
#define BASE_JR 0x00001111
// Jump And Link
// JAL $RD Offset
#define BASE_JAL 0x00001112
#define BASE_JALR 0x00001113
// Jump to absolute address
#define BASE_JA 0x00001114
#define BASE_JAR 0x00001115
// Jump to Absolute address And Link
#define BASE_JAAL 0x00001116
#define BASE_JAALR 0x00001117

#define BASE_BEQ 0x00001118
#define BASE_BEQI 0x00001119

#define BASE_BLT 0x0000111A
#define BASE_BLTI 0x0000111B
#define BASE_BGT 0x0000111C
#define BASE_BGTI 0x0000111D

// ALLOC = malloc(size_t)
// alloc $rd(64bit) $rs
#define HL_ALLOC 0x00002000
// Resize
// realloc $rd $rs(64bit) $rt
#define HL_REALLOC 0x00002001
// Relative Resize
// rresize $rd(64bit) $rs
#define HL_RRESIZE 0x00002002

// Expand a memory allocation
#define HL_EXPAND 0x00002003
// Shrink a memory allocation
// shrink $rs $value
#define HL_SHRINK 0x00002004
#define HL_EXPANDI 0x00002005
#define HL_SHRINKI 0x00002006
// Query Data From Text Section
// qtext $rd(64-bit) $rs
#define HL_QTEXT 0x00002007
#define HL_QTEXTA 0x00002008

// Push a data to end of a memory.
// PUSHD $SRC_REG $MEM_PTR_REG $SIZE_REG
#define HL_PUSHD 0x00002100
// Push a data to end of a memory
// PUSHDI $SRC_REG $MEM_PTR_REG Size
#define HL_PUSHDI 0x00002101
// Pop a data out
#define HL_POPD 0x00002102
#define HL_POPDI 0x00002103

// Push direct data 8
// pushdd8 $MemPtrReg DirectValue
#define HL_PUSHDD8 0x00002110
// Push direct data 16
// pushdd8 $MemPtrReg DirectValue
#define HL_PUSHDD16 0x00002111
// Push direct data 32
// pushdd8 $MemPtrReg DirectValue
#define HL_PUSHDD32 0x00002112
// Push direct data 64
// pushdd8 $MemPtrReg DirectValue
#define HL_PUSHDD64 0x00002113
// Pop and push
// PP $SRC_PTR_REGISTER $DEST_PTR_REGISTER $SIZE
#define HL_PP 0x00002104
// Pop and push immediate
// PPI $SRC_PTR_REGISTER $DEST_PTR_REGISTER Size
#define HL_PPI 0x00002105

// Copy and push
// cp $SrcMemPtr $DestMemPtr $SizePtr
#define HL_CP 0x00002106
// Copy and push
// cp $SrcMemPtr $DestMemPtr Size
#define HL_CPI 0x00002107

// Get the mapped global memory address of current assembly.
// mapglbmem $reg_to_store_memptr
#define HL_MAP_GLBMEM 0x00002200
// Get the mapped global memory address of a assembly.
// mapaglbmem $reg_to_store_memptr $AssemblyID
#define HL_MAP_AGLBMEM 0x00002201
// mapaglbmemi $reg_to_store_memptr AssemblyID
#define HL_MAP_AGLBMEMI 0x00002202
// Load assembly (Memory will be copied)
// loadasm $MemPtr $Size
#define HL_LOADASM 0x00002210
// Load Assembly (Memory will be copied)
// loadasm $MemPtr Size
#define HL_LOADASMI 0x00002211
// Load Assembly from a file
// loadasmf $ResourceID
#define HL_LOADASMF 0x00002212

// syscalltest namespace id
#define BASE_SYSCALL_TEST 0x00001200
// syscall namespace id
#define BSAE_SYSCALL 0x00001201
// syscallr $namespace $id
#define BSAE_SYSCALLR 0x00001202

#define RegisterOffset_00 0x00
#define RegisterOffset_01 0x08
#define RegisterOffset_02 0x10
#define RegisterOffset_03 0x18
#define RegisterOffset_04 0x20
#define RegisterOffset_05 0x28
#define RegisterOffset_06 0x30
#define RegisterOffset_07 0x38
#define RegisterOffset_08 0x40
#define RegisterOffset_09 0x48
#define RegisterOffset_10 0x50
#define RegisterOffset_11 0x58
#define RegisterOffset_12 0x60
#define RegisterOffset_13 0x68
#define RegisterOffset_14 0x70
#define RegisterOffset_15 0x78
#define RegisterOffset_16 0x80
#define RegisterOffset_17 0x88
#define RegisterOffset_18 0x90
#define RegisterOffset_19 0x98
#define RegisterOffset_20 0xA0
#define RegisterOffset_21 0xA8
#define RegisterOffset_22 0xB0
#define RegisterOffset_23 0xB8
#define RegisterOffset_24 0xC0
#define RegisterOffset_25 0xC8
#define RegisterOffset_26 0xD0
#define RegisterOffset_27 0xD8
#define RegisterOffset_28 0xE0
#define RegisterOffset_29 0xE8
#define RegisterOffset_30 0xF0
#define RegisterOffset_31 0xF8
#define RegisterOffset_32 0x100
#define RegisterOffset_33 0x108
#define RegisterOffset_34 0x110
#define RegisterOffset_35 0x118
#define RegisterOffset_36 0x120
#define RegisterOffset_37 0x128
#define RegisterOffset_38 0x130
#define RegisterOffset_39 0x138
#define RegisterOffset_40 0x140
#define RegisterOffset_41 0x148
#define RegisterOffset_42 0x150
#define RegisterOffset_43 0x158
#define RegisterOffset_44 0x160
#define RegisterOffset_45 0x168
#define RegisterOffset_46 0x170
#define RegisterOffset_47 0x178
#define RegisterOffset_48 0x180
#define RegisterOffset_49 0x188
#define RegisterOffset_50 0x190
#define RegisterOffset_51 0x198
#define RegisterOffset_52 0x1A0
#define RegisterOffset_53 0x1A8
#define RegisterOffset_54 0x1B0
#define RegisterOffset_55 0x1B8
#define RegisterOffset_56 0x1C0
#define RegisterOffset_57 0x1C8
#define RegisterOffset_58 0x1D0
#define RegisterOffset_59 0x1D8
#define RegisterOffset_60 0x1E0
#define RegisterOffset_61 0x1E8
#define RegisterOffset_62 0x1F0
#define RegisterOffset_63 0x1F8

#endif
