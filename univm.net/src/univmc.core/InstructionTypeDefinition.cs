using System;

namespace univmc.core
{
    public class InstructionTypeDefinition
    {
        public Type? Data0Type;
        public Type? Data1Type;
        public Type? Data2Type;

        public InstructionTypeDefinition(Type? data0Type, Type? data1Type, Type? data2Type)
        {
            Data0Type = data0Type;
            Data1Type = data1Type;
            Data2Type = data2Type;
        }
    }
}
