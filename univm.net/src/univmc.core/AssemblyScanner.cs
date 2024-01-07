using LibCLCC.NET.TextProcessing;
using System.Collections.Generic;

namespace univmc.core
{
    public class AssemblyScanner : GeneralPurposeScanner
    {
        public AssemblyScanner()
        {
            this.lineCommentIdentifiers = new List<LineCommentIdentifier> {
                new LineCommentIdentifier() { StartSequence = ";" },
                new LineCommentIdentifier() { StartSequence = "#" },
                new LineCommentIdentifier() { StartSequence = "//" },
            };
            this.PredefinedSegmentCharacters = new List<char> { ':' };
            this.Splitters = new char[] { ';', ' ', '\t', '\r', '\n' };
        }
    }
}
