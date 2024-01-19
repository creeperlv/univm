using System;
using System.IO;
using System.Runtime.InteropServices;
using univm.core;
using univm.core.Definitions;

namespace univm.syscalls
{
    public static class sys_generic
    {
        public unsafe static bool read(CoreData coreData)
        {
            var FileID = coreData.GetDataFromRegister<int>(RegisterDefinition.A1);
            var _Buffer = coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.A2);
            var Length = coreData.GetDataFromRegister<int>(RegisterDefinition.A3);
            coreData.TryQueryResourceByID(FileID, true, out var fid);
            if (fid is Stream stream)
            {
                int ID = (int)_Buffer.MemID;
                if (coreData.mdata.MemBlocks.Count > ID)
                {
                    var size = coreData.GetMemBlockSize(_Buffer);
                    byte* target = coreData.mdata.MemBlocks[ID].Data;
                    target += _Buffer.Offset;
                    Span<byte> bytes = new Span<byte>(target, (int)size);
                    int offset = stream.Read(bytes);
                    coreData.SetDataToRegister(RegisterDefinition.A0, (uint)offset);
                }
            }
            else
            {
                coreData.SetDataToRegister(RegisterDefinition.A0, -1);
                coreData.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.EBADF);
            }
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
                data += _Buffer.Offset;
                Span<byte> _buffer = new Span<byte>(data, Length);
                //fixed (byte* __buffer = _buffer)
                //    Buffer.MemoryCopy(data, __buffer, Length, Length);
                stream.Write(_buffer);
                coreData.SetDataToRegister(RegisterDefinition.A0, Length);
            }
            else
            {
                coreData.SetDataToRegister(RegisterDefinition.A0, -1);
                coreData.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.EBADF);
            }
            return true;
        }
    }
}
