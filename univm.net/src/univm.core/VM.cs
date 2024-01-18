using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using univm.core.Utilities;

namespace univm.core
{
    public interface IDispatcherFactory
    {
        IDispatcher CreateDispatcher();
    }
    public sealed class VM : IDisposable
    {
        public MachineData machineData = new MachineData();
        public List<VMCore> Cores = new List<VMCore>();
        public List<IDispatcher> Dispatchers = new List<IDispatcher>();
        public int ExitCode = 0;
        public VMConfiguration CurrentConfiguration;
        public int CurrentDispatcher = 0;
        public VM(VMConfiguration currentConfiguration)
        {
            CurrentConfiguration = currentConfiguration;
        }

        public void Exit(int exitCode)
        {
            foreach (var item in Cores)
            {
                item.WillContinue = false;
            }
            this.ExitCode = exitCode;
            machineData.Dispose();
            machineData = new MachineData();
            Cores.Clear();
        }
        public void RunAsm(UniVMAssembly asm)
        {
            var core = CreateCore();
            var ID = machineData.AddAssembly(asm, core.coreData);
            core.coreData.CallStack.Add(new CallStackItem() { AssemblyID = (uint)ID, PCInAssembly = 0 });
            core.Run();
        }
        public void CallParallel(uint AssemblyID, uint PCInAssembly)
        {
            var core = CreateCore();
            core.coreData.CallStack.Add(new CallStackItem() { AssemblyID = AssemblyID, PCInAssembly = PCInAssembly });
            if (CurrentConfiguration.UseDispatcher)
            {
                if (Dispatchers.Count < CurrentConfiguration.DispatcherLimit)
                {
                    var dispatcher = CurrentConfiguration.DispatcherFactory.CreateDispatcher();
                    Dispatchers.Add(dispatcher);
                    dispatcher.AddCore(core);
                    dispatcher.Start();
                    CurrentDispatcher++;
                }
                else
                {
                    if (CurrentDispatcher >= Dispatchers.Count) CurrentDispatcher = 0;
                    var dispatcher = Dispatchers[CurrentDispatcher];
                    dispatcher.AddCore(core);

                }
            }
            else
            {
                Task.Run(core.Run);
            }
        }
        public void SetSysCall(uint Namespace, uint ID, SysCall call)
        {
            machineData.SetSysCall(Namespace, ID, call);
        }
        public void RedirectSTDIO()
        {
            var input = Console.OpenStandardInput();
            var output = Console.OpenStandardOutput();
            var err = Console.OpenStandardError();
            machineData.Resources.Add(input);
            machineData.Resources.Add(output);
            machineData.Resources.Add(err);
        }
        public void Dispose()
        {
            machineData.Dispose();
        }
        public VMCore CreateCore()
        {
            VMCore core = new VMCore(this);
            Cores.Add(core);
            return core;
        }
        public void DumpText(TextWriter writer, int Width = 80)
        {
            writer.WriteLine("Cores");
            writer.WriteLine(Cores.Count);
            int _w = 0;
            int ID = 0;
            foreach (var item in Cores)
            {
                writer.WriteLine($"Core {ID}");
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
                ID++;
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
}