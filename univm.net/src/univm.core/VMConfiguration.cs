using System.Collections.Generic;
using System.Threading.Tasks;

namespace univm.core
{
    public sealed class VMConfiguration
    {
        public bool UseDispatcher;
        public int DispatcherLimit;
        public IDispatcherFactory DispatcherFactory;
        public readonly static VMConfiguration NoDispatcherConfiguration = new VMConfiguration(new DefaultDispatcherFactory()) { UseDispatcher = false };
        public VMConfiguration(IDispatcherFactory dispatcherFactory)
        {
            DispatcherFactory = dispatcherFactory;
        }
    }
    public class DefaultDispatcherFactory : IDispatcherFactory
    {
        public IDispatcher CreateDispatcher()
        {
            return new DefaultDispatcher();
        }
    }
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
        void Run()
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
        public void Start()
        {
            if (RunInThread)
                Task.Run(Run);
            else Run();
        }
    }
}