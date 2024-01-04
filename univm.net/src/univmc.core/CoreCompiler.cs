using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using univm.core;
using univm.core.Utilities;
using univmc.core.Errors;

namespace univmc.core
{
    public class SourceFile
    {
        public string Data = string.Empty;
        public bool DataIsNotFile;
    }
    public class CompileOptions
    {
        public List<SourceFile>? SourceFiles;
        public List<string>? IncludeDirectories;
        public List<string>? Libraries;
        public bool IsStatic = false;
        public string output = "a.out";
        public string FallbackWorkingDirectory = ".";
    }
    public class CompileIntermediateData
    {
        public IntermediateUniAssembly assembly;

        public CompileIntermediateData(IntermediateUniAssembly assembly)
        {
            this.assembly = assembly;
        }

        public Dictionary<uint, uint> Offsets = new Dictionary<uint, uint>();
        public Dictionary<string, uint> Labels = new Dictionary<string, uint>();
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
        public unsafe OperationResult<bool> TryParse(CompileIntermediateData cidata, string content, out uint data)
        {
            OperationResult<bool> operationResult = new OperationResult<bool>(false);
            if (content.StartsWith("%"))
            {
                if (cidata.assembly.Constants.TryGetValue(content[1..], out var _value))
                {
                    return TryParse(cidata, _value, out data);
                }
                else
                {
                    data = default;
                    operationResult.AddError(new ConstantDefinitionNotFound(content[1..]));
                    return operationResult;
                }
            }
            else if (content.StartsWith("@"))
            {

                var _data = cidata.assembly.TextKeys.IndexOf(content[1..]);
                if (_data >= 0)
                {
                    data = (uint)_data;
                    return true;
                }
                else
                {
                    data = default;
                    operationResult.AddError(new TextDefinitionNotFound(content[1..]));
                    return operationResult;
                }
            }
            else if (content.StartsWith(":"))
            {

                if (cidata.Labels.TryGetValue(content[1..], out uint _data))
                {
                    data = _data;
                    return true;
                }
                else
                {
                    data = default;
                    operationResult.AddError(new LabelNotFound(content[1..]));
                    return operationResult;
                }
            }
            data = default;
            return true;
        }
        public OperationResult<bool> FinalizeData(CompileTimeData data)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            data.Artifact = new univm.core.UniVMAssembly();
            List<Inst> Insts = new List<Inst>();
            if (data.IntermediateUniAssembly != null)
            {
                CompileIntermediateData cidata = new CompileIntermediateData(data.IntermediateUniAssembly);
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
                            cidata.Offsets.Add(ID, Offset);
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
                        var DL_Result = DestructLabels(cidata.Labels, Inst, (uint)i + (uint)Insts.Count);

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
                    if (item.IsFinallized)
                    {
                        Insts.Add(item.FinalInstruction);
                    }
                    else
                    {
                        if (item.Data0 != null)
                        {
                            var res = TryParse(cidata, item.Data0.content, out var _value);
                            if (result.CheckAndInheritErrorAndWarnings(res))
                            {
                                return result;
                            }
                            item.FinalInstruction.Data0 = _value;
                        }
                        if (item.Data1 != null)
                        {
                            var res = TryParse(cidata, item.Data1.content, out var _value);
                            if (result.CheckAndInheritErrorAndWarnings(res))
                            {
                                return result;
                            }
                            item.FinalInstruction.Data1 = _value;
                        }
                        if (item.Data2 != null)
                        {
                            var res = TryParse(cidata, item.Data2.content, out var _value);
                            if (result.CheckAndInheritErrorAndWarnings(res))
                            {
                                return result;
                            }
                            item.FinalInstruction.Data2 = _value;
                        }
                        Insts.Add(item.FinalInstruction);
                    }

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
                    string Directory;
                    Segment HEAD;
                    if (item.DataIsNotFile)
                    {
                        HEAD=scanner.Scan(item.Data, false);
                        Directory = options.FallbackWorkingDirectory;
                    }
                    else
                    {
                        var file_info = new FileInfo(item.Data);
                        HEAD = scanner.Scan(File.ReadAllText(file_info.FullName), false, file_info.FullName);
                        var parent = file_info.Directory;
                        Directory = parent.FullName;
                    }
                    Parser parser = new Parser();
                    var _result = parser.Parse(data, Directory, HEAD);
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
