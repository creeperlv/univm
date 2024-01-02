using System;
using univm.core;

namespace univm.syscalls
{
    public static class DefaultSysCalls
    {
        public static bool read(CoreData runtimeData)
        {
            return false;
        }
        public static bool write(CoreData runtimeData)
        {
            return false;
        }
        public static void SetupSysCall(VM vm)
        {
            vm.SetSysCall(0, 63, read);
            vm.SetSysCall(0, 64, write);
        }
    }
}
