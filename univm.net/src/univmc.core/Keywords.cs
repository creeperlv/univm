using System.Collections.Generic;
using univm.core;

namespace univmc.core
{
    public static class Keywords
    {
        public static Dictionary<string, Section> SectionNames = new Dictionary<string, Section>()
        {
            {".text", Section.Text },
            {".constants", Section.Constants},
            {".defines", Section.Constants},
            {".define", Section.Constants},
            {".definition", Section.Constants},
            {".program", Section.Program},
            {".code", Section.Program},
            {".codes", Section.Program},
        };
        public static Dictionary<string, uint> RegisterNames = new Dictionary<string, uint>
        {
            {"a0", RegisterDefinition.A0 },
            {"a1", RegisterDefinition.A1 },
            {"a2", RegisterDefinition.A2 },
            {"a3", RegisterDefinition.A3 },
            {"sp", RegisterDefinition.SP },
            {"spoffset", RegisterDefinition.SPOffset},
        };
        public static Dictionary<string, uint> InstructionNames = new Dictionary<string, uint>()
        {

            {"add", InstOPCodes.BASE_ADD },
            {"sub", InstOPCodes.BASE_SUB },
        };
    }
}
