using univm.core;

namespace univm.tests
{
    public class RuntimeDataTest
    {
        [Fact]
        public void RegisterTest()
        {
            CoreData data = new CoreData();
            data.SetDataToRegister(0, 1234567);
            Assert.Equal(1234567, data.GetDataFromRegister<int>(0));
        }
        [Fact]
        public void MemBlockTest()
        {
            using VM _vm = new VM();
            VMCore core = new VMCore(_vm);
            MachineData machinedata = _vm.machineData;
            CoreData coreData = core.coreData;
            var ID = machinedata.Alloc(4, coreData);
            var ID2 = machinedata.Alloc(4, coreData);
            MemPtr L = new MemPtr() { MemID = ID, Offset = 0 };
            MemPtr R = new MemPtr() { MemID = ID2, Offset = 0 };
            coreData.SetDataToRegister(RegisterDefinition.RegisterOffset_00, L);
            coreData.SetDataToRegister(RegisterDefinition.RegisterOffset_01, R);
            machinedata.SetDataToMemPtr(coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), 1.234f, coreData);
            machinedata.MemCpy(coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01), 4, coreData);
            Assert.Equal(machinedata.GetDataFromMemPtr<float>(coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), coreData),
                         machinedata.GetDataFromMemPtr<float>(coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01), coreData));

        }
        [Fact]
        public void MemReallocFreeTest()
        {
            using VM _vm = new VM();
            VMCore core = new VMCore(_vm);
            MachineData machinedata = _vm.machineData;
            CoreData coreData = core.coreData;
            var id = machinedata.Alloc(4, coreData);
            MemPtr PTR0 = new MemPtr() { MemID = id, Offset = 0 };
            machinedata.SetDataToMemPtr(PTR0, 117, coreData);
            machinedata.Realloc((int)PTR0.MemID, 8, coreData);
            PTR0.Offset += 4;
            machinedata.SetDataToMemPtr(PTR0, 117, coreData);
            Assert.Equal(117, machinedata.GetDataFromMemPtr<int>(PTR0, coreData ));
        }
    }
}