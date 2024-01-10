using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using univm.core.Utilities;

namespace univm.core
{

    public unsafe struct TextItem
    {
        public uint Length;
        public byte* Data;
    }
    public unsafe class UniVMAssembly : IDisposable
    {
        public uint GlobalMemSize = 0;
        [NonSerialized]
        public uint GlobalMemPtr = 0;
        public Dictionary<uint, int> RuntimeLibraryMap = new Dictionary<uint, int>();
        public TextItem[]? Texts;
        public string[]? Libraries;
        public Inst[]? Instructions;
        public void Dispose()
        {
            if (Texts != null)
            {

                foreach (var ptr in Texts)
                {
                    Marshal.FreeHGlobal((IntPtr)ptr.Data);
                }
            }
        }
        public static void Write(Stream stream, UniVMAssembly asm)
        {
            stream.WriteData((uint)(asm.Texts?.Length ?? 0));
            stream.WriteData((uint)(asm.Libraries?.Length ?? 0));
            stream.WriteData((uint)(asm.Instructions?.Length ?? 0));
            stream.WriteData(asm.GlobalMemSize);
            if (asm.Texts != null)
                foreach (var item in asm.Texts)
                {
                    stream.WriteData(item.Length);
                    stream.WritePointer(item.Data, item.Length);
                }
            if (asm.Libraries != null)
                foreach (var item in asm.Libraries)
                {
                    var b = Encoding.UTF8.GetBytes(item);
                    stream.WriteData(b.Length);
                    stream.Write(b);
                }
            if (asm.Instructions != null)
            {
                Span<byte> buffer = stackalloc byte[sizeof(Inst)];
                foreach (var item in asm.Instructions)
                {
                    fixed (byte* ptr = buffer)
                    {
                        ((Inst*)ptr)[0] = item;
                        stream.Write(buffer);
                    }
                }
            }
            stream.Flush();
        }
        public static UniVMAssembly Read(Stream stream)
        {
            Span<byte> buffer4 = stackalloc byte[4];
            Span<byte> buffer_InstOP_Code = stackalloc byte[sizeof(uint) * 4];
            uint Value0 = 0;//Text Length
            stream.ReadUInt32(buffer4, out uint TextCount);
            stream.ReadUInt32(buffer4, out uint LibraryCount);
            stream.ReadUInt32(buffer4, out uint InstCount);
            stream.ReadUInt32(buffer4, out uint GlobalMemSize);
            TextItem[] Texts = new TextItem[TextCount];
            for (int i = 0; i < TextCount; i++)
            {
                stream.ReadUInt32(buffer4, out Value0);
                var data = (byte*)Marshal.AllocHGlobal((int)(uint)0);
                Texts[i].Length = 0;
                Texts[i].Data = data;
                int d = default;
                byte* target = (byte*)data;
                for (int index = 0; index < (uint)0; index++)
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
                Span<byte> b = stackalloc byte[(int)(uint)0];
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

            UniVMAssembly uniVMAssembly = new UniVMAssembly() { Texts = Texts, Libraries = Libraries, Instructions = insts, GlobalMemSize = GlobalMemSize };

            return uniVMAssembly;
        }
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IsNull() => MemID == uint.MaxValue && Offset == uint.MaxValue;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly bool IsNotNull() => MemID != uint.MaxValue || Offset != uint.MaxValue;
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public readonly unsafe T GetDataFromRegister<T>(uint Offset) where T : unmanaged
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