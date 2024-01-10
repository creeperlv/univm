using LibCLCC.NET.Operations;
using LibCLCC.NET.TextProcessing;
using System;
using System.IO;
using univm.core;
using univmc.core.Errors;

namespace univmc.core
{
    public class Parser
    {
        public unsafe void PtrCpy(uint* target, void* source)
        {
            target[0] = ((uint*)source)[0];
        }
        public unsafe void PtrCpy(short* target, void* source)
        {
            target[0] = ((short*)source)[0];

        }
        public unsafe bool TryParse(string str, Type t, uint* data0, uint* data1)
        {
            if (t == typeof(byte))
            {
                if (byte.TryParse(str, out var d))
                {
                    ((byte*)data0)[0] = d;
                    return true;
                }
            }
            else
            if (t == typeof(sbyte))
            {
                if (sbyte.TryParse(str, out var d))
                {
                    ((sbyte*)data0)[0] = d;
                    return true;
                }
            }
            else
            if (t == typeof(Int32))
            {
                if (Int32.TryParse(str, out int d))
                {
                    PtrCpy(data0, &d);
                    return true;
                }
            }
            else
            if (t == typeof(UInt32))
            {
                if (UInt32.TryParse(str, out var data))
                {
                    PtrCpy(data0, &data);
                    return true;
                }
            }
            else
            if (t == typeof(Register))
            {
                if (Register.TryParse(str, out uint data))
                {
                    PtrCpy(data0, &data);
                    return true;
                }
            }
            else
            if (t == typeof(float))
            {
                if (float.TryParse(str, out var data))
                {
                    PtrCpy(data0, &data);
                    return true;
                }
            }
            else
            if (t == typeof(short))
            {
                if (short.TryParse(str, out var data))
                {
                    PtrCpy((short*)data0, &data);
                    return true;
                }
            }
            else
            if (t == typeof(Int64))
            {
                if (Int64.TryParse(str, out var data))
                {
                    var ptr = &data;
                    PtrCpy(data0, ptr);
                    ptr += sizeof(uint);
                    PtrCpy(data1, ptr);
                    return true;
                }
            }
            else
            if (t == typeof(UInt64))
            {
                if (UInt64.TryParse(str, out var data))
                {
                    var ptr = &data;
                    PtrCpy(data0, ptr);
                    ptr += sizeof(uint);
                    PtrCpy(data1, ptr);
                    return true;
                }
            }
            else
            if (t == typeof(double))
            {
                if (double.TryParse(str, out var data))
                {
                    var ptr = &data;
                    PtrCpy(data0, ptr);
                    ptr += sizeof(uint);
                    PtrCpy(data1, ptr);
                    return true;
                }
            }
            return false;
        }
        public unsafe OperationResult<bool> ConvertData(PartialInstruction instruction, Type? Data0T, Type? Data1T, Type? Data2T)
        {
            OperationResult<bool> result = new OperationResult<bool>(false);
            Inst FinalInst = instruction.FinalInstruction;
            bool AllMatch = true;
            if (Data0T != null)
            {

                if (instruction.Data0 != null)
                {
                    if (TryParse(instruction.Data0.content, Data0T, &FinalInst.Data0, &FinalInst.Data1))
                    {
                        AllMatch |= true;
                    }
                    else
                    {
                        AllMatch = false;
                    }
                }
            }
            if (Data1T != null)
            {

                if (instruction.Data1 != null)
                {
                    if (TryParse(instruction.Data1.content, Data1T, &FinalInst.Data1, &FinalInst.Data2))
                    {
                        AllMatch |= true;
                    }
                    else
                    {
                        AllMatch = false;
                    }
                }
            }
            if (Data2T != null)
            {

                if (instruction.Data2 != null)
                {
                    if (TryParse(instruction.Data2.content, Data2T, &FinalInst.Data2, null))
                    {
                        AllMatch |= true;
                    }
                    else
                    {
                        AllMatch = false;
                    }
                }
            }
            instruction.FinalInstruction = FinalInst;
            if (AllMatch)
            {
                instruction.IsFinallized = true;
            }
            result.Result = true;
            return result;
        }
        public OperationResult<bool> DataMarcher(PartialInstruction partialInstruction, ParseContext context, int Count)
        {
            OperationResult<bool> result = new OperationResult<bool>(false);
            int i = 0;
            if (!context.GoNext())
            {
                result.AddError(new UnexpectedEndError(context.Current));
                return result;
            }
            partialInstruction.Data0 = context.Current;
            i++;
            if (i >= Count)
                goto END;
            if (!context.GoNext())
            {
                result.AddError(new UnexpectedEndError(context.Current));
                return result;
            }
            partialInstruction.Data1 = context.Current;
            i++;
            if (i >= Count)
                goto END;
            if (!context.GoNext())
            {
                result.AddError(new UnexpectedEndError(context.Current));
                return result;
            }
        END:
            result.Result = true;
            return result;
        }
        public OperationResult<bool> InstructionParse(PartialInstruction partialInstruction, ParseContext context, InstructionTypeDefinition definition)
        {
            int Count = 0;
            if (definition.Data0Type != null)
            {
                Count++;
            }
            if (definition.Data2Type != null)
            {
                Count++;
            }
            if (definition.Data1Type != null)
            {
                Count++;
            }
            var marchResult = DataMarcher(partialInstruction, context, Count);
            if (marchResult.Result == false)
            {
                return marchResult;
            }
            return ConvertData(partialInstruction, definition.Data0Type, definition.Data1Type, definition.Data2Type);
        }
        public OperationResult<bool> FindAndLoadAsmDef(CompileTimeData data, string workingDir, string file, bool IsLocalized)
        {
            OperationResult<bool> result = new OperationResult<bool>(false);
            if (IsLocalized)
            {
                var _file = Path.Combine(workingDir, file);
                if (!File.Exists(_file))
                {
                    result.AddError(new AssemblyDefinitionNotFound(_file));
                    return result;
                }
                using var fs = File.OpenRead(_file);
                var def = AssemblyDefinition.LoadFromStream(fs);
                if (def.IsStaticAssembly)
                {
                    if (def.AssemblyID != null)
                    {
                        data.IntermediateUniAssembly?.LoadedDefinitions.Add(def.AssemblyID, def);
                        goto SKIP_NORMAL_DEF;
                    }
                }
                data.AssembleDefs.Add(def);
            SKIP_NORMAL_DEF:
                return true;
            }
            return result;
            //data.AssembleDefs.Add
        }
        public OperationResult<bool> Parse(ISADefinition definition, CompileTimeData data, string WorkDirectory, Segment HEAD)
        {
            OperationResult<bool> result = new OperationResult<bool>(false);
            if (data.IntermediateUniAssembly is null)
            {
                data.IntermediateUniAssembly = new IntermediateUniAssembly();
            }
            ParseContext context = new ParseContext(HEAD);
            IntermediateInstruction? inst = null;
            IntermediateInstruction? last = null;
            Section currentSection = Section.Program;
            while (true)
            {
                bool WillBreak = false;
                var isLabel = context.IsLabel();
                if (isLabel == MatchResult.Match)
                {
                    var LabelName = context.GetCurrentContent();
                    var QueryName = LabelName.ToLower();
                    if (Keywords.SectionNames.ContainsKey(QueryName))
                    {
                        currentSection = Keywords.SectionNames[QueryName];
                        context.GoNext();
                    }
                    else
                    {
                        inst = new IntermediateInstruction();
                        inst.HasLabel = true;
                        inst.label = LabelName;
                        context.GoNext();
                    }
                }
                else if (isLabel == MatchResult.No)
                {
                    switch (currentSection)
                    {
                        case Section.Prep:
                            {
                                var label = context.GetCurrentContent().ToLower();
                                if (Keywords.PrepLabels.TryGetValue(label, out var key))
                                {
                                    switch (key)
                                    {
                                        case PrepLabel.include:
                                            {
                                                if (!context.GoNext())
                                                {
                                                    result.AddError(new UnexpectedEndError(context.Current));
                                                    return result;
                                                }
                                                if (context.Current.EncapsulationIdentifier.L == '\"')
                                                {
                                                    var _result = FindAndLoadAsmDef(data, WorkDirectory, context.GetCurrentContent(), true);
                                                    if (result.CheckAndInheritErrorAndWarnings(_result))
                                                    {
                                                        return result;
                                                    }
                                                    //data.IntermediateUniAssembly.Includes.Add(Path.Combine(WorkDirectory, context.GetCurrentContent()));
                                                }
                                                else if (context.Current.EncapsulationIdentifier.L == '<')
                                                {
                                                    var _result = FindAndLoadAsmDef(data, WorkDirectory, context.GetCurrentContent(), false);
                                                    if (result.CheckAndInheritErrorAndWarnings(_result))
                                                    {
                                                        return result;
                                                    }
                                                    //data.IntermediateUniAssembly.Includes.Add(Path.Combine(WorkDirectory, context.GetCurrentContent()));
                                                }
                                            }
                                            break;
                                        case PrepLabel.library:
                                            {

                                                if (!context.GoNext())
                                                {
                                                    result.AddError(new UnexpectedEndError(context.Current));
                                                    return result;
                                                }
                                                {
                                                    var Name = context.GetCurrentContent();
                                                    if (!context.GoNext())
                                                    {
                                                        result.AddError(new UnexpectedEndError(context.Current));
                                                        return result;
                                                    }
                                                    var Content = context.GetCurrentContent();
                                                    data.IntermediateUniAssembly.LibraryKeys.Add(Name);
                                                    data.IntermediateUniAssembly.LibraryMap.Add(Name, Content);
                                                }
                                            }
                                            break;
                                        case PrepLabel.global:
                                            {

                                                if (!context.GoNext())
                                                {
                                                    result.AddError(new UnexpectedEndError(context.Current));
                                                    return result;
                                                }
                                                {
                                                    var Name = context.GetCurrentContent();
                                                    if (!context.GoNext())
                                                    {
                                                        result.AddError(new UnexpectedEndError(context.Current));
                                                        return result;
                                                    }
                                                    var Value = context.GetCurrentContent();
                                                    if (uint.TryParse(Value, out var _value))
                                                    {
                                                        data.IntermediateUniAssembly.GMOKeys.Add(Name);
                                                        data.IntermediateUniAssembly.GlobalMemOffsets.Add(Name, _value);
                                                    }
                                                    else
                                                    {
                                                        result.AddError(new TypeParseError(typeof(uint), Value));
                                                    }
                                                }
                                            }
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                            break;
                        case Section.Text:
                            {
                                var Name = context.GetCurrentContent();
                                if (!context.GoNext())
                                {
                                    result.AddError(new UnexpectedEndError(context.Current));
                                    return result;
                                }
                                var Content = context.GetCurrentContent();
                                data.IntermediateUniAssembly.TextKeys.Add(Name);
                                data.IntermediateUniAssembly.Texts.Add(Name, Content);
                            }
                            break;
                        case Section.Constants:
                            {
                                var Name = context.GetCurrentContent();
                                if (!context.GoNext())
                                {
                                    result.AddError(new UnexpectedEndError(context.Current));
                                    return result;
                                }
                                var Content = context.GetCurrentContent();
                                data.IntermediateUniAssembly.Constants.Add(Name, Content);
                            }
                            break;
                        case Section.Program:
                            var op_code = context.GetCurrentContent().ToLower();
                            if (definition.Operations.ContainsKey(op_code))
                            {
                                inst = new PartialInstruction();
                                var pi = inst as PartialInstruction;
                                if (pi is null)
                                {
                                    //WTF happened!?
                                    result.AddError(new Error());
                                    return result;
                                }
                                pi.FinalInstruction = new Inst();
                                var OPCode = definition.Operations[op_code];
                                pi.FinalInstruction.Op_Code = OPCode;
                                if (definition.Definitions.ContainsKey(OPCode))
                                {
                                    InstructionParse(pi, context, definition.Definitions[OPCode]);
                                }
                                else
                                {
                                    //Not Implemented Yet.
                                }
                            }
                            else
                            {

                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    WillBreak = true;
                    goto FINALIZE_INST;
                }
            FINALIZE_INST:
                if (inst != null)
                {
                    if (data.IntermediateUniAssembly.intermediateInstructions.Count == 0)
                    {
                        data.IntermediateUniAssembly.intermediateInstructions.Add(inst);
                        last = inst;
                    }
                    else
                    {
                        bool WillSkip = false;
                        if (last != null)
                        {
                            if (last.HasLabel && last.FollowingInstruction == null)
                            {
                                last.FollowingInstruction = inst;
                                last = inst;
                                WillSkip = true;
                            }
                        }

                        if (!WillSkip)
                        {
                            data.IntermediateUniAssembly.intermediateInstructions.Add(inst);
                            last = inst;
                        }
                    }
                }
                if (WillBreak) break;
                if (context.IsCurrentLastSegment())
                {
                    break;
                }
                if (context.PeekNext() == ";")
                {
                    context.GoNext();
                }
                context.GoNext();

            }
            result.Result = true;
            return result;
        }
    }
}
