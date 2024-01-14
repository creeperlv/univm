using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using univm.cli.core;
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
    public class Output : IDisposable
    {
        public Stream stream;

        public Output(string path)
        {
            this.stream = File.OpenWrite(path);
        }
        public Output(Stream stream)
        {
            this.stream = stream;
        }

        public void Dispose()
        {
            stream.Dispose();
        }
    }
    public class CompileOptions : IDisposable
    {
        public List<SourceFile> SourceFiles = new List<SourceFile>();
        public List<string>? IncludeDirectories;
        public List<string>? Libraries;
        public bool IsStatic = false;
        public bool ProduceDefinition = false;
        public bool UseBuiltInISADefinition = false;
        public bool ISAFileInWorkingDirectory = false;
        public string ISADefinitionFile = "isas/univm.isa";
        public Output? output;// = "a.out";
        public Output? DefinitionFile;
        public string FallbackWorkingDirectory = ".";
        public CompileOptions() { }
        public CompileOptions(CLIOptions options)
        {
            foreach (var item in options.MainParameters)
            {
                SourceFiles.Add(new SourceFile { Data = item, DataIsNotFile = false });
            }
            if (options.Options.TryGetValue("-O", out var _value))
            {
                output = new Output(_value);
            }
            else
            {
                output = new Output("a.out");
            }
        }

        public void Dispose()
        {
            output?.Dispose();
            DefinitionFile?.Dispose();
        }
    }
    public class CompileIntermediateData
    {
        public IntermediateUniAssembly assembly;
        public UniVMAssembly finalAssembly;
        public CompileIntermediateData(IntermediateUniAssembly assembly, UniVMAssembly finalAssembly)
        {
            this.assembly = assembly;
            this.finalAssembly = finalAssembly;
        }

        public Dictionary<uint, uint> Offsets = new Dictionary<uint, uint>();
        public Dictionary<uint, uint> GMOffsets = new Dictionary<uint, uint>();
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
                if (inst.HasLabel)
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
            else if (content.StartsWith("?"))
            {

                var _data = cidata.assembly.TextKeys.IndexOf(content[1..]);
                if (_data >= 0)
                {
                    if (cidata.finalAssembly.Texts != null)
                    {

                        data = (uint)cidata.finalAssembly.Texts[_data].Length;
                        return true;
                    }
                    else
                    {
                        data = 0;
                        return false;
                    }
                }
                else
                {
                    data = default;
                    operationResult.AddError(new TextDefinitionNotFound(content[1..]));
                    return operationResult;
                }
            }
            else if (content.StartsWith(">"))
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
            else if (content.StartsWith("^"))
            {

                if (cidata.assembly.GlobalMemOffsets.TryGetValue(content[1..], out uint _data))
                {
                    data = _data;
                    return true;
                }
                else
                {
                    data = default;
                    operationResult.AddError(new GlobalVariableNotFound(content[1..]));
                    return operationResult;
                }
            }
            data = default;
            return true;
        }
        public unsafe OperationResult<bool> PostProcessInstructions(CompileTimeData data)
        {
            OperationResult<bool> result = new OperationResult<bool>();
            data.Artifact = new UniVMAssembly();
            List<Inst> Insts = new List<Inst>();
            if (data.IntermediateUniAssembly != null)
            {
                uint GMOffset = 0;
                CompileIntermediateData cidata = new CompileIntermediateData(data.IntermediateUniAssembly, data.Artifact);
                if (options.IsStatic)
                {
                    var libs = data.IntermediateUniAssembly.Libraries;
                    uint ID = 0;
                    foreach (var item in libs)
                    {

                        uint Offset = 0;
                        using var stream = File.OpenRead(item);
                        FileInfo fileInfo = new FileInfo(item);
                        var ASM_ID = fileInfo.Name.ToLower();
                        var asm = UniVMAssembly.Read(stream);
                        {
                            var def = data.IntermediateUniAssembly.LoadedDefinitions[ASM_ID];
                            foreach (var lbl in def.Labels)
                            {
                                cidata.Labels.Add(lbl.Key, lbl.Value + Offset);
                            }
                        }
                        if (asm.Instructions != null)
                        {
                            cidata.Offsets.Add(ID, Offset);
                            cidata.GMOffsets.Add(ID, GMOffset);
                            for (int i = 0; i < asm.Instructions.Length; i++)
                            {
                                Inst inst = asm.Instructions[i];
                                switch (inst.Op_Code)
                                {
                                    case InstOPCodes.BASE_SETLBL:
                                        inst.Data1 += Offset;
                                        break;
                                    case InstOPCodes.BASE_JA:
                                        inst.Data1 += Offset;
                                        break;
                                    case InstOPCodes.BASE_JAAL:
                                        inst.Data1 += Offset;
                                        break;
                                    case InstOPCodes.BASE_CALL:
                                        inst.Data0 += Offset;
                                        break;
                                    case InstOPCodes.HL_MAP_GLBMEM:
                                        inst.Data1 += GMOffset;
                                        break;
                                    case InstOPCodes.HL_GETASMID:
                                    case InstOPCodes.BASE_JAR:
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
                        GMOffset += asm.GlobalMemSize;
                        ID++;
                    }
                }
                else
                {
                    foreach (var item in data.AssembleDefs)
                    {
                        cidata.Labels.MergeWith(item.Labels);
                    }
                }

                data.Artifact.Texts = new TextItem[data.IntermediateUniAssembly.Texts.Values.Count];
                var textList = data.IntermediateUniAssembly.Texts.Values.ToList();
                for (int i = 0; i < textList.Count; i++)
                {
                    string? item = textList[i];
                    var bytes = Encoding.UTF8.GetBytes(item);
                    void* b = (void*)Marshal.AllocHGlobal(bytes.Length);
                    fixed (void* _b = bytes)
                    {
                        Buffer.MemoryCopy(_b, b, bytes.Length, bytes.Length);
                    }
                    data.Artifact.Texts[i].Length = (uint)bytes.Length;
                    data.Artifact.Texts[i].Data = (byte*)b;
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
                            foreach (var item in result.Errors)
                            {
                                Console.WriteLine(item.ToString());
                            }
                            return result;
                        }
                        data.IntermediateUniAssembly.intermediateInstructions[i] = DL_Result.Result;
                    }

                }
                for (int i = 0; i < data.IntermediateUniAssembly.intermediateInstructions.Count; i++)
                {
                    //ProcessInstructions.
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
                        if (options.IsStatic)
                        {
                            switch (item.FinalInstruction.Op_Code)
                            {
                                case InstOPCodes.BASE_CALLE:
                                    {
                                        item.FinalInstruction.Op_Code = InstOPCodes.BASE_CALL;
                                        item.FinalInstruction.Data0 = item.FinalInstruction.Data1;
                                        item.FinalInstruction.Data1 = uint.MinValue;
                                    }
                                    break;
                                case InstOPCodes.BASE_CALLER:
                                    {
                                        item.FinalInstruction.Op_Code = InstOPCodes.BASE_CALLR;
                                        item.FinalInstruction.Data0 = item.FinalInstruction.Data1;
                                        item.FinalInstruction.Data1 = uint.MinValue;
                                    }
                                    //item.FinalInstruction.Data0+=;
                                    break;
                                case InstOPCodes.HL_MAP_GLBMEM:
                                    {
                                        item.FinalInstruction.Data1 = (uint)(item.FinalInstruction.Data1 + GMOffset);
                                    }
                                    break;
                                case InstOPCodes.HL_MAP_AGLBMEM:
                                    {
                                        item.FinalInstruction.Op_Code = InstOPCodes.HL_MAP_GLBMEM;
                                        item.FinalInstruction.Data1 = cidata.GMOffsets[item.FinalInstruction.Data1];
                                        item.FinalInstruction.Data2 = 0;
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        Insts.Add(item.FinalInstruction);
                    }

                }
                uint Size = 0;
                foreach (var item in data.IntermediateUniAssembly.GlobalMemOffsets.Values)
                {
                    Size += item;
                }
                data.Artifact.GlobalMemSize = Size;
                data.Artifact.Instructions = Insts.ToArray();
            }
            return result;
        }

        public OperationResult<CompileTimeData> Compile()
        {
            CompileTimeData data = new CompileTimeData();
            OperationResult<CompileTimeData> result = new OperationResult<CompileTimeData>(data);
            AssemblyScanner scanner = new AssemblyScanner();
            ISADefinition isa;
            if (options.UseBuiltInISADefinition)
            {
                isa = ISADefinition.Default;
            }
            else
            {
                if (options.ISAFileInWorkingDirectory)
                {
                    using var sr = File.OpenText(options.ISADefinitionFile);
                    isa = ISADefinition.LoadFromReader(sr);
                }
                else
                {

                    var FullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.ISADefinitionFile);
                    using var sr = File.OpenText(FullPath);
                    isa = ISADefinition.LoadFromReader(sr);
                }
            }
            if (options.SourceFiles != null)
            {
                foreach (var item in options.SourceFiles)
                {
                    string Directory;
                    Segment HEAD;
                    if (item.DataIsNotFile)
                    {
                        HEAD = scanner.Scan(item.Data, false);
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
                    var _result = parser.Parse(isa, data, Directory, HEAD);
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
            PostProcessInstructions(data);
            return result;
        }
    }
}
