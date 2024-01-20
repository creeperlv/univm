using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace univm.core
{
    public class DefaultDispatcher : IDispatcher
    {
        public List<VMCore?> Cores = new List<VMCore?>();
        public bool RunInThread = false;
        public void AddCore(VMCore core)
        {
            Cores.Add(core);
        }

        public List<VMCore?> GetCoreList()
        {
            return Cores;
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal void Cycle()
        {
            for (int i = Cores.Count - 1; i >= 0; i--)
            {
                VMCore? item = Cores[i];
                if (item == null)
                {
                    Cores.RemoveAt(i);
                }
                else
                {
                    if (item.WillWaitUntil)
                    {
                        if (DateTime.Now > item.WaitUntil)
                        {
                            item.WillWaitUntil = false;
                        }
                        else
                            continue;
                    }
                    var coreData = item.coreData;
                    var machineData = item.machineData;
                    var frame = coreData.CallStack[^1];
                    var inst = machineData.assemblies[(int)frame.AssemblyID].Instructions[frame.PCInAssembly];
                    frame.PCInAssembly++;
                    coreData.CallStack[^1] = (frame);
                    item.Execute(inst);
                    if (coreData.CallStack.Count == 0)
                        break;
                    frame = coreData.CallStack[coreData.CallStack.Count - 1];
                    if (machineData.assemblies[(int)frame.AssemblyID].Instructions.Length <= frame.PCInAssembly)
                    {
                        Cores[i] = null;
                    }
                }
            }
        }
        void Run()
        {
            while (true)
            {
                Cycle();
            }
        }
        public void Start()
        {
            if (RunInThread)
                Task.Run(Run);
            else Run();
        }
    }
}