using System.Collections.Generic;
using univm.core;

namespace univmc.core
{
    public static class Keywords
    {
        public static Dictionary<string, PrepLabel> PrepLabels = new Dictionary<string, PrepLabel>()
        {
            { "library",PrepLabel.library },
            { "lib",PrepLabel.library },
            { "l",PrepLabel.library },
            { "include",PrepLabel.include },
            { "inc",PrepLabel.include },
            { "i",PrepLabel.include },
        };
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
            {".prep", Section.Prep},
            {".preparation", Section.Prep},
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
            {"mul", InstOPCodes.BASE_MUL},
            {"div", InstOPCodes.BASE_DIV},
            {"alloc", InstOPCodes.HL_ALLOC},
            {"realloc", InstOPCodes.HL_REALLOC},
            {"resize", InstOPCodes.HL_RRESIZE},
            {"expand", InstOPCodes.HL_EXPAND},
            {"expandi", InstOPCodes.HL_EXPANDI},
            {"shrink", InstOPCodes.HL_SHRINK},
            {"shrinki", InstOPCodes.HL_SHRINKI},
            {"free", InstOPCodes.HL_FREE},
            {"pushd", InstOPCodes.HL_PUSHD},
            {"pushdi", InstOPCodes.HL_PUSHDI},
            {"pushd8", InstOPCodes.HL_PUSHDD8},
            {"pushd16", InstOPCodes.HL_PUSHDD16},
            {"pushd32", InstOPCodes.HL_PUSHDD32},
            {"pushd64", InstOPCodes.HL_PUSHDD64},
            {"popd", InstOPCodes.HL_POPD},
            {"popdi", InstOPCodes.HL_POPDI},
            {"pp", InstOPCodes.HL_PP},
            {"ppi", InstOPCodes.HL_PPI},
            {"measure", InstOPCodes.HL_MEASURE},
            {"memlen", InstOPCodes.HL_MEASURE},
            {"cp", InstOPCodes.HL_CP},
            {"cpi", InstOPCodes.HL_CPI},
            {"loadasm", InstOPCodes.HL_LOADASM},
            {"loadasmf", InstOPCodes.HL_LOADASMF},
            {"loadasmi", InstOPCodes.HL_LOADASMI},
            {"call", InstOPCodes.BASE_CALL },
            {"callr", InstOPCodes.BASE_CALLR },
            {"calle", InstOPCodes.BASE_CALLE },
            {"caller", InstOPCodes.BASE_CALLER },
        };
    }
}
