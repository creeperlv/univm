using System;
using System.IO;
using System.Text;
using univm.core;

namespace univm.syscalls
{
    public static class vfs_syscalls
    {
        public unsafe static bool chdir(CoreData coreData)
        {
            var PathPtr = coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.A1);
            if (coreData.TryGetMemBlockFromPtr(PathPtr, true, out var blk))
            {
                Span<byte> buf = stackalloc byte[(int)blk.Size];
                fixed (byte* ptr = buf)
                {
                    Buffer.MemoryCopy(blk.Data, ptr, blk.Size, blk.Size);
                }
                Environment.CurrentDirectory = Encoding.UTF8.GetString(buf);
            }
            return true;
        }
        public unsafe static bool mkdir(CoreData coreData)
        {
            var PathPtr = coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.A1);
            if (coreData.TryGetMemBlockFromPtr(PathPtr, true, out var blk))
            {
                Span<byte> buf = stackalloc byte[(int)blk.Size];
                fixed (byte* ptr = buf)
                {
                    Buffer.MemoryCopy(blk.Data, ptr, blk.Size, blk.Size);
                }
                Directory.CreateDirectory(Encoding.UTF8.GetString(buf));
            }
            return true;
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
            fixed (byte* buf = buffer)
            {
                coreData.GetDataFromMemPtr(buf, PathPtr, BufLen);
            }
            string path = Encoding.UTF8.GetString(buffer);
            if (Directory.Exists(path))
            {
                var id = coreData.mdata.AddResource(new DirectoryDscriptor(path));
                coreData.SetDataToRegister(RegisterDefinition.A0, id);
            }
            else
            {
                var stream = File.Open(path, mode, access, share);
                var id = coreData.mdata.AddResource(stream);
                coreData.SetDataToRegister(RegisterDefinition.A0, id);

            }

            return false;
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
    }
}
