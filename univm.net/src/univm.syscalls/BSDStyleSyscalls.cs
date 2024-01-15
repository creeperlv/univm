using System;
using System.Data.Common;
using System.IO;
using univm.core;
using univm.core.Utilities;

namespace univm.syscalls
{
    public class DirectoryDscriptor : IDisposable
    {
        public DirectoryInfo? DirectoryInfo;
        public DirectoryDscriptor(string path)
        {
            DirectoryInfo = new DirectoryInfo(path);
        }
        public void Dispose()
        {
            DirectoryInfo = null;
        }
    }
    public static class fcntl
    {
        public const uint FileAccessMask = 0x0000000F;
        public const uint O_RDONLY = 0x00000000;
        public const uint O_WRONLY_MASK = 0xFFFF_FFFE;
        public const uint O_WRONLY = 0x00000001;
        public const uint O_RDWR_MASK = 0xFFFF_FFFD;
        public const uint O_RDWR = 0x00000002;
        public const uint O_CREAT_MASK = 0xFFFFFEFF;
        public const uint O_CREAT = 0x00000100;
        public const uint O_EXCL_MASK = 0xFFFFFDFF;
        public const uint O_EXCL = 0x00000200;
        public const uint StartPosition = 0x0000F000;
        public const uint O_TRUNC_MASK = 0xFFFF_EFFF;
        public const uint O_TRUNC = 0x00001000;
        public const uint O_APPEND_MASK = 0xFFFF_DFFF;
        public const uint O_APPEND = 0x00002000;
        public const uint ShareMask = 0xF0;
        public const uint O_SHLOCK_MASK = 0xFFFF_FFEF;
        public const uint O_SHLOCK = 0x10;
        public const uint O_EXLOCK_MASK = 0xFFFF_FFDF;
        public const uint O_EXLOCK = 0x20;
    }
    public static class kern_descrip
    {
        public static bool close(CoreData coreData)
        {
            var FileID = coreData.GetDataFromRegister<int>(RegisterDefinition.A1);
            coreData.TryCloseResourceByID(FileID);
            return true;
        }
    }
    public static class kern_exit
    {
        public static bool exit(CoreData coreData)
        {
            var status = coreData.GetDataFromRegister<int>(RegisterDefinition.A1);
            coreData.core.HostMachine.Exit(status);
            return true;
        }
    }
    public static class BSDStyleSyscalls
    {
        public static void SetupSysCall(VM vm)
        {
            vm.SetSysCall(0, 1, kern_exit.exit);
            vm.SetSysCall(0, 3, sys_generic.read);
            vm.SetSysCall(0, 4, sys_generic.write);
            vm.SetSysCall(0, 5, vfs_syscalls.open);
            vm.SetSysCall(0, 6, kern_descrip.close);
            vm.SetSysCall(0, 12, vfs_syscalls.chdir);
            vm.SetSysCall(0, 95, vfs_syscalls.fsync);
        }
    }
}
