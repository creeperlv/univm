using univm.core;

namespace univm.tests
{
    public class RuntimeDataTest
    {
        [Fact]
        public void RegisterTest()
        {
            RuntimeData data = new RuntimeData();
            data.SetDataToRegister(0, 1234567);
            Assert.Equal(1234567, data.GetDataFromRegister<int>(0));
        }
        [Fact]
        public void MemBlockTest()
        {
            RuntimeData runtimeData = new RuntimeData();
            var ID = runtimeData.Alloc(4);
            var ID2 = runtimeData.Alloc(4);
            MemPtr L = new MemPtr() { MemID = ID, Offset = 0 };
            MemPtr R = new MemPtr() { MemID = ID2, Offset = 0 };
            runtimeData.SetDataToRegister(RegisterDefinition.RegisterOffset_00, L);
            runtimeData.SetDataToRegister(RegisterDefinition.RegisterOffset_01, R);
            runtimeData.SetDataToMemPtr(runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00), 1.234f);
            runtimeData.MemCpy(runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00),
                               runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01),
                               4);
            Assert.Equal(runtimeData.GetDataFromMemPtr<float>(runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_00)),
                         runtimeData.GetDataFromMemPtr<float>(runtimeData.GetDataFromRegister<MemPtr>(RegisterDefinition.RegisterOffset_01)));

        }
        [Fact]
        public void MemReallocFreeTest()
        {
            using RuntimeData runtimeData = new RuntimeData();
            var id=runtimeData.Alloc(4);
            MemPtr PTR0 = new MemPtr() { MemID=id,Offset = 0 };
            runtimeData.SetDataToMemPtr(PTR0,117);
            runtimeData.Realloc((int)PTR0.MemID,8);
            PTR0.Offset+=4;
            runtimeData.SetDataToMemPtr(PTR0,117);
            Assert.Equal(117,runtimeData.GetDataFromMemPtr<int>(PTR0));
        }
    }
}