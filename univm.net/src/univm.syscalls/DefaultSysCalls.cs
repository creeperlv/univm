using System;
using System.Data.Common;
using System.IO;
using System.Text;
using univm.core;
using univm.core.Utilities;

namespace univm.syscalls
{
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
    public static class DefaultSysCalls
    {
        public static bool read(CoreData coreData)
        {
            return false;
        }
        public unsafe static bool open(CoreData coreData)
        {
            var PathPtr = coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.A1);
            var Flags = coreData.GetDataFromRegister<uint>(RegisterDefinition.A2);
            uint BufLen = coreData.GetMemBlockSize(PathPtr);
            Span<byte> buffer = stackalloc byte[(int)BufLen];
            FileAccess access = FileAccess.Read;
            FileMode mode = FileMode.Open;
            FileShare share = FileShare.None;
            bool IsWROnly = (Flags & fcntl.O_WRONLY) == fcntl.O_WRONLY;
            bool IsRDWR = (Flags & fcntl.O_RDWR) == fcntl.O_RDWR;
            if (IsRDWR)
            {
                access = FileAccess.ReadWrite;
            }
            else if (IsWROnly)
            {
                access = FileAccess.Write;
            }
            bool IsCreate = (Flags & fcntl.O_CREAT) == fcntl.O_CREAT;
            bool IsExcl = (Flags & fcntl.O_EXCL) == fcntl.O_EXCL;
            bool IsTrunc = (Flags & fcntl.O_TRUNC) == fcntl.O_TRUNC;
            bool IsAppend = (Flags & fcntl.O_APPEND) == fcntl.O_APPEND;
            if (IsCreate && IsTrunc)
            {
                mode = FileMode.CreateNew;
            }
            else if (IsCreate)
            {
                mode = mode = FileMode.OpenOrCreate;
            }

            if (IsTrunc)
            {
                mode = FileMode.Truncate;
            }
            else if (IsAppend)
            {
                mode = FileMode.Append;
            }
            bool IsExclusive = (Flags & fcntl.O_EXLOCK) == fcntl.O_EXLOCK;
            bool IsShare = (Flags & fcntl.O_SHLOCK) == fcntl.O_SHLOCK;
            if (IsShare)
            {
                share = FileShare.ReadWrite;
            }
            fixed(byte* buf = buffer)
            {
                coreData.GetDataFromMemPtr(buf,PathPtr, BufLen);
            }
            var stream = File.Open(Encoding.UTF8.GetString(buffer), mode, access, share);
            var id = coreData.mdata.AddResource(stream);
            coreData.SetDataToRegister(RegisterDefinition.A0, id);
            return false;
        }
        public unsafe static bool write(CoreData coreData)
        {
            var FileID = coreData.GetDataFromRegister<int>(RegisterDefinition.A1);
            var _Buffer = coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.A2);
            var Length = coreData.GetDataFromRegister<int>(RegisterDefinition.A3);
            //var fid = coreData.mdata.Resources[FileID];
            coreData.TryQueryResourceByID(FileID, true, out var fid);
            if (fid is Stream stream)
            {
                if (coreData.mdata.MemBlocks.Count <= _Buffer.MemID)
                {
                    coreData.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.MemIDNotExist);
                    return false;
                }
                var data = coreData.mdata.MemBlocks[(int)_Buffer.MemID].Data;
                Span<byte> _buffer = stackalloc byte[Length];
                data += _Buffer.Offset;
                fixed (byte* __buffer = _buffer)
                    Buffer.MemoryCopy(data, __buffer, Length, Length);
                stream.Write(_buffer);
                //stream.Flush();
            }
            return true;
        }
        public static bool fsync(CoreData coreData)
        {
            var FileID = coreData.GetDataFromRegister<int>(RegisterDefinition.A1);

            coreData.TryQueryResourceByID(FileID, true, out var fid);
            if (fid is Stream stream)
            {
                stream.Flush();
            }
            return true;
        }
        public static void SetupSysCall(VM vm)
        {
            vm.SetSysCall(0, 3, read);
            vm.SetSysCall(0, 4, write);
            vm.SetSysCall(0, 5, open);
            vm.SetSysCall(0, 95, fsync);
        }
    }
}
