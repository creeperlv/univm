using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace univm.core.Utilities
{
    public static class StreamExtensions
    {
        public static unsafe void WritePointer(this Stream stream, byte* pointer, uint length)
        {
            for (int i = 0; i < length; i++)
            {
                stream.WriteByte(pointer[i]);
            }
        }
        public static unsafe void WriteData<T>(this Stream stream, T data) where T : unmanaged
        {
            int size = sizeof(T);
            Span<byte> buffer = stackalloc byte[size];
            fixed (byte* ptr = buffer)
            {
                T* _t = (T*)ptr;
                _t[0] = data;
            }
            stream.Write(buffer);
        }
        public static unsafe bool ReadData<T>(this Stream stream, out T data) where T : unmanaged
        {
            int size = sizeof(T);
            Span<byte> buffer = stackalloc byte[size];

            if (stream.Read(buffer) == size)
            {
                fixed (byte* ptr = buffer)
                {
                    data = ((T*)ptr)[0];
                }
                return true;
            }
            data = default;
            return false;
        }
        public static unsafe bool ReadInt32(this Stream stream, Span<byte> buffer, out int data)
        {
            if (stream.Read(buffer) == 4)
            {
                fixed (byte* ptr = buffer)
                {
                    data = ((int*)ptr)[0];
                }
                return true;
            }
            data = 0;
            return false;
        }
        public static unsafe bool ReadUInt32(this Stream stream, Span<byte> buffer, out uint data)
        {
            if (stream.Read(buffer) == 4)
            {
                fixed (byte* ptr = buffer)
                {
                    data = ((uint*)ptr)[0];
                }
                return true;
            }
            data = 0;
            return false;
        }
    }
    public static class UIntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static T BitWiseConvert<T>(this uint value) where T : unmanaged
        {
            return ((T*)&value)[0];
        }
    }
    public static class IntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static T BitWiseConvert<T>(this int value) where T : unmanaged
        {
            return ((T*)&value)[0];
        }
    }
    public static class ByteExtensions
    {
        static char OneDIGIT(int value)
        {
            if (value < 10) return (char)(value + '0');
            else return (char)(value - 10 + 'A');
        }
        static char OneDigit(int value)
        {
            if (value < 0xA) return (char)(value + '0');
            else return (char)(value - 110 + 'a');
        }
        public static string ToHEX(this byte value)
        {
            char HIGH = OneDIGIT(value / 0x10);
            return $"{HIGH}{OneDIGIT(value % 0x10)}";
        }
        public static string ToHex(this byte value)
        {
            return $"{OneDigit(value / 0x10)}{OneDigit(value % 0x10)}";
        }
    }
}
