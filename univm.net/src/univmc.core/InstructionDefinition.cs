using System;
using System.Collections.Generic;
using univm.core;

namespace univmc.core
{
    internal  class InstructionDefinition
    {
        private static readonly Type TRegister = typeof(Register);
        private static readonly Type TUInt32 = typeof(uint);
        private static readonly Type TInt32 = typeof(int);
        private static readonly Type TInt16 = typeof(Int16);
        private static readonly Type TInt64 = typeof(Int64);
        private static readonly Type TByte = typeof(byte);
        private static readonly Type TSByte = typeof(sbyte);
        private static readonly Type TUInt64 = typeof(UInt64);
        private static readonly Type TUInt16 = typeof(UInt16);
        private static readonly Type TSingle = typeof(float);
        private static readonly Type TDouble = typeof(double);
        private static Dictionary<uint, InstructionTypeDefinition> PredefinedInstructionWithType = new Dictionary<uint, InstructionTypeDefinition>()
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
            { InstOPCodes.BASE_SET8, new InstructionTypeDefinition(  TRegister, TByte,null) },
            { InstOPCodes.BASE_SET16, new InstructionTypeDefinition(  TRegister, TInt16,null) },
            { InstOPCodes.BASE_SET32, new InstructionTypeDefinition(  TRegister, TInt32,null) },
            { InstOPCodes.BASE_SET64, new InstructionTypeDefinition(  TRegister, TInt64,null) },
            { InstOPCodes.BASE_SETS8, new InstructionTypeDefinition(  TRegister, TSByte,null) },
            { InstOPCodes.BASE_SETU16, new InstructionTypeDefinition(  TRegister, TUInt16,null) },
            { InstOPCodes.BASE_SETU32, new InstructionTypeDefinition(  TRegister, TUInt32,null) },
            { InstOPCodes.BASE_SETU64, new InstructionTypeDefinition(  TRegister, TUInt64,null) },
            { InstOPCodes.BASE_CALL, new InstructionTypeDefinition( TUInt32, null,null ) },
            { InstOPCodes.BASE_CALLR, new InstructionTypeDefinition( TRegister, null,null ) },
            { InstOPCodes.BASE_CALLE, new InstructionTypeDefinition( TUInt32, TUInt32,null ) },
            { InstOPCodes.BASE_CALLER, new InstructionTypeDefinition( TRegister, TRegister,null ) },
        };
    }
}
