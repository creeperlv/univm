using System.Collections.Generic;

namespace univm.core
{
    public interface IDispatcher
    {
        void AddCore(VMCore core);
        List<VMCore?> GetCoreList();
        void Start();
    }
}