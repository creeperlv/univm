using univm.core;
using univm.syscalls;

namespace univm;

class Program
{
    static void Main(string[] args)
    {
        using CoreData runtimeData = new CoreData();
        DefaultSysCalls.SetupSysCall(runtimeData);
        runtimeData.SetDataToRegister(0, 123);
        Console.WriteLine(runtimeData.GetDataFromRegister<int>(0));
        runtimeData.SetDataToRegister(1, 123);
        Console.WriteLine(runtimeData.GetDataFromRegister<int>(0));
        runtimeData.SetDataToRegister(0, 1.025f);
        Console.WriteLine(runtimeData.GetDataFromRegister<float>(0));
        var ID = runtimeData.Alloc(4);
        var ID2 = runtimeData.Alloc(4);
        MemPtr L = new MemPtr() { MemID = ID, Offset = 0 };
        MemPtr R = new MemPtr() { MemID = ID2, Offset = 0 };
        unsafe
        {
            Console.WriteLine("Sizeof(MemPtr):" + sizeof(MemPtr));
        }
        runtimeData.SetDataToRegister(RegisterDefinition.RegisterOffset_00, L);
        runtimeData.SetDataToRegister(RegisterDefinition.RegisterOffset_01, R);
        runtimeData.SetDataToMemPtr(runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), 1.234f);
        runtimeData.MemCpy(runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00),
                           runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01),
                           4);
        Console.WriteLine(runtimeData.GetDataFromMemPtr<float>(runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00)));
        Console.WriteLine(runtimeData.GetDataFromMemPtr<float>(runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01)));
        runtimeData.Realloc((int)ID,8);
        Console.WriteLine(runtimeData.GetDataFromMemPtr<float>(L));
        L.Offset+=4;
        runtimeData.SetDataToMemPtr(L, 117);
        Console.WriteLine(runtimeData.GetDataFromMemPtr<int>(L));
        using FileStream stream = File.OpenWrite("coredump");
        runtimeData.DumpBinary(stream);
        stream.Flush();
        runtimeData.DumpText(Console.Out);
    }
}
