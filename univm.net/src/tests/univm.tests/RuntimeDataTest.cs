using univm.core;

namespace univm.tests
{
    public class RuntimeDataTest
    {
        [Fact]
        public void RegisterTest()
        {
            CoreData data = new CoreData(new VMCore(new VM()));
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
            var ID = coreData.Alloc(4);
            var ID2 = coreData.Alloc(4);
            MemPtr L = new MemPtr() { MemID = ID, Offset = 0 };
            MemPtr R = new MemPtr() { MemID = ID2, Offset = 0 };
            coreData.SetDataToRegister(RegisterDefinition.RegisterOffset_00, L);
            coreData.SetDataToRegister(RegisterDefinition.RegisterOffset_01, R);
            coreData.SetDataToMemPtr(coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), 1.234f);
            coreData.MemCpy(coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01), 4);
            Assert.Equal(coreData.GetDataFromMemPtr<float>(coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00)),
                         coreData.GetDataFromMemPtr<float>(coreData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01)));

        }
        [Fact]
        public void MemReallocFreeTest()
        {
            using VM _vm = new VM();
            VMCore core = new VMCore(_vm);
            MachineData machinedata = _vm.machineData;
            CoreData coreData = core.coreData;
            var id = coreData.Alloc(4);
            MemPtr PTR0 = new MemPtr() { MemID = id, Offset = 0 };
            coreData.SetDataToMemPtr(PTR0, 117);
            coreData.Realloc((int)PTR0.MemID, 8);
            PTR0.Offset += 4;
            coreData.SetDataToMemPtr(PTR0, 117);
            Assert.Equal(117, coreData.GetDataFromMemPtr<int>(PTR0));
        }
    }
}