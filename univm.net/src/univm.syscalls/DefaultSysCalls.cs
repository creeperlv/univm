using System;
using univm.core;

namespace univm.syscalls
{
    public static class DefaultSysCalls
    {
        public static bool read(RuntimeData runtimeData)
        {
            return false;
        }
        public static bool write(RuntimeData runtimeData)
        {
            return false;
        }
        public static void SetupSysCall(RuntimeData data)
        {
            data.SetSysCall(0, 63, read);
            data.SetSysCall(0, 64, write);
        }
    }
}
