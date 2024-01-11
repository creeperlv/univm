using System;
using System.IO;
using univm.core;
using univm.core.Utilities;

namespace univm.syscalls
{
    public static class DefaultSysCalls
    {
        public static bool read(CoreData coreData)
        {
            return false;
        }
        public unsafe static bool write(CoreData coreData)
        {
            var FileID = coreData.GetDataFromRegister<int>(RegisterDefinition.A1);
            var _Buffer = coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.A2);
            var Length = coreData.GetDataFromRegister<int>(RegisterDefinition.A3);
            var fid = coreData.mdata.Resources[FileID];
            if (fid is Stream stream)
            {
                var data = coreData.mdata.MemBlocks[(int)_Buffer.MemID].Data;
                Span<byte> _buffer = stackalloc byte[Length];
                data += _Buffer.Offset;
                fixed (byte* __buffer = _buffer)
                    Buffer.MemoryCopy(data, __buffer, Length, Length);
                stream.Write(_buffer);
                stream.Flush();
            }
            return false;
        }
        public static void SetupSysCall(VM vm)
        {
            vm.SetSysCall(0, 63, read);
            vm.SetSysCall(0, 64, write);
        }
    }
}
