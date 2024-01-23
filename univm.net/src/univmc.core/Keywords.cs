using System.Collections.Generic;
using univm.core;

namespace univmc.core
{
    public static class Keywords
    {
        public static Dictionary<string, PrepLabel> PrepLabels = new Dictionary<string, PrepLabel>()
        {
            { "library",PrepLabel.library },
            { ".library",PrepLabel.library },
            { "lib",PrepLabel.library },
            { ".lib",PrepLabel.library },
            { "l",PrepLabel.library },
            { ".l",PrepLabel.library },
            { ".include",PrepLabel.include },
            { "include",PrepLabel.include },
            { "inc",PrepLabel.include },
            { ".inc",PrepLabel.include },
            { "i",PrepLabel.include },
            { ".i",PrepLabel.include },
            { ".global",PrepLabel.global},
            { "global",PrepLabel.global},
            { "glb",PrepLabel.global},
            { ".glb",PrepLabel.global},
            { "g",PrepLabel.global},
            { ".g",PrepLabel.global},
            { "expose",PrepLabel.expose},
            { ".expose",PrepLabel.expose},
            { "exp",PrepLabel.expose},
            { ".exp",PrepLabel.expose},
            { "e",PrepLabel.expose},
            { ".e",PrepLabel.expose},
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
            {"t0", RegisterDefinition.T0 },
            {"t1", RegisterDefinition.T1 },
            {"t2", RegisterDefinition.T2 },
            {"t3", RegisterDefinition.T3 },
            {"t4", RegisterDefinition.T4 },
            {"t5", RegisterDefinition.T5 },
            {"t6", RegisterDefinition.T6 },
            {"s0", RegisterDefinition.S0 },
            {"s1", RegisterDefinition.S1 },
            {"s2", RegisterDefinition.S2 },
            {"s3", RegisterDefinition.S3 },
            {"s4", RegisterDefinition.S4 },
            {"s5", RegisterDefinition.S5 },
            {"s6", RegisterDefinition.S6 },
            {"s7", RegisterDefinition.S7 },
            {"s8", RegisterDefinition.S8 },
            {"s9", RegisterDefinition.S9 },
            {"s10", RegisterDefinition.S10 },
            {"s11", RegisterDefinition.S11 },
            {"a0", RegisterDefinition.A0 },
            {"a1", RegisterDefinition.A1 },
            {"a2", RegisterDefinition.A2 },
            {"a3", RegisterDefinition.A3 },
            {"a4", RegisterDefinition.A4 },
            {"a5", RegisterDefinition.A5 },
            {"a6", RegisterDefinition.A6 },
            {"a7", RegisterDefinition.A7 },
            {"sp", RegisterDefinition.SP },
            {"spoffset", RegisterDefinition.SPOffset},
        };
    }
}
