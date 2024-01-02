﻿namespace univm.core
{
    public static class InstOPCodes
    {
        public const uint BASE_ADD = 0x00000001;
        public const uint BASE_SUB = 0x00000002;
        public const uint BASE_MUL = 0x00000003;
        public const uint BASE_DIV = 0x00000004;

        public const uint BASE_ADD_S = 0x00000005;
        public const uint BASE_SUB_S = 0x00000006;
        public const uint BASE_MUL_S = 0x00000007;
        public const uint BASE_DIV_S = 0x00000008;
        public const uint BASE_ADD_D = 0x00000009;
        public const uint BASE_SUB_D = 0x0000000A;
        public const uint BASE_MUL_D = 0x0000000B;
        public const uint BASE_DIV_D = 0x0000000C;
        public const uint BASE_ADD_I = 0x0000000D;
        public const uint BASE_SUB_I = 0x0000000E;
        public const uint BASE_MUL_I = 0x0000000F;
        public const uint BASE_DIV_I = 0x00000010;
        public const uint BASE_ADD_IU = 0x00000011;
        public const uint BASE_SUB_IU = 0x00000012;
        public const uint BASE_MUL_IU = 0x00000013;
        public const uint BASE_DIV_IU = 0x00000014;
        public const uint BASE_ADD_U = 0x00000015;
        public const uint BASE_SUB_U = 0x00000016;
        public const uint BASE_MUL_U = 0x00000017;
        public const uint BASE_DIV_U = 0x00000018;
        public const uint BASE_ABS = 0x00000019;
        public const uint BASE_ABS_S = 0x00000020;
        public const uint BASE_ABS_D = 0x00000021;
        public const uint BASE_ABS_B = 0x00000022;
        public const uint BASE_ABS_64 = 0x00000023;
        public const uint BASE_ABS_16 = 0x00000024;

        //_B* series means byte wise operation.
        public const uint BASE_ADD_B = 0x00000025;
        public const uint BASE_SUB_B = 0x00000026;
        public const uint BASE_MUL_B = 0x00000027;
        public const uint BASE_DIV_B = 0x00000028;
        public const uint BASE_ADD_BI = 0x00000029;
        public const uint BASE_SUB_BI = 0x0000002A;
        public const uint BASE_MUL_BI = 0x0000002B;
        public const uint BASE_DIV_BI = 0x0000002C;
        public const uint BASE_ADD_BU = 0x0000002D;
        public const uint BASE_SUB_BU = 0x0000002E;
        public const uint BASE_MUL_BU = 0x0000002F;
        public const uint BASE_DIV_BU = 0x00000030;
        public const uint BASE_ADD_BIU = 0x00000031;
        public const uint BASE_SUB_BIU = 0x00000032;
        public const uint BASE_MUL_BIU = 0x00000033;
        public const uint BASE_DIV_BIU = 0x00000034;
        //_64* series means  64-bit data operation.
        //_64I can only process low 32 bit of data.
        public const uint BASE_ADD_64 = 0x00000035;
        public const uint BASE_SUB_64 = 0x00000036;
        public const uint BASE_MUL_64 = 0x00000037;
        public const uint BASE_DIV_64 = 0x00000038;
        public const uint BASE_ADD_64I = 0x00000039;
        public const uint BASE_SUB_64I = 0x0000003A;
        public const uint BASE_MUL_64I = 0x0000003B;
        public const uint BASE_DIV_64I = 0x0000003C;
        public const uint BASE_ADD_64U = 0x0000003D;
        public const uint BASE_SUB_64U = 0x0000003E;
        public const uint BASE_MUL_64U = 0x0000003F;
        public const uint BASE_DIV_64U = 0x00000040;
        public const uint BASE_ADD_64IU = 0x00000041;
        public const uint BASE_SUB_64IU = 0x00000042;
        public const uint BASE_MUL_64IU = 0x00000043;
        public const uint BASE_DIV_64IU = 0x00000044;

        // lw/sw $rs/rd $rptr(64bit)
        // lw/sw series:
        // op $data_ptr $mem_ptr_reg offset
        //e.g.: sw $t3 $sp -4
        //e.g.: lw $t3 $sp -4
        public const uint BASE_SW = 0x00001000;
        public const uint BASE_S32 = 0x00001000;
        public const uint BASE_LW = 0x00001001;
        public const uint BASE_L32 = 0x00001001;
        public const uint BASE_SB = 0x00001002;
        public const uint BASE_S8 = 0x00001002;
        public const uint BASE_LB = 0x00001003;
        public const uint BASE_L8 = 0x00001003;
        public const uint BASE_SH = 0x00001004;
        public const uint BASE_S16 = 0x00001004;
        public const uint BASE_LH = 0x00001005;
        public const uint BASE_L16 = 0x00001005;

        public const uint BASE_SD = 0x00001006;
        public const uint BASE_S64 = 0x00001006;
        public const uint BASE_LD = 0x00001007;
        public const uint BASE_L64 = 0x00001007;
        public const uint BASE_SET8 = 0x00001008;
        public const uint BASE_SET16 = 0x00001009;
        public const uint BASE_SET32 = 0x0000100A;
        public const uint BASE_SET64 = 0x0000100B;

        public const uint BASE_CALL = 0x00001100;
        public const uint BASE_CALLI = 0x00001101;
        public const uint BASE_RET = 0x00001102;
        public const uint BASE_SPADD = 0x00001103;
        public const uint BASE_GETSPLEN = 0x00001104;
        public const uint BASE_SETSPLEN = 0x00001104;
        // Jump to relative address
        public const uint BASE_J = 0x00001110;
        public const uint BASE_JR = 0x00001111;
        // Jump And Link
        // JAL $RD Offset
        public const uint BASE_JAL = 0x00001112;
        public const uint BASE_JALR = 0x00001113;
        // Jump to absolute address
        public const uint BASE_JA = 0x00001114;
        public const uint BASE_JAR = 0x00001115;
        // Jump to Absolute address And Link
        public const uint BASE_JAAL = 0x00001116;
        public const uint BASE_JAALR = 0x00001117;

        public const uint BASE_BEQ = 0x00001118;
        public const uint BASE_BEQI = 0x00001119;

        public const uint BASE_BLT = 0x0000111A;
        public const uint BASE_BLTI = 0x0000111B;
        public const uint BASE_BGT = 0x0000111C;
        public const uint BASE_BGTI = 0x0000111D;

        // ALLOC = malloc(size_t)
        // alloc $rd(64bit) $rs
        public const uint HL_ALLOC = 0x00002000;
        // Resize
        // realloc $rd $rs(64bit) $rt
        public const uint HL_REALLOC = 0x00002001;
        // Relative Resize
        // rresize $rd(64bit) $rs
        public const uint HL_RRESIZE = 0x00002002;
        // Expand a memory allocation
        public const uint HL_EXPAND = 0x00002003;
        // Shrink a memory allocation
        // shrink $rs $value
        public const uint HL_SHRINK = 0x00002004;
        public const uint HL_EXPANDI = 0x00002005;
        public const uint HL_SHRINKI = 0x00002006;
        // Query Data From Text Section
        // qtext $rd(64-bit) $rs
        public const uint HL_QTEXT = 0x00002007;
        // Query Data From Text Section of a assembly
        // qtexta $register_to_store_mem $assemblyID $textID
        public const uint HL_QTEXTA = 0x00002008;
        // measure $mem_ptr_reg $register_to_recieve_size
        public const uint HL_MEASURE = 0x00002009;
        // Free a memptr
        public const uint HL_FREE = 0x0000200A;
        // Push a data to end of a memory.
        // PUSHD $SRC_REG $MEM_PTR_REG $SIZE_REG
        public const uint HL_PUSHD = 0x0000_2100;
        // Push a data to end of a memory
        // PUSHDI $SRC_REG $MEM_PTR_REG Size
        public const uint HL_PUSHDI = 0x0000_2101;
        // Push direct data 8
        // pushdd8 $MemPtrReg DirectValue
        public const uint HL_PUSHDD8 = 0x0000_2110;
        // Push direct data 16
        // pushdd8 $MemPtrReg DirectValue
        public const uint HL_PUSHDD16 = 0x0000_2111;
        // Push direct data 32
        // pushdd8 $MemPtrReg DirectValue
        public const uint HL_PUSHDD32 = 0x0000_2112;
        // Push direct data 64
        // pushdd8 $MemPtrReg DirectValue
        public const uint HL_PUSHDD64 = 0x0000_2113;

        // Pop a data out
        public const uint HL_POPD = 0x0000_2102;
        public const uint HL_POPDI = 0x0000_2103;

        // Pop and push
        // PP $SRC_PTR_REGISTER $DEST_PTR_REGISTER $SIZE
        public const uint HL_PP = 0x0000_2104;
        // Pop and push immediate
        // PPI $SRC_PTR_REGISTER $DEST_PTR_REGISTER Size
        public const uint HL_PPI = 0x0000_2105;

        // Copy and push
        // cp $SrcMemPtr $DestMemPtr $SizePtr
        public const uint HL_CP = 0x0000_2106;
        // Copy and push
        // cp $SrcMemPtr $DestMemPtr Size
        public const uint HL_CPI = 0x0000_2107;

        // Get the mapped global memory address of current assembly.
        // mapglbmem $reg_to_store_memptr
        public const uint HL_MAP_GLBMEM = 0x0000_2200;
        // Get the mapped global memory address of a assembly.
        // mapaglbmem $reg_to_store_memptr $AssemblyID
        public const uint HL_MAP_AGLBMEM = 0x0000_2201;
        //mapaglbmemi $reg_to_store_memptr AssemblyID
        public const uint HL_MAP_AGLBMEMI = 0x0000_2202;
        // Load assembly (Memory will be copied)
        // loadasm $MemPtr $Size
        public const uint HL_LOADASM = 0x0000_2210;
        // Load Assembly (Memory will be copied)
        // loadasm $MemPtr Size
        public const uint HL_LOADASMI = 0x0000_2211;
        // Load Assembly from a file
        // loadasmf $ResourceID
        public const uint HL_LOADASMF = 0x0000_2212;

        // syscalltest namespace id $reciver
        public const uint BASE_SYSCALL_TEST = 0x0000_1200;
        public const uint BASE_SYSCALL_TESTR = 0x0000_1201;
        // syscall namespace id 
        public const uint BASE_SYSCALL = 0x00001202;
        // syscallr $namespace $id 
        public const uint BASE_SYSCALLR = 0x00001203;
    }
}