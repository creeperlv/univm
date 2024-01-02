using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using univm.core;

namespace univmc.core
{
    public class AssembleDef
    {
        public Dictionary<string, uint> Labels = new Dictionary<string, uint>();
        public Dictionary<string, string> Texts = new Dictionary<string, string>();
    }
    public class CompileTimeData
    {
        public IntermediateUniAssembly? IntermediateUniAssembly = null;
        public UniVMAssembly? Artifact = null;
        public AssembleDef? ArtifaceDef = null;
        public List<AssembleDef> AssembleDefs = new List<AssembleDef>();
        public List<UniVMAssembly?> LoadedAssemblies = new List<UniVMAssembly?>();
    }
    public class AssemblyScanner : GeneralPurposeScanner
    {
        public AssemblyScanner()
        {
            this.lineCommentIdentifiers = new List<LineCommentIdentifier> {
                new LineCommentIdentifier() { StartSequence = ";" },
                new LineCommentIdentifier() { StartSequence = "#" },
                new LineCommentIdentifier() { StartSequence = "//" },
            };
            this.Splitters = new char[] { ';', ' ', '\t', '\r', '\n' };
        }
    }
    public enum MatchResult
    {
        Match, No, ReachEnd
    }
    public class IntermediateUniAssembly
    {
        public List<string> Includes = new List<string>();
        public List<IntermediateInstruction> intermediateInstructions = new List<IntermediateInstruction>();
    }

    public class IntermediateInstruction
    {
        public bool HasLabel;
        public string? label;
        public IntermediateInstruction? FollowingInstruction;
    }
    public enum PrepLabel
    {
        include,
    }
    public enum Section
    {
        Text,
        Constants,
        Program,
        Prep
    }
    public class PartialInstruction : IntermediateInstruction
    {
        public Inst FinalInstruction;
        public bool IsFinallized = false;
        public Segment? Data0;
        public Segment? Data1;
        public Segment? Data2;
    }
    public class UnexpectedEndError : Error
    {
        public Segment segment;
        public UnexpectedEndError(Segment segment) : base()
        {
            this.segment = segment;
        }
        public override string ToString()
        {
            return $"Unexpected End at:{segment.ID}:{segment.LineNumber},{segment.ID}";
        }
    }
}
