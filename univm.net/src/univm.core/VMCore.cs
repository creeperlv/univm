using System;
using System.Collections.Generic;
using System.IO;
using univm.core.Utilities;

namespace univm.core
{
    public sealed class VM : IDisposable
    {
        MachineData machineData = new MachineData();
        public List<VMCore> Cores = new List<VMCore>();
        public void Dispose()
        {
            machineData.Dispose();
        }
        public void DumpText(TextWriter writer, int Width = 80)
        {
            writer.WriteLine("Cores");
            writer.WriteLine(Cores.Count);
            int _w = 0;
            foreach (var item in Cores)
            {
                writer.WriteLine(item.coreData.RegisterData.Length);
                for (int i = 0; i < item.coreData.RegisterData.Length; i++)
                {
                    writer.Write(item.coreData.RegisterData[i].ToHEX());
                    _w += 2;
                    if (_w >= Width)
                    {
                        writer.WriteLine();
                        _w = 0;
                    }
                }
                writer.WriteLine();

            }
            writer.WriteLine("Memory");
            writer.WriteLine(machineData.MemBlocks.Count);
            unsafe
            {

                foreach (var item in machineData.MemBlocks)
                {
                    _w = 0;
                    writer.WriteLine(item.Size);
                    for (int i = 0; i < item.Size; i++)
                    {
                        writer.Write((item.Data[i]).ToHEX());
                        _w += 2;
                        if (_w >= Width)
                        {
                            writer.WriteLine();
                            _w = 0;
                        }
                    }
                    writer.WriteLine();
                }
            }
        }
        public void DumpBinary(Stream output)
        {
            output.WriteData(Cores.Count);
            foreach (var item in Cores)
            {
                output.WriteData(item.coreData.RegisterData.Length);
                output.Write(item.coreData.RegisterData);

            }
            output.WriteData(machineData.MemBlocks.Count);
            unsafe
            {
                foreach (var item in machineData.MemBlocks)
                {
                    output.WriteData(item.Size);
                    for (int i = 0; i < item.Size; i++)
                    {
                        output.WriteByte(item.Data[i]);
                    }
                }
            }
        }
    }
    public sealed class VMCore : IDisposable
    {
        public CoreData coreData = new CoreData();
        public MachineData sharedData = new MachineData();
        public void Dispose()
        {
        }
        public void Execute(Inst inst)
        {
            switch (inst.Op_Code)
            {
                case InstOPCodes.BASE_ADD:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) + coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) - coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) * coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) / coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SET32:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1);
                    break;
                case InstOPCodes.BASE_SET16:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(short));
                    break;
                case InstOPCodes.BASE_SET8:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(byte));
                    break;
                case InstOPCodes.BASE_SET64:
                    coreData.SetDataToRegister64(inst.Data0, inst.Data1, inst.Data2);
                    break;
                case InstOPCodes.HL_ALLOC:
                    {
                        var id = coreData.SharedCoreData.Alloc(coreData.GetDataFromRegister<uint>(inst.Data1), coreData);
                        MemPtr ptr = new MemPtr(id, 0);
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_FREE:
                    {
                        coreData.SharedCoreData.Free(coreData.GetDataFromRegister<uint>(inst.Data0));
                    }
                    break;
                case InstOPCodes.HL_MEASURE:
                    coreData.SetDataToRegister(inst.Data1, coreData.SharedCoreData.GetMemBlockSize(coreData.GetDataFromRegister<uint>(inst.Data0), coreData));
                    break;
                case InstOPCodes.BASE_SYSCALL:
                    coreData.SharedCoreData.SysCall(inst.Data0, inst.Data1, coreData);
                    break;
                case InstOPCodes.BASE_SYSCALLR:
                    coreData.SharedCoreData.SysCall(coreData.GetDataFromRegister<uint>(inst.Data0), coreData.GetDataFromRegister<uint>(inst.Data0), coreData);
                    break;
                case InstOPCodes.BASE_SYSCALL_TEST:
                    coreData.SetDataToRegister(inst.Data2, coreData.SharedCoreData.IsSysCallExist(inst.Data0, inst.Data1) ? 1 : 0);
                    break;
                case InstOPCodes.BASE_SYSCALL_TESTR:
                    coreData.SetDataToRegister(inst.Data2, coreData.SharedCoreData.IsSysCallExist(coreData.GetDataFromRegister<uint>(inst.Data0), coreData.GetDataFromRegister<uint>(inst.Data0)) ? 1 : 0);
                    break;
                default:
                    break;
            }
        }
        public void Call(CallStackItem frame)
        {
            coreData.CallStack.Add(frame);
            Run();
        }
        public void Run()
        {
            while (true)
            {

                var frame = coreData.CallStack[coreData.CallStack.Count - 1];
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var inst = sharedData.assemblies[(int)frame.AssemblyID].Instructions[frame.PCInAssembly];
                frame.PCInAssembly++;
                coreData.CallStack[coreData.CallStack.Count - 1] = (frame);
                Execute(inst);
                frame = coreData.CallStack[coreData.CallStack.Count - 1];
                if (sharedData.assemblies[(int)frame.AssemblyID].Instructions.Length >= frame.PCInAssembly)
                {
                    return;
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
    }
}