using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using univm.core.Utilities;

namespace univmc.core
{
    [Serializable]
    public class AssemblyDefinition
    {
        public bool IsStaticAssembly;
        public string? AssemblyID;
        public Dictionary<string, string> Texts = new Dictionary<string, string>();
        public Dictionary<string, string> Constants = new Dictionary<string, string>();
        public Dictionary<string, uint> Labels = new Dictionary<string, uint>();
        public Dictionary<string, uint> Globals = new Dictionary<string, uint>();
        public static void WriteToStream(Stream stream, AssemblyDefinition definition)
        {
            stream.WriteData(definition.IsStaticAssembly);
            if (definition.IsStaticAssembly)
            {
                if (definition.AssemblyID != null)
                {
                    var data = Encoding.UTF8.GetBytes(definition.AssemblyID);
                    stream.WriteData(data.Length);
                    stream.Write(data);
                }
                else
                {
                    stream.WriteData(0);
                }
            }
            stream.WriteData((uint)definition.Texts.Count);
            stream.WriteData((uint)definition.Constants.Count);
            stream.WriteData((uint)definition.Labels.Count);
            stream.WriteData((uint)definition.Globals.Count);
            foreach (var item in definition.Texts)
            {
                var buf0 = Encoding.UTF8.GetBytes(item.Key);
                var buf1 = Encoding.UTF8.GetBytes(item.Value);
                stream.WriteData(buf0.Length);
                stream.Write(buf0);
                stream.WriteData(buf1.Length);
                stream.Write(buf1);
            }
            foreach (var item in definition.Constants)
            {
                var buf0 = Encoding.UTF8.GetBytes(item.Key);
                var buf1 = Encoding.UTF8.GetBytes(item.Value);
                stream.WriteData(buf0.Length);
                stream.Write(buf0);
                stream.WriteData(buf1.Length);
                stream.Write(buf1);
            }
            foreach (var item in definition.Labels)
            {
                var buf0 = Encoding.UTF8.GetBytes(item.Key);
                stream.WriteData(buf0.Length);
                stream.Write(buf0);
                stream.WriteData(item.Value);
            }
            foreach (var item in definition.Globals)
            {
                var buf0 = Encoding.UTF8.GetBytes(item.Key);
                stream.WriteData(buf0.Length);
                stream.Write(buf0);
                stream.WriteData(item.Value);
            }
        }
        public static AssemblyDefinition LoadFromStream(Stream stream)
        {
            AssemblyDefinition def = new AssemblyDefinition();
            stream.ReadData(out def.IsStaticAssembly);

            if (def.IsStaticAssembly)
            {
                stream.ReadData(out int ID_Len);
                Span<byte> buffer = stackalloc byte[ID_Len];
                stream.Read(buffer);
                def.AssemblyID = Encoding.UTF8.GetString(buffer);
            }
            stream.ReadData(out int TextCount);
            stream.ReadData(out int ConstantsCount);
            stream.ReadData(out int LabelCount);
            stream.ReadData(out int GlobalsCount);
            for (int i = 0; i < TextCount; i++)
            {
                stream.ReadData(out int LLen);
                stream.ReadData(out int TLen);
                Span<byte> Span0 = stackalloc byte[LLen];
                Span<byte> Span1 = stackalloc byte[TLen];
                stream.Read(Span0);
                stream.Read(Span1);
                def.Texts.Add(Encoding.UTF8.GetString(Span0), Encoding.UTF8.GetString(Span1));
            }
            for (int i = 0; i < ConstantsCount; i++)
            {
                stream.ReadData(out int LLen);
                stream.ReadData(out int TLen);
                Span<byte> Span0 = stackalloc byte[LLen];
                Span<byte> Span1 = stackalloc byte[TLen];
                stream.Read(Span0);
                stream.Read(Span1);
                def.Constants.Add(Encoding.UTF8.GetString(Span0), Encoding.UTF8.GetString(Span1));
            }
            for (int i = 0; i < LabelCount; i++)
            {
                stream.ReadData(out int LLen);
                Span<byte> Span0 = stackalloc byte[LLen];
                stream.ReadData(out uint Value);
                def.Labels.Add(Encoding.UTF8.GetString(Span0), Value);

            }
            for (int i = 0; i < GlobalsCount; i++)
            {
                stream.ReadData(out int LLen);
                Span<byte> Span0 = stackalloc byte[LLen];
                stream.ReadData(out uint Value);
                def.Globals.Add(Encoding.UTF8.GetString(Span0), Value);
            }
            return def;
        }
    }
}
