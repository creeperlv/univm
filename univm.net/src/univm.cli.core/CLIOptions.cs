using LibCLCC.NET.Operations;
using System;
using System.Collections.Generic;
using univm.cli.core.Errors;
using univm.core.Utilities;

namespace univm.cli.core
{
    public class Parameter
    {
        public string MainName;
        public List<string> Aliases;
        public string Description = "";
        public string? ValuePlaceHolder = null;
        public bool AcceptValues;
        public bool RequireValues;
        public bool KeyAsPartOfName;
        public Func<string, bool> Validation = (_) => true;
        public string? Value = null;

        public Parameter(string mainName, List<string> aliases)
        {
            MainName = mainName;
            Aliases = aliases;
        }
    }
    public class CLIOptions
    {
        public bool ShowHelp;
        public bool ShowVersion;
        public Dictionary<string, string> Options = new Dictionary<string, string>();
        public List<string> MainParameters = new List<string>();
        public static OperationResult<CLIOptions> ParseFromStringArray(string[] args, CLIProcessor processor)
        {
            OperationResult<CLIOptions> result = new CLIOptions();
            if (args.Length == 0)
            {
                result.Result.ShowHelp = true;
                return result;
            }
            for (int i = 0; i < args.Length; i++)
            {
                string? item = args[i];
                foreach (var para in processor.ParameterOptions)
                {
                    if (para.KeyAsPartOfName == false && para.AcceptValues == false)
                    {
                        if (item == (para.MainName))
                        {
                            result.Result.Options.Set(para.MainName, "1");
                            goto EndOfIteration;
                        }
                        foreach (var name in para.Aliases)
                        {
                            if (item == (name))
                            {
                                result.Result.Options.Set(para.MainName, "1");
                                goto EndOfIteration;
                            }
                        }
                    }
                    bool Hit = false;
                    if (para.KeyAsPartOfName || para.AcceptValues)
                    {
                        if (item.StartsWith(para.MainName))
                        {
                            Hit = true;
                            goto __hit_stage_2;
                        }
                        for (int i1 = 0; i1 < para.Aliases.Count; i1++)
                        {
                            string? name = para.Aliases[i1];
                            if (item.StartsWith(name))
                            {
                                Hit = true;
                                goto __hit_stage_2;
                            }
                        }
                    }
                __hit_stage_2:
                    if (Hit)
                    {

                        if (para.AcceptValues && !para.KeyAsPartOfName)
                        {
                            if (item.StartsWith(para.MainName))
                            {
                                if (item != para.MainName)
                                {
                                    if (item[para.MainName.Length] != '=')
                                    {
                                        Hit = false;
                                        goto EndOfParameterCheck;
                                    }
                                    else
                                    {
                                        result.Result.Options.Set(para.MainName, item[(para.MainName.Length + 1)..]);
                                        goto EndOfIteration;
                                    }
                                }
                                else
                                {
                                    if (i + 1 < args.Length)
                                    {

                                        i++;
                                        var value = args[i];
                                        result.Result.Options.Set(para.MainName, value);
                                        goto EndOfIteration;
                                    }
                                    else if (para.RequireValues)
                                    {
                                        result.AddError(new RequireValueError(item));
                                        return result;
                                    }
                                    else
                                    {
                                        result.Result.Options.Set(para.MainName, "");
                                    }
                                }
                            }
                            for (int i1 = 0; i1 < para.Aliases.Count; i1++)
                            {
                                string? name = para.Aliases[i1];
                                if (item.StartsWith(name))
                                {
                                    if (item != name)
                                    {
                                        if (item[name.Length] != '=')
                                        {
                                            Hit = false;
                                            goto EndOfParameterCheck;
                                        }
                                        else
                                        {
                                            result.Result.Options.Set(para.MainName, item[(name.Length + 1)..]);
                                            goto EndOfIteration;
                                        }
                                    }
                                }
                                else
                                {
                                    if (i + 1 < args.Length)
                                    {

                                        i++;
                                        var value = args[i];
                                        result.Result.Options.Set(para.MainName, value);
                                        goto EndOfIteration;
                                    }
                                    else if (para.RequireValues)
                                    {
                                        result.AddError(new RequireValueError(item));
                                        return result;
                                    }
                                    else
                                    {
                                        result.Result.Options.Set(para.MainName, "");
                                    }
                                }
                            }
                        }
                        else if (para.AcceptValues && para.KeyAsPartOfName)
                        {
                            if (item.StartsWith(para.MainName))
                            {
                                int SQL_EQ = item.IndexOf('=');
                                if (SQL_EQ > 0)
                                {
                                    result.Result.Options.Set(item.Substring(para.MainName.Length, SQL_EQ - para.MainName.Length),
                                        item[(SQL_EQ + 1)..]);
                                    goto EndOfIteration;
                                }
                                else
                                {
                                    if (i + 1 < args.Length)
                                    {
                                        i++;
                                        var value = args[i];
                                        result.Result.Options.Set(item.Substring(para.MainName.Length), value);
                                        goto EndOfIteration;
                                    }
                                    else if (para.RequireValues)
                                    {
                                        result.AddError(new RequireValueError(item));
                                        return result;
                                    }
                                    else
                                    {
                                        result.Result.Options.Set(item.Substring(para.MainName.Length), "");
                                        goto EndOfIteration;
                                    }

                                }
                            }
                            for (int i1 = 0; i1 < para.Aliases.Count; i1++)
                            {
                                string? name = para.Aliases[i1];
                                if (item.StartsWith(name))
                                {
                                    int SQL_EQ = item.IndexOf('=');
                                    if (SQL_EQ > 0)
                                    {
                                        result.Result.Options.Set(item.Substring(name.Length, SQL_EQ - name.Length),
                                            item[(SQL_EQ + 1)..]);
                                        goto EndOfIteration;
                                    }
                                    else
                                    {

                                        if (i + 1 < args.Length)
                                        {

                                            i++;
                                            var value = args[i];
                                            result.Result.Options.Set(item.Substring(name.Length), value);
                                            goto EndOfIteration;
                                        }
                                        else if (para.RequireValues)
                                        {
                                            result.AddError(new RequireValueError(item));
                                            return result;
                                        }
                                        else
                                        {
                                            result.Result.Options.Set(item.Substring(name.Length), "");
                                            goto EndOfIteration;
                                        }
                                    }
                                }
                            }
                        }
                    }
                EndOfParameterCheck:;
                }
                if (processor.UnknownParametersAsMainParameters)
                {
                    if (result.Result.MainParameters.Count > 0)
                    {
                        if (processor.AllowMultipleMainParameters)
                        {
                            result.Result.MainParameters.Add(item);
                        }
                        else if (processor.AllowOverwriteMainPatametersWhenOnlyOneIsAllowed)
                        {
                            result.Result.MainParameters[0] = item;
                        }
                        else
                        {
                            result.AddError(new UnknownParameterError(item));
                        }
                    }
                    else
                        result.Result.MainParameters.Add(item);
                }
                else
                    result.AddError(new UnknownParameterError(item));
                EndOfIteration:;
            }
            return result;
        }
    }
}
