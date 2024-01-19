using System;
using System.Collections;
using System.IO;
using System.Runtime.Versioning;
using System.Xml.Linq;
using univm.core.Definitions;
using univm.core.Utilities;

namespace univm.core
{
    public sealed class VMCore : IDisposable
    {
        public CoreData coreData;
        public MachineData machineData = new MachineData();
        public VM HostMachine;
        public bool JumpFlag = false;
        public VMCore(VM hostMachine)
        {
            coreData = new CoreData(this);
            HostMachine = hostMachine;
            machineData = hostMachine.machineData;
            coreData.mdata = machineData;
        }

        public void Dispose()
        {
        }
        public unsafe void Execute(Inst inst)
        {
            switch (inst.Op_Code)
            {
                case InstOPCodes.BASE_ADD:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) + coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) - coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) * coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) / coreData.GetDataFromRegister<int>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<uint>(inst.Data1) + coreData.GetDataFromRegister<uint>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<uint>(inst.Data1) - coreData.GetDataFromRegister<uint>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<uint>(inst.Data1) * coreData.GetDataFromRegister<uint>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<uint>(inst.Data1) / coreData.GetDataFromRegister<uint>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) + ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SUB_I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) - ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_MUL_I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) * ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_DIV_I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) / ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_ADD_IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) + inst.Data2);
                    break;
                case InstOPCodes.BASE_SUB_IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) - inst.Data2);
                    break;
                case InstOPCodes.BASE_MUL_IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) * inst.Data2);
                    break;
                case InstOPCodes.BASE_DIV_IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<int>(inst.Data1) / inst.Data2);
                    break;
                case InstOPCodes.BASE_ADD_64:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) + coreData.GetDataFromRegister<long>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_64:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) - coreData.GetDataFromRegister<long>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_64:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) * coreData.GetDataFromRegister<long>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_64:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) / coreData.GetDataFromRegister<long>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_64I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) + ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SUB_64I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) - ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_MUL_64I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) * ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_DIV_64I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<long>(inst.Data1) / ((int*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_ADD_64IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) + inst.Data2);
                    break;
                case InstOPCodes.BASE_SUB_64IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) - inst.Data2);
                    break;
                case InstOPCodes.BASE_MUL_64IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) * inst.Data2);
                    break;
                case InstOPCodes.BASE_DIV_64IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) / inst.Data2);
                    break;
                case InstOPCodes.BASE_ADD_64U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) + coreData.GetDataFromRegister<ulong>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_64U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) - coreData.GetDataFromRegister<ulong>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_64U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) * coreData.GetDataFromRegister<ulong>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_64U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ulong>(inst.Data1) / coreData.GetDataFromRegister<ulong>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_16:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) + coreData.GetDataFromRegister<short>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_16:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) - coreData.GetDataFromRegister<short>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_16:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) * coreData.GetDataFromRegister<short>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_16:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) / coreData.GetDataFromRegister<short>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_16U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) + coreData.GetDataFromRegister<ushort>(inst.Data2));
                    break;
                case InstOPCodes.BASE_SUB_16U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) - coreData.GetDataFromRegister<ushort>(inst.Data2));
                    break;
                case InstOPCodes.BASE_MUL_16U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) * coreData.GetDataFromRegister<ushort>(inst.Data2));
                    break;
                case InstOPCodes.BASE_DIV_16U:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) / coreData.GetDataFromRegister<ushort>(inst.Data2));
                    break;
                case InstOPCodes.BASE_ADD_16I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) + ((short*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SUB_16I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) - ((short*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_MUL_16I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) * ((short*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_DIV_16I:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<short>(inst.Data1) / ((short*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_ADD_16IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) + ((ushort*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SUB_16IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) - ((ushort*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_MUL_16IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) * ((ushort*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_DIV_16IU:
                    coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromRegister<ushort>(inst.Data1) / ((ushort*)&inst.Data2)[0]);
                    break;
                case InstOPCodes.BASE_SET32:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1);
                    break;
                case InstOPCodes.BASE_ABS:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<int>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_B:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<sbyte>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_D:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<double>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_S:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<float>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_16:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<short>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_ABS_64:
                    coreData.SetDataToRegister(inst.Data0, Math.Abs(coreData.GetDataFromRegister<long>(inst.Data1)));
                    break;
                case InstOPCodes.BASE_CVT8:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister<sbyte>(inst.Data0, (sbyte)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CVT8U:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister<byte>(inst.Data0, (byte)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;

                case InstOPCodes.BASE_CVT16:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (short)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CVT16U:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ushort)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CVT32:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (int)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CVT32U:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (uint)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CVT64:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (long)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CVT64U:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (ulong)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CVTS:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (float)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CVTD:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<Int32>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<Int64>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<UInt32>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<UInt64>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    coreData.SetDataToRegister(inst.Data0, (double)coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_SET16:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(short));
                    break;
                case InstOPCodes.BASE_SET8:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(sbyte));
                    break;
                case InstOPCodes.BASE_SETU32:
                case InstOPCodes.BASE_SETLBL:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1);
                    break;
                case InstOPCodes.BASE_SETU16:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(short));
                    break;
                case InstOPCodes.BASE_SETS8:
                    coreData.SetDataToRegister(inst.Data0, inst.Data1, sizeof(sbyte));
                    break;
                case InstOPCodes.BASE_SET64:
                    coreData.SetDataToRegister64(inst.Data0, inst.Data1, inst.Data2);
                    break;
                case InstOPCodes.BASE_SETU64:
                    coreData.SetDataToRegister64(inst.Data0, inst.Data1, inst.Data2);
                    break;
                case InstOPCodes.BASE_SW:
                    {
                        var memptr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        memptr.Offset = (uint)(memptr.Offset + inst.Data2.BitWiseConvert<int>());
                        coreData.SetDataToMemPtr(memptr, coreData.GetDataFromRegister<int>(inst.Data0));
                    }
                    break;
                case InstOPCodes.BASE_S16:
                    {
                        var memptr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        memptr.Offset = (uint)(memptr.Offset + inst.Data2.BitWiseConvert<int>());
                        coreData.SetDataToMemPtr(memptr, coreData.GetDataFromRegister<short>(inst.Data0));
                    }
                    break;
                case InstOPCodes.BASE_S64:
                    {
                        var memptr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        memptr.Offset = (uint)(memptr.Offset + inst.Data2.BitWiseConvert<int>());
                        coreData.SetDataToMemPtr(memptr, coreData.GetDataFromRegister<long>(inst.Data0));
                    }
                    break;
                case InstOPCodes.BASE_SB:
                    {
                        var memptr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        memptr.Offset = (uint)(memptr.Offset + inst.Data2.BitWiseConvert<int>());
                        coreData.SetDataToMemPtr(memptr, coreData.GetDataFromRegister<byte>(inst.Data0));
                    }
                    break;
                case InstOPCodes.BASE_LW:
                    {
                        var memptr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        memptr.Offset = (uint)(memptr.Offset + inst.Data2.BitWiseConvert<int>());
                        coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromMemPtr<int>(memptr));
                    }
                    break;
                case InstOPCodes.BASE_LB:
                    {
                        var memptr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        memptr.Offset = (uint)(memptr.Offset + inst.Data2.BitWiseConvert<int>());
                        coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromMemPtr<byte>(memptr));
                    }
                    break;
                case InstOPCodes.BASE_LH:
                    {
                        var memptr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        memptr.Offset = (uint)(memptr.Offset + inst.Data2.BitWiseConvert<int>());
                        coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromMemPtr<short>(memptr));
                    }
                    break;
                case InstOPCodes.BASE_LD:
                    {
                        var memptr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        memptr.Offset = (uint)(memptr.Offset + inst.Data2.BitWiseConvert<int>());
                        coreData.SetDataToRegister(inst.Data0, coreData.GetDataFromMemPtr<long>(memptr));
                    }
                    break;
                case InstOPCodes.HL_ALLOC:
                    {
                        uint size = coreData.GetDataFromRegister<uint>(inst.Data1);
                        var id = coreData.Alloc(size);
                        MemPtr ptr = new MemPtr(id, 0);
                        if (machineData.MemBlocks[(int)id].Size == 0 && size != 0)
                        {
                            ptr = new MemPtr(uint.MaxValue, uint.MaxValue);
                        }
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_REALLOC:
                    {
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        uint size = coreData.GetDataFromRegister<uint>(inst.Data2);
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_PUSHD:
                    {
                        var Offset = inst.Data0;
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        int _size = (int)coreData.GetMemBlockSize(memPtr.MemID);
                        int bytes_to_copy = (int)coreData.GetDataFromRegister<uint>(inst.Data2);
                        int size = _size + bytes_to_copy;
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        else
                        {
                            if (coreData.TryGetPtr(memPtr, true, out var __ptr))
                            {
                                __ptr += _size;
                                fixed (byte* srcPtr = coreData.RegisterData)
                                {
                                    Buffer.MemoryCopy(srcPtr + (int)Offset, __ptr, bytes_to_copy, bytes_to_copy);
                                }
                            }
                        }
                    }
                    break;
                case InstOPCodes.HL_PUSHDD32:
                    {
                        var Offset = inst.Data0;
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data0);
                        int _size = (int)coreData.GetMemBlockSize(memPtr.MemID);
                        int bytes_to_copy = sizeof(int);
                        int size = _size + bytes_to_copy;
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        else
                        {
                            if (coreData.TryGetPtr(memPtr, true, out var __ptr))
                            {
                                __ptr += _size;
                                {
                                    Buffer.MemoryCopy(&inst.Data1, __ptr, bytes_to_copy, bytes_to_copy);
                                }
                            }
                        }
                    }
                    break;
                case InstOPCodes.HL_PUSHDD64:
                    {
                        var Offset = inst.Data0;
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data0);
                        int _size = (int)coreData.GetMemBlockSize(memPtr.MemID);
                        int bytes_to_copy = sizeof(ulong);
                        Span<uint> data = stackalloc uint[2];
                        data[0] = inst.Data1;
                        data[1] = inst.Data2;
                        int size = _size + bytes_to_copy;
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        else
                        {
                            if (coreData.TryGetPtr(memPtr, true, out var __ptr))
                            {
                                __ptr += _size;
                                fixed (uint* srcPtr = data)
                                {
                                    Buffer.MemoryCopy(srcPtr, __ptr, bytes_to_copy, bytes_to_copy);
                                }
                            }
                        }
                    }
                    break;
                case InstOPCodes.HL_PUSHDD16:
                    {
                        var Offset = inst.Data0;
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data0);
                        int _size = (int)coreData.GetMemBlockSize(memPtr.MemID);
                        int bytes_to_copy = sizeof(ushort);
                        int size = _size + bytes_to_copy;
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        else
                        {
                            if (coreData.TryGetPtr(memPtr, true, out var __ptr))
                            {
                                __ptr += _size;
                                {
                                    Buffer.MemoryCopy(&inst.Data1, __ptr, bytes_to_copy, bytes_to_copy);
                                }
                            }
                        }
                    }
                    break;
                case InstOPCodes.HL_PUSHDD8:
                    {
                        var Offset = inst.Data0;
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data0);
                        int _size = (int)coreData.GetMemBlockSize(memPtr.MemID);
                        int bytes_to_copy = sizeof(byte);
                        int size = _size + bytes_to_copy;
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        else
                        {
                            if (coreData.TryGetPtr(memPtr, true, out var __ptr))
                            {
                                __ptr += _size;
                                {
                                    Buffer.MemoryCopy(&inst.Data1, __ptr, bytes_to_copy, bytes_to_copy);
                                }
                            }
                        }
                    }
                    break;
                case InstOPCodes.HL_PUSHDI:
                    {
                        var Offset = inst.Data0;
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        int _size = (int)coreData.GetMemBlockSize(memPtr.MemID);
                        int bytes_to_copy = (int)inst.Data2;
                        int size = _size + bytes_to_copy;
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        if (isSuccess)
                        {
                            if (coreData.TryGetPtr(memPtr, true, out var __ptr))
                            {
                                __ptr += _size;
                                fixed (byte* srcPtr = coreData.RegisterData)
                                {
                                    Buffer.MemoryCopy(srcPtr + (int)Offset, __ptr, bytes_to_copy, bytes_to_copy);
                                }
                            }
                        }
                    }
                    break;
                case InstOPCodes.HL_POPDI:
                    {
                        var Offset = inst.Data0;
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        int _size = (int)coreData.GetMemBlockSize(memPtr.MemID);
                        int bytes_to_copy = (int)inst.Data2;
                        {
                            if (coreData.TryGetPtr(memPtr, true, out var __ptr))
                            {
                                __ptr += _size - bytes_to_copy;
                                fixed (byte* dest = coreData.RegisterData)
                                {
                                    Buffer.MemoryCopy(__ptr, dest + (int)Offset, bytes_to_copy, bytes_to_copy);
                                }
                                int size = _size - bytes_to_copy;
                                var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                            }
                        }
                    }
                    break;
                case InstOPCodes.HL_POPD:
                    {
                        var Offset = inst.Data0;
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        int _size = (int)coreData.GetMemBlockSize(memPtr.MemID);
                        int bytes_to_copy = (int)coreData.GetDataFromRegister<uint>(inst.Data2);
                        {
                            if (coreData.TryGetPtr(memPtr, true, out var __ptr))
                            {
                                __ptr += _size - bytes_to_copy;
                                fixed (byte* dest = coreData.RegisterData)
                                {
                                    Buffer.MemoryCopy(__ptr, dest + (int)Offset, bytes_to_copy, bytes_to_copy);
                                }
                                int size = _size - bytes_to_copy;
                                var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                            }
                        }
                    }
                    break;
                case InstOPCodes.HL_RRESIZE:
                    {
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        uint size = (uint)(coreData.GetMemBlockSize(memPtr.MemID) + coreData.GetDataFromRegister<int>(inst.Data2));
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_EXPAND:
                    {
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        uint size = (coreData.GetMemBlockSize(memPtr.MemID) + coreData.GetDataFromRegister<uint>(inst.Data2));
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_EXPANDI:
                    {
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        uint size = (coreData.GetMemBlockSize(memPtr.MemID) + inst.Data2);
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_SHRINK:
                    {
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        uint size = (coreData.GetMemBlockSize(memPtr.MemID) - coreData.GetDataFromRegister<uint>(inst.Data2));
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_SHRINKI:
                    {
                        MemPtr memPtr = coreData.GetDataFromRegister<MemPtr>(inst.Data1);
                        uint size = (coreData.GetMemBlockSize(memPtr.MemID) - inst.Data2);
                        var isSuccess = coreData.TryRealloc((int)memPtr.MemID, (int)size);
                        MemPtr ptr = memPtr;
                        if (!isSuccess)
                        {
                            ptr = Constants.NULL;
                        }
                        coreData.SetDataToRegister(inst.Data0, ptr);
                    }
                    break;
                case InstOPCodes.HL_FREE:
                    {
                        var PTR = coreData.GetDataFromRegister<MemPtr>(inst.Data0);
                        if (PTR.IsNotNull())
                            coreData.Free(PTR.MemID);
                    }
                    break;
                case InstOPCodes.HL_MEASURE:
                    coreData.SetDataToRegister(inst.Data1, coreData.GetMemBlockSize(coreData.GetDataFromRegister<uint>(inst.Data0)));
                    break;
                case InstOPCodes.BASE_J:
                    {
                        var frame = coreData.CallStack[^1];
                        frame.PCInAssembly = (uint)(frame.PCInAssembly + ((int*)&inst.Data0)[0]);
                        coreData.CallStack[^1] = (frame);
                    }
                    break;
                case InstOPCodes.BASE_JA:
                    {
                        var frame = coreData.CallStack[^1];
                        frame.PCInAssembly = inst.Data0;
                        coreData.CallStack[^1] = (frame);
                    }
                    break;
                case InstOPCodes.BASE_JR:
                    if (JumpFlag)
                    {
                        JumpFlag = false;
                        var frame = coreData.CallStack[^1];
                        frame.PCInAssembly = (uint)(frame.PCInAssembly + coreData.GetDataFromRegister<int>(inst.Data0));
                        coreData.CallStack[^1] = (frame);
                    }
                    break;
                case InstOPCodes.BASE_BEQ:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<byte>(inst.Data0) ==
                                                coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<short>(inst.Data0) ==
                                                coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<int>(inst.Data0) ==
                                                coreData.GetDataFromRegister<int>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<long>(inst.Data0) ==
                                                coreData.GetDataFromRegister<long>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<sbyte>(inst.Data0) ==
                                                coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ushort>(inst.Data0) ==
                                                coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<uint>(inst.Data0) ==
                                                coreData.GetDataFromRegister<uint>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ulong>(inst.Data0) ==
                                                coreData.GetDataFromRegister<ulong>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<float>(inst.Data0) ==
                                                coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<double>(inst.Data0) ==
                                                coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_BLE:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<byte>(inst.Data0) <=
                                                coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<short>(inst.Data0) <=
                                                coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<int>(inst.Data0) <=
                                                coreData.GetDataFromRegister<int>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<long>(inst.Data0) <=
                                                coreData.GetDataFromRegister<long>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<sbyte>(inst.Data0) <=
                                                coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ushort>(inst.Data0) <=
                                                coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<uint>(inst.Data0) <=
                                                coreData.GetDataFromRegister<uint>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ulong>(inst.Data0) <=
                                                coreData.GetDataFromRegister<ulong>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<float>(inst.Data0) <=
                                                coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<double>(inst.Data0) <=
                                                coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_BGE:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<byte>(inst.Data0) >=
                                                coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<short>(inst.Data0) >=
                                                coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<int>(inst.Data0) >=
                                                coreData.GetDataFromRegister<int>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<long>(inst.Data0) >=
                                                coreData.GetDataFromRegister<long>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<sbyte>(inst.Data0) >=
                                                coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ushort>(inst.Data0) >=
                                                coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<uint>(inst.Data0) >=
                                                coreData.GetDataFromRegister<uint>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ulong>(inst.Data0) >=
                                                coreData.GetDataFromRegister<ulong>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<float>(inst.Data0) >=
                                                coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<double>(inst.Data0) >=
                                                coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_BGT:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<byte>(inst.Data0) >
                                                coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<short>(inst.Data0) >
                                                coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<int>(inst.Data0) >
                                                coreData.GetDataFromRegister<int>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<long>(inst.Data0) >
                                                coreData.GetDataFromRegister<long>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<sbyte>(inst.Data0) >
                                                coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ushort>(inst.Data0) >
                                                coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<uint>(inst.Data0) >
                                                coreData.GetDataFromRegister<uint>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ulong>(inst.Data0) >
                                                coreData.GetDataFromRegister<ulong>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<float>(inst.Data0) >
                                                coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<double>(inst.Data0) >
                                                coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_BLT:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<byte>(inst.Data0) <
                                                coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<short>(inst.Data0) <
                                                coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<int>(inst.Data0) <
                                                coreData.GetDataFromRegister<int>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<long>(inst.Data0) <
                                                coreData.GetDataFromRegister<long>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<sbyte>(inst.Data0) <
                                                coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ushort>(inst.Data0) <
                                                coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<uint>(inst.Data0) <
                                                coreData.GetDataFromRegister<uint>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ulong>(inst.Data0) <
                                                coreData.GetDataFromRegister<ulong>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<float>(inst.Data0) <
                                                coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<double>(inst.Data0) <
                                                coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_BNE:
                    {
                        var type = inst.Data2;
                        switch (type)
                        {
                            case 0:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<byte>(inst.Data0) !=
                                                coreData.GetDataFromRegister<byte>(inst.Data1));
                                }
                                break;
                            case 1:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<short>(inst.Data0) !=
                                                coreData.GetDataFromRegister<short>(inst.Data1));
                                }
                                break;
                            case 2:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<int>(inst.Data0) !=
                                                coreData.GetDataFromRegister<int>(inst.Data1));
                                }
                                break;
                            case 3:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<long>(inst.Data0) !=
                                                coreData.GetDataFromRegister<long>(inst.Data1));
                                }
                                break;
                            case 4:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<sbyte>(inst.Data0) !=
                                                coreData.GetDataFromRegister<sbyte>(inst.Data1));
                                }
                                break;
                            case 5:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ushort>(inst.Data0) !=
                                                coreData.GetDataFromRegister<ushort>(inst.Data1));
                                }
                                break;
                            case 6:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<uint>(inst.Data0) !=
                                                coreData.GetDataFromRegister<uint>(inst.Data1));
                                }
                                break;
                            case 7:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<ulong>(inst.Data0) !=
                                                coreData.GetDataFromRegister<ulong>(inst.Data1));
                                }
                                break;
                            case 8:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<float>(inst.Data0) !=
                                                coreData.GetDataFromRegister<float>(inst.Data1));
                                }
                                break;
                            case 9:
                                {
                                    JumpFlag = (coreData.GetDataFromRegister<double>(inst.Data0) !=
                                                coreData.GetDataFromRegister<double>(inst.Data1));
                                }
                                break;
                            default:
                                break;
                        }
                    }
                    break;
                case InstOPCodes.BASE_CJ:
                    if (JumpFlag)
                    {
                        JumpFlag = false;
                        var frame = coreData.CallStack[^1];
                        frame.PCInAssembly = (uint)(frame.PCInAssembly + ((int*)&inst.Data0)[0]);
                        coreData.CallStack[^1] = (frame);
                    }
                    break;
                case InstOPCodes.BASE_CJA:
                    if (JumpFlag)
                    {
                        JumpFlag = false;
                        var frame = coreData.CallStack[^1];
                        frame.PCInAssembly = inst.Data0;
                        coreData.CallStack[^1] = (frame);
                    }
                    break;
                case InstOPCodes.BASE_CJR:
                    {
                        var frame = coreData.CallStack[^1];
                        frame.PCInAssembly = (uint)(frame.PCInAssembly + coreData.GetDataFromRegister<int>(inst.Data0));
                        coreData.CallStack[^1] = (frame);
                    }
                    break;
                case InstOPCodes.BASE_CALL:
                    {
                        var frame = coreData.CallStack[^1];
                        {
                            frame.StackSize = coreData.CurrentStackSize;
                            coreData.CallStack[^1] = (frame);
                        }
                        {
                            CallStackItem callStackItem = new CallStackItem();
                            callStackItem.AssemblyID = frame.AssemblyID;
                            callStackItem.PCInAssembly = inst.Data0;
                            coreData.CallStack.Add(callStackItem);
                        }
                    }
                    break;
                case InstOPCodes.BASE_CALLP:
                    {
                        var frame = coreData.CallStack[^1];
                        {
                            this.HostMachine.CallParallel(frame.AssemblyID, inst.Data0);
                        }
                    }
                    break;
                case InstOPCodes.BASE_CALLR:
                    {
                        var frame = coreData.CallStack[^1];
                        {
                            frame.StackSize = coreData.CurrentStackSize;
                            coreData.CallStack[^1] = (frame);
                        }
                        {
                            CallStackItem callStackItem = new CallStackItem();
                            callStackItem.AssemblyID = frame.AssemblyID;
                            callStackItem.PCInAssembly = coreData.GetDataFromRegister<uint>(inst.Data0);
                            coreData.CallStack.Add(callStackItem);
                        }
                    }
                    break;
                case InstOPCodes.BASE_RET:
                    {
                        if (coreData.CallStack.Count == 1)
                        {
                            coreData.CallStack.Clear();
                        }
                        else
                        {
                            var frame = coreData.CallStack[^2];
                            uint NewSize = ((frame.StackSize / Constants.StackBlockSize) + 1) * Constants.StackBlockSize;
                            coreData.Realloc(0, (int)NewSize);
                            coreData.CurrentStackSize = frame.StackSize;
                            coreData.CallStack.RemoveAt(coreData.CallStack.Count - 1);
                        }
                    }
                    break;
                case InstOPCodes.HL_MAP_GLBMEM:
                    {
                        var frame = coreData.CallStack[^1];
                        coreData.SetDataToRegister<MemPtr>(inst.Data0, new MemPtr { MemID = machineData.assemblies[(int)frame.AssemblyID].GlobalMemPtr, Offset = inst.Data1 });
                    }
                    break;
                case InstOPCodes.HL_QTEXT:
                    {
                        var frame = coreData.CallStack[^1];
                        var Texts = machineData.assemblies[(int)frame.AssemblyID].Texts;
                        if (Texts != null)
                        {
                            var text = Texts[(int)inst.Data1];
                            var ptr = coreData.Alloc(text.Length);
                            {

                                byte* SrcPtr = text.Data;
                                byte* DestPtr = machineData.MemBlocks[(int)ptr].Data;
                                try
                                {
                                    Buffer.MemoryCopy(SrcPtr, DestPtr, text.Length, text.Length);
                                    coreData.SetDataToRegister(inst.Data0, new MemPtr { MemID = ptr, Offset = 0 });
                                }
                                catch (ArgumentOutOfRangeException)
                                {
                                    coreData.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.OutOfBoundary);
                                }
                                catch (Exception)
                                {
                                    coreData.SetDataToRegister(RegisterDefinition.ERRNO, ErrNos.Unknown);
                                }

                            }
                        }
                    }
                    break;
                case InstOPCodes.BASE_SYSCALL:
                    machineData.SysCall(inst.Data0, inst.Data1, coreData);
                    break;
                case InstOPCodes.BASE_SYSCALLR:
                    machineData.SysCall(coreData.GetDataFromRegister<uint>(inst.Data0), coreData.GetDataFromRegister<uint>(inst.Data0), coreData);
                    break;
                case InstOPCodes.BASE_SYSCALL_TEST:
                    coreData.SetDataToRegister(inst.Data2, machineData.IsSysCallExist(inst.Data0, inst.Data1) ? 1 : 0);
                    break;
                case InstOPCodes.BASE_SYSCALL_TESTR:
                    coreData.SetDataToRegister(inst.Data2, machineData.IsSysCallExist(coreData.GetDataFromRegister<uint>(inst.Data0), coreData.GetDataFromRegister<uint>(inst.Data0)) ? 1 : 0);
                    break;
                case InstOPCodes.DEBUG_COREDUMP:
                    {
                        using FileStream stream = File.OpenWrite("coredump");
                        stream.SetLength(0);
                        HostMachine.DumpBinary(stream);
                        stream.Flush();
                    }
                    break;
                case InstOPCodes.DEBUG_COREDUMPRES:
                    {
                        if (coreData.TryQueryResourceByID(coreData.GetDataFromRegister<int>(inst.Data0), true, out var fid))
                        {
                            if (fid is Stream stream)
                            {
                                stream.SetLength(0);
                                HostMachine.DumpBinary(stream);
                                stream.Flush();
                            }
                        }
                    }
                    break;
                case InstOPCodes.DEBUG_COREDUMPTEXTRES:
                    {
                        if (coreData.TryQueryResourceByID(coreData.GetDataFromRegister<int>(inst.Data0), true, out var fid))
                        {
                            if (fid is Stream stream)
                            {
                                StreamWriter writer = new StreamWriter(stream);
                                HostMachine.DumpText(writer);
                                stream.Flush();
                            }
                        }
                    }
                    break;
                case InstOPCodes.DEBUG_COREDUMPTEXT:
                    {
                        using FileStream stream = File.OpenWrite("coredump");
                        stream.SetLength(0);
                        using StreamWriter writer = new StreamWriter(stream);
                        HostMachine.DumpText(writer);
                        stream.Flush();
                    }
                    break;
                default:
                    break;
            }
        }
        public void Call(CallStackItem frame)
        {
            coreData.CallStack.Add(frame);
            Run();
        }
        internal bool WillContinue = true;

        public void Run()
        {
            while (WillContinue)
            {

                var frame = coreData.CallStack[coreData.CallStack.Count - 1];
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                var inst = machineData.assemblies[(int)frame.AssemblyID].Instructions[frame.PCInAssembly];
                frame.PCInAssembly++;
                coreData.CallStack[coreData.CallStack.Count - 1] = (frame);
                Execute(inst);
                if (coreData.CallStack.Count == 0)
                    break;
                frame = coreData.CallStack[coreData.CallStack.Count - 1];
                if (machineData.assemblies[(int)frame.AssemblyID].Instructions.Length <= frame.PCInAssembly)
                {
                    return;
                }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }
    }
}