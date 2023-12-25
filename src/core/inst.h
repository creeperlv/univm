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
#define BSAE_ABS_S 0x00000020
#define BSAE_ABS_D 0x00000021
#define BSAE_ABS_B 0x00000022
#define BSAE_ABS_64 0x00000023
#define BSAE_ABS_16 0x00000024

//_B* series means byte wise operation.
#define BSAE_ADD_B 0x00000025
#define BSAE_SUB_B 0x00000026
#define BSAE_MUL_B 0x00000027
#define BSAE_DIV_B 0x00000028

#define BSAE_ADD_BI 0x00000029
#define BSAE_SUB_BI 0x0000002A
#define BSAE_MUL_BI 0x0000002B
#define BSAE_DIV_BI 0x0000002C

#define BSAE_ADD_BU 0x0000002D
#define BSAE_SUB_BU 0x0000002E
#define BSAE_MUL_BU 0x0000002F
#define BSAE_DIV_BU 0x00000030

#define BSAE_ADD_BIU 0x00000031
#define BSAE_SUB_BIU 0x00000032
#define BSAE_MUL_BIU 0x00000033
#define BSAE_DIV_BIU 0x00000034
//_64* series means  64-bit data operation.
#define BSAE_ADD_64 0x00000035
#define BSAE_SUB_64 0x00000036
#define BSAE_MUL_64 0x00000037
#define BSAE_DIV_64 0x00000038

#define BSAE_ADD_64I 0x00000039
#define BSAE_SUB_64I 0x0000003A
#define BSAE_MUL_64I 0x0000003B
#define BSAE_DIV_64I 0x0000003C

#define BSAE_ADD_64U 0x0000003D
#define BSAE_SUB_64U 0x0000003E
#define BSAE_MUL_64U 0x0000003F
#define BSAE_DIV_64U 0x00000040

#define BSAE_ADD_64IU 0x00000041
#define BSAE_SUB_64IU 0x00000042
#define BSAE_MUL_64IU 0x00000043
#define BSAE_DIV_64IU 0x00000044

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

#define BSAE_SD 0x00001006
#define BSAE_S64 0x00001006
#define BSAE_LD 0x00001007
#define BSAE_L64 0x00001007
// ALLOC = malloc(size_t)
// alloc $rd(64bit) $rs
#define HL_ALLOC 0x00002000
// Resize
// realloc $rd $rs(64bit) $rt
#define HL_REALLOC 0x00002001
// Relative Resize
// rresize $rd(64bit) $rs
#define HL_RRESIZE 0x00002002
// Query Data From Text Section
// qtext $rd(64-bit) $rs
#define HL_QUERY_TEXT 0x00002003

#define BSAE_SYSCALL 0x00000000

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
