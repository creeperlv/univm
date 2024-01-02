using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace univm.cli.core
{
    public class CLIProcessor
    {
        public bool UnknownParametersAsMainParameters;
        public bool AllowMultipleMainParameters;
        public bool AllowOverwriteMainPatametersWhenOnlyOneIsAllowed;
        public string MainParameterPlaceholder = "file0";
        //public required Func<T> CreateOptionInstance;
        public List<Parameter> ParameterOptions = new List<Parameter>();
        public void PrintParameters(TextWriter writer, int Depth)
        {
            writer.Write(Process.GetCurrentProcess().ProcessName);
            writer.Write(" ");
            if (ParameterOptions.Count > 0)
            {
                writer.Write("<options> ");
            }
            if (UnknownParametersAsMainParameters)
            {
                writer.Write(MainParameterPlaceholder);
            }
            writer.WriteLine();
            writer.WriteLine();
            writer.WriteLine("Options:");
            writer.WriteLine();
            foreach (var item in ParameterOptions)
            {
                for (int i = 0; i < Depth; i++)
                {
                    writer.Write("\t");
                }
                writer.Write(item.MainName);
                if (item.AcceptValues)
                {
                    writer.Write(' ');
                    if (item.RequireValues)
                    {
                        writer.Write("<");
                    }
                    else
                    {
                        writer.Write("[");
                    }
                    writer.Write($"{item.ValuePlaceHolder ?? "value"}");

                    if (item.RequireValues)
                    {
                        writer.Write(">");
                    }
                    else
                    {
                        writer.Write("]");
                    }
                }
                writer.WriteLine();
                for (int i = 0; i < item.Aliases.Count; i++)
                {
                    string? alias = item.Aliases[i];
                    writer.Write(alias);
                    if (i != item.Aliases.Count - 1)
                        writer.Write(',');
                }
                if (item.Aliases.Count > 0)
                {
                    writer.WriteLine();
                }

                for (int i = 0; i < Depth + 1; i++)
                {
                    writer.Write("\t");
                }
                writer.WriteLine(item.Description);
            }
        }
    }
}
