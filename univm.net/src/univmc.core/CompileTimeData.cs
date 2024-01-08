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
        public AssembleDef? ArtifactDef = null;
        public List<AssembleDef> AssembleDefs = new List<AssembleDef>();
        public List<UniVMAssembly?> LoadedAssemblies = new List<UniVMAssembly?>();
    }
    public enum MatchResult
    {
        Match, No, ReachEnd
    }
    public class IntermediateUniAssembly
    {
        public List<string> Includes = new List<string>();
        public List<string> Libraries = new List<string>();
        public List<string> LibraryKeys = new List<string>();
        public List<string> TextKeys = new List<string>();
        public List<string> GMOKeys= new List<string>();
        public Dictionary<string, string> LibraryMap = new Dictionary<string, string>();
        public Dictionary<string, string> Texts = new Dictionary<string, string>();
        public Dictionary<string, string> Constants = new Dictionary<string, string>();
        public Dictionary<string, uint> GlobalMemOffsets= new Dictionary<string, uint>();
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
        include, library,global
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
