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
        public static void SetupSysCall(CoreData data)
        {
            data.SetSysCall(0, 63, read);
            data.SetSysCall(0, 64, write);
        }
    }
}
