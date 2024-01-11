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
            { "global",PrepLabel.global},
            { "glb",PrepLabel.global},
            { "g",PrepLabel.global},
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
            {"a4", RegisterDefinition.A4 },
            {"sp", RegisterDefinition.SP },
            {"spoffset", RegisterDefinition.SPOffset},
        };
    }
}
