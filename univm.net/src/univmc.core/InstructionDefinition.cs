using System.Collections.Generic;
using univm.core;

namespace univmc.core
{
    public static class InstructionDefinition
    {
        private static readonly System.Type TRegister = typeof(Register);
        private static readonly System.Type TUInt32 = typeof(uint);
        public static Dictionary<uint, InstructionTypeDefinition> PredefinedInstructionWithType = new Dictionary<uint, InstructionTypeDefinition>()
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
            { InstOPCodes.BASE_CALL, new InstructionTypeDefinition( TUInt32, null,null ) },
            { InstOPCodes.BASE_CALLR, new InstructionTypeDefinition( TRegister, null,null ) },
            { InstOPCodes.BASE_CALLE, new InstructionTypeDefinition( TUInt32, TUInt32,null ) },
            { InstOPCodes.BASE_CALLER, new InstructionTypeDefinition( TRegister, TRegister,null ) },
        };
    }
}
