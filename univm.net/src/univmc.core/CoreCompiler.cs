using LibCLCC.NET.Operations;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using univm.core;
using univm.core.Utilities;
using univmc.core.Errors;

namespace univmc.core
{
    public class CompileOptions
    {
        public List<string>? SourceFiles;
        public List<string>? IncludeDirectories;
        public List<string>? Libraries;
        public bool IsStatic = false;
        public string output = "a.out";
    }
    public class CoreCompiler
    {
        CompileOptions options;
        public CoreCompiler(CompileOptions options)
        {
            this.options = options;
        }
        public OperationResult<PartialInstruction> DestructLabels(Dictionary<string, uint> Labels, IntermediateInstruction inst, uint ID)
        {
            OperationResult<PartialInstruction> result = new OperationResult<PartialInstruction>();
            if (inst is PartialInstruction pinst)
            {
                return pinst;
            }
            else
            {
                if (!inst.HasLabel)
                    if (inst.label != null)
                    {
                        if (inst.FollowingInstruction != null)
                        {
                            Labels.Set(inst.label, ID);
                            return DestructLabels(Labels, inst.FollowingInstruction, ID);

                        }
                        result.AddError(new LabelMustHaveSubsequentInstruction(inst.label));
                        return result;
                    }
            }
            result.AddError(new LabelHaveNoRealLabelString());
            return result;
        }
        public OperationResult<bool> FinalizeData(CompileTimeData data)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            data.Artifact = new univm.core.UniVMAssembly();
            List<Inst> Insts = new List<Inst>();
            Dictionary<uint, uint> Offsets = new Dictionary<uint, uint>();
            Dictionary<string, uint> Labels = new Dictionary<string, uint>();
            if (data.IntermediateUniAssembly != null)
            {
                if (options.IsStatic)
                {
                    var libs = data.IntermediateUniAssembly.Libraries;
                    uint ID = 0;
                    foreach (var item in libs)
                    {

                        uint Offset = 0;
                        using var stream = File.OpenRead(item);
                        var asm = UniVMAssembly.Read(stream);
                        if (asm.Instructions != null)
                        {
                            Offsets.Add(ID, Offset);
                            foreach (var inst in asm.Instructions)
                            {
                                switch (inst.Op_Code)
                                {
                                    case InstOPCodes.HL_GETASMID:
                                    case InstOPCodes.BASE_JA:
                                    case InstOPCodes.BASE_JAR:
                                    case InstOPCodes.BASE_JAAL:
                                    case InstOPCodes.BASE_JAALR:
                                    case InstOPCodes.BASE_CALLE:
                                    case InstOPCodes.BASE_CALLER:
                                        result.AddError(new NotStaticAssembly(item));
                                        return result;
                                    default:
                                        break;
                                }
                                Insts.Add(inst);
                            }
                            Offset++;
                        }
                        ID++;
                    }
                }
                //Scan Labels.
                for (int i = 0; i < data.IntermediateUniAssembly.intermediateInstructions.Count; i++)
                {
                    var Inst = data.IntermediateUniAssembly.intermediateInstructions[i];
                    if (Inst is PartialInstruction pinst)
                    {

                    }
                    else
                    {
                        var DL_Result = DestructLabels(Labels, Inst, (uint)i);

                        if (result.CheckAndInheritErrorAndWarnings(DL_Result))
                        {
                            return result;
                        }
                        data.IntermediateUniAssembly.intermediateInstructions[i] = DL_Result.Result;
                    }

                }
                for (int i = 0; i < data.IntermediateUniAssembly.intermediateInstructions.Count; i++)
                {
                    PartialInstruction item = (PartialInstruction)data.IntermediateUniAssembly.intermediateInstructions[i];
                }
            }
            return result;
        }
        public OperationResult<CompileTimeData> Compile()
        {
            CompileTimeData data = new CompileTimeData();
            OperationResult<CompileTimeData> result = new OperationResult<CompileTimeData>(data);
            AssemblyScanner scanner = new AssemblyScanner();
            if (options.SourceFiles != null)
            {
                foreach (var item in options.SourceFiles)
                {
                    var file_info = new FileInfo(item);
                    var parent = file_info.Directory;
                    var HEAD = scanner.Scan(File.ReadAllText(item), false, null);
                    Parser parser = new Parser();
                    var _result = parser.Parse(data, parent.FullName, HEAD);
                    if (result.CheckAndInheritErrorAndWarnings(result))
                    {
                        return result;
                    }
                    if (!_result.Result)
                    {
                        return result;
                    }
                }
            }

            FinalizeData(data);
            return result;
        }
    }
}
