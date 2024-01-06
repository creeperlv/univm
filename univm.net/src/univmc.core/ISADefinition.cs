using System;
using System.Collections.Generic;
using System.IO;
using univm.core;
using univmc.core.Utilities;

namespace univmc.core
{
    public class ISADefinition
    {
        private static readonly Type TRegister = typeof(Register);
        private static readonly Type TUInt8 = typeof(byte);
        private static readonly Type TSingle = typeof(float);
        private static readonly Type TDouble = typeof(double);
        private static readonly Type TInt16 = typeof(short);
        private static readonly Type TInt32 = typeof(int);
        private static readonly Type TInt64 = typeof(Int64);
        private static readonly Type TSInt8 = typeof(sbyte);
        private static readonly Type TUInt16 = typeof(UInt16);
        private static readonly Type TUInt32 = typeof(UInt32);
        private static readonly Type TUInt64 = typeof(UInt64);
        public static Dictionary<string, Type?> knowntypes = new Dictionary<string, Type?>()
        {
            {"byte", TUInt8 },
            {"uint8", TUInt8 },
            {"short", TInt16 },
            {"int16", TInt16 },
            {"int", TInt32 },
            {"int32", TInt32 },
            {"int64", TInt64 },
            {"long", TInt64 },
            {"sbyte", TSInt8 },
            {"sint8", TSInt8 },
            {"ushort", TUInt16 },
            {"uint16", TUInt16 },
            {"uint", TUInt32 },
            {"uint32", TUInt32 },
            {"uint64", TUInt64 },
            {"ulong", TUInt64 },
            {"single", TSingle},
            {"double", TDouble},
            {"register", TRegister},
            {"reg", TRegister},
            {"null", null},
        };
        public static Dictionary<string, int> Sections = new Dictionary<string, int>()
        {
            {"opcode",0 },
            {"opcodes",0 },
            {"typedef",1 },
            {"def",1 },
            {"definition",1 },
        };
        public static ISADefinition Default = new ISADefinition
        {
            Operations = new Dictionary<string, uint>() {
            {"add", InstOPCodes.BASE_ADD },
            { "sub", InstOPCodes.BASE_SUB },
            { "mul", InstOPCodes.BASE_MUL },
            { "div", InstOPCodes.BASE_DIV },
            { "alloc", InstOPCodes.HL_ALLOC },
            { "realloc", InstOPCodes.HL_REALLOC },
            { "resize", InstOPCodes.HL_RRESIZE },
            { "expand", InstOPCodes.HL_EXPAND },
            { "expandi", InstOPCodes.HL_EXPANDI },
            { "shrink", InstOPCodes.HL_SHRINK },
            { "shrinki", InstOPCodes.HL_SHRINKI },
            { "free", InstOPCodes.HL_FREE },
            { "pushd", InstOPCodes.HL_PUSHD },
            { "pushdi", InstOPCodes.HL_PUSHDI },
            { "pushd8", InstOPCodes.HL_PUSHDD8 },
            { "pushd16", InstOPCodes.HL_PUSHDD16 },
            { "pushd32", InstOPCodes.HL_PUSHDD32 },
            { "pushd64", InstOPCodes.HL_PUSHDD64 },
            { "popd", InstOPCodes.HL_POPD },
            { "popdi", InstOPCodes.HL_POPDI },
            { "pp", InstOPCodes.HL_PP },
            { "ppi", InstOPCodes.HL_PPI },
            { "measure", InstOPCodes.HL_MEASURE },
            { "memlen", InstOPCodes.HL_MEASURE },
            { "cp", InstOPCodes.HL_CP },
            { "cpi", InstOPCodes.HL_CPI },
            { "loadasm", InstOPCodes.HL_LOADASM },
            { "loadasmf", InstOPCodes.HL_LOADASMF },
            { "loadasmi", InstOPCodes.HL_LOADASMI },
            { "call", InstOPCodes.BASE_CALL },
            { "callr", InstOPCodes.BASE_CALLR },
            { "calle", InstOPCodes.BASE_CALLE },
            { "caller", InstOPCodes.BASE_CALLER },
            { "set8", InstOPCodes.BASE_SET8 },
            { "set16", InstOPCodes.BASE_SET16 },
            { "set32", InstOPCodes.BASE_SET32 },
            { "set64", InstOPCodes.BASE_SET64 },
            { "sets8", InstOPCodes.BASE_SETS8 },
            { "setu16", InstOPCodes.BASE_SETU16 },
            { "setu32", InstOPCodes.BASE_SETU32 },
            { "setu64", InstOPCodes.BASE_SETU64 },
            { "sets", InstOPCodes.BASE_SETS }
                },
            Definitions = new Dictionary<uint, InstructionTypeDefinition>()
        {
            { InstOPCodes.BASE_ADD, new InstructionTypeDefinition(  TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_SUB, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_MUL, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_DIV, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_ADD_B, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_SUB_B, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_MUL_B, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_DIV_B, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_ADD_64, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_SUB_64, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_MUL_64, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.BASE_DIV_64, new InstructionTypeDefinition( TRegister, TRegister,TRegister ) },
            { InstOPCodes.HL_ALLOC, new InstructionTypeDefinition( TRegister, TRegister,null ) },
            { InstOPCodes.BASE_SYSCALL, new InstructionTypeDefinition( TUInt32, TUInt32,null ) },
            { InstOPCodes.BASE_SYSCALLR, new InstructionTypeDefinition( TRegister, TRegister,null ) },
            { InstOPCodes.BASE_SYSCALL_TEST, new InstructionTypeDefinition( TUInt32, TUInt32,null ) },
            { InstOPCodes.BASE_SYSCALL_TESTR, new InstructionTypeDefinition( TRegister, TRegister,null ) },
            { InstOPCodes.HL_FREE, new InstructionTypeDefinition( TRegister, null,null ) },
            { InstOPCodes.BASE_SETS, new InstructionTypeDefinition(  TRegister, TSingle,null) },
            { InstOPCodes.BASE_SETD, new InstructionTypeDefinition(  TRegister, TDouble,null) },
            { InstOPCodes.BASE_SET8, new InstructionTypeDefinition(  TRegister, TUInt8,null) },
            { InstOPCodes.BASE_SET16, new InstructionTypeDefinition(  TRegister, TInt16,null) },
            { InstOPCodes.BASE_SET32, new InstructionTypeDefinition(  TRegister, TInt32,null) },
            { InstOPCodes.BASE_SET64, new InstructionTypeDefinition(  TRegister, TInt64,null) },
            { InstOPCodes.BASE_SETS8, new InstructionTypeDefinition(  TRegister, TSInt8,null) },
            { InstOPCodes.BASE_SETU16, new InstructionTypeDefinition(  TRegister, TUInt16,null) },
            { InstOPCodes.BASE_SETU32, new InstructionTypeDefinition(  TRegister, TUInt32,null) },
            { InstOPCodes.BASE_SETU64, new InstructionTypeDefinition(  TRegister, TUInt64,null) },
            { InstOPCodes.BASE_CALL, new InstructionTypeDefinition( TUInt32, null,null ) },
            { InstOPCodes.BASE_CALLR, new InstructionTypeDefinition( TRegister, null,null ) },
            { InstOPCodes.BASE_CALLE, new InstructionTypeDefinition( TUInt32, TUInt32,null ) },
            { InstOPCodes.BASE_CALLER, new InstructionTypeDefinition( TRegister, TRegister,null ) },
        }
        };
        public Dictionary<string, uint> Operations = new Dictionary<string, uint>();
        public Dictionary<uint, InstructionTypeDefinition> Definitions = new Dictionary<uint, InstructionTypeDefinition>();
        public static ISADefinition LoadFromReader(TextReader treader)
        {
            ISADefinition definition = new ISADefinition();
            string? line;
            int Section = 0;
            while ((line = treader.ReadLine()) != null)
            {
                line = line.Trim().ToLower();
                if (line.EndsWith(":"))
                {
                    Sections.TryGetValue(line[0..^1], out Section);
                }
                else if (line.StartsWith("//") || line.StartsWith("#") || line.StartsWith(";"))
                {
                }
                else
                {

                    switch (Section)
                    {
                        case 0:
                            var segs = line.Split(' ', '\t');
                            try
                            {
                                if (segs[1].TryParse(out uint value))
                                {
                                    definition.Operations.Add(segs[0], value);
                                }
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        case 1:
                            segs = line.Split(' ', '\t');
                            try
                            {
                                uint op_code;
                                if (!definition.Operations.TryGetValue(segs[0], out op_code))
                                {
                                    try
                                    {
                                        if (!segs[0].TryParse(out op_code))
                                        {
                                            continue;
                                        }
                                    }
                                    catch (Exception)
                                    {
                                        continue;
                                    }
                                }
                                Type? T0 = null;
                                Type? T1 = null;
                                Type? T2 = null;
                                if (segs.Length >= 2)
                                    knowntypes.TryGetValue(segs[1], out T0);
                                if (segs.Length >= 3)
                                    knowntypes.TryGetValue(segs[2], out T1);
                                if (segs.Length >= 4)
                                    knowntypes.TryGetValue(segs[3], out T2);
                                definition.Definitions.Add(op_code, new InstructionTypeDefinition(T0, T1, T2));
                            }
                            catch (Exception)
                            {
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return definition;
        }
    }
}
