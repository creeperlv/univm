using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using univm.core.Utilities;

namespace univm.core
{


    public unsafe class UniVMAssembly : IDisposable
    {
        public byte*[]? Texts;
        public string[]? Libraries;
        public Inst[]? Instructions;

        public void Dispose()
        {
            if (Texts != null)
            {

                foreach (var ptr in Texts)
                {
                    Marshal.FreeHGlobal((IntPtr)ptr);
                }
            }
        }
        public static UniVMAssembly Read(Stream stream)
        {
            Span<byte> buffer4 = stackalloc byte[4];
            Span<byte> buffer_InstOP_Code = stackalloc byte[sizeof(uint) * 4];
            uint TextCount = 0;
            uint Value0 = 0;//Text Length
            uint InstCount = 0;
            uint LibraryCount = 0;
            stream.ReadUInt32(buffer4, out TextCount);
            stream.ReadUInt32(buffer4, out LibraryCount);
            stream.ReadUInt32(buffer4, out InstCount);
            byte*[] Texts = new byte*[TextCount];
            for (int i = 0; i < TextCount; i++)
            {
                stream.ReadUInt32(buffer4, out Value0);
                var data = (byte*)Marshal.AllocHGlobal((int)Value0);
                Texts[i] = data;
                int d = default;
                byte* target = (byte*)data;
                for (int index = 0; index < Value0; index++)
                {
                    if ((d = stream.ReadByte()) != -1)
                    {
                        target[index] = (byte)d;
                    }
                }
            }
            string[] Libraries = new string[LibraryCount];
            for (int i = 0; i < LibraryCount; i++)
            {
                stream.ReadUInt32(buffer4, out Value0);
                Span<byte> b = stackalloc byte[(int)Value0];
                stream.Read(b);
                Libraries[i] = Encoding.UTF8.GetString(b);
            }
            Inst[] insts = new Inst[InstCount];
            for (int i = 0; i < InstCount; i++)
            {
                Inst inst = new Inst();
                stream.Read(buffer_InstOP_Code);
                Inst* inst_ptr = &inst;
                fixed (byte* b = buffer_InstOP_Code)
                {
                    inst_ptr[0] = ((Inst*)b)[0];
                }
                insts[i] = inst;
            }

            UniVMAssembly uniVMAssembly = new UniVMAssembly() { Texts = Texts, Libraries = Libraries, Instructions = insts };

            return uniVMAssembly;
        }
    }
    public struct CallStackItem
    {
        public uint AssemblyID;
        public uint PCInAssembly;
    }
    public delegate bool SysCall(CoreData arg);
    [StructLayout(LayoutKind.Sequential)]
    public struct MemPtr
    {
        public uint MemID;
        public uint Offset;
        public MemPtr(uint memID, uint offset)
        {
            MemID = memID;
            Offset = offset;
        }
    }
    public unsafe struct MemBlock
    {
        public uint Size;
        public byte* Data;
        public MemBlock(uint Size, byte* data)
        {
            this.Size = Size;
            this.Data = data;
        }
        public unsafe T GetDataFromRegister<T>(uint Offset) where T : unmanaged
        {
            T t;
            {
                byte* _b = Data + Offset;
                T* _t = &t;
                _t[0] = ((T*)_b)[0];
            }
            return t;
        }
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct Inst
    {
        public uint Op_Code;
        public uint Data0;
        public uint Data1;
        public uint Data2;
    }
}