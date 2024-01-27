#include "vm.h"
#include "core.h"
#include "vm_func.h"
bool InitVM(VM vm)
{
    vm->CallMap = CreateSysCallMapDict();
    vm->CurrentRuntime = CreateRT();
    InitMachineData(&vm->CurrentRuntime->machine);
    return true;
}

VMCore CreateCore(VM vm)
{
    VMCore ptr = malloc(sizeof(vmCore));
    CoreData cData;
    if (IsNull(ptr))
    {
        Panic(ID_MALLOC_FAIL);
        return ptr;
    }
    cData = malloc(sizeof(coreData));
    if (IsNull(cData))
    {
        free(ptr);
        Panic(ID_MALLOC_FAIL);
        return NULL;
    }
    cData->CallStack.HEAD = malloc(sizeof(CallStackItem) * CALLSTACK_BLOCK_SIZE);
    if (IsNull(cData->CallStack.HEAD))
    {
        free(cData);
        free(ptr);
        Panic(ID_MALLOC_FAIL);
        return NULL;
    }
    cData->CallStack.StackSize = CALLSTACK_BLOCK_SIZE;
    cData->CallStack.ItemCount = 0;
    return ptr;
}
bool AddUniVMAsm(VM vm, UniVMAsm module)
{
    Runtime rt = vm->CurrentRuntime;
    MachineData mdata = &rt->machine;
    uint32 ID;
    if (AppenndAsm(mdata, module, &ID) == false)
    {
        return false;
    }
    return true;
}
void UniVMDefaultExecutionLoop(VMCore core)
{
    return;
}
bool UniVMCallSync(VM vm, uint32 AsmID, uint32 PC)
{
    return true;
}
bool InitMachineData(MachineData mdata)
{
    if (InitMemBlock(mdata) == false)
    {
        return false;
    }
    if (InitAsms(mdata) == false)
    {
        return false;
    }
    mdata->ResourceBufSize = 0;
    mdata->ResourceCount = 0;
    mdata->resources = NULL;
    return true;
}
bool ReleaseVM(VM vm)
{
    size_t i = 0;
    Runtime rt = vm->CurrentRuntime;
    machineData mdata = rt->machine;
    uint32 Length = mdata.ResourceCount;
    for (; i < Length; i++)
    {
        if (mdata.resources[i]->IsInited && mdata.resources[i]->Data != NULL)
        {
            mdata.resources[i]->Release(mdata.resources[i]);
        }
    }
    Length = rt->CoreCount;
    for (i = 0; i < Length; i++)
    {
        FreeCore(rt->Cores[i]);
    }
    return true;
}
bool ExecuteInst(VMCore vmCore, Instruction inst)
{
    uint32_t _inst = inst->Inst;
    VM vm = vmCore->HostMachine;
    CoreData cData = vmCore->CoreData;
    // Runtime RT = vm->CurrentRuntime;
    switch (_inst)
    {
    case HL_ALLOC: {
        uint32 register0 = inst->Data[0];
        uint32 size = inst->Data[1];
        vm_func_fl_alloc(vm, register0, size);
    }
    break;
    case BSAE_ADD: {
        uint32 R0 = inst->Data[0];
        uint32 R1 = inst->Data[0];
        uint32 R2 = inst->Data[0];
        int32 L = GetRegister_Int32(cData, R1);
        int32 R = GetRegister_Int32(cData, R2);
        SetRegister_Int32(cData, L + R, R0);
    }
    break;
    case BSAE_SUB: {
        uint32 R0 = inst->Data[0];
        uint32 R1 = inst->Data[0];
        uint32 R2 = inst->Data[0];
        int32 L = GetRegister_Int32(cData, R1);
        int32 R = GetRegister_Int32(cData, R2);
        SetRegister_Int32(cData, L - R, R0);
    }
    break;
    case BSAE_MUL: {
        uint32 R0 = inst->Data[0];
        uint32 R1 = inst->Data[0];
        uint32 R2 = inst->Data[0];
        int32 L = GetRegister_Int32(cData, R1);
        int32 R = GetRegister_Int32(cData, R2);
        SetRegister_Int32(cData, L * R, R0);
    }
    break;
    case BSAE_DIV: {
        uint32 R0 = inst->Data[0];
        uint32 R1 = inst->Data[0];
        uint32 R2 = inst->Data[0];
        int32 L = GetRegister_Int32(cData, R1);
        int32 R = GetRegister_Int32(cData, R2);
        SetRegister_Int32(cData, L / R, R0);
    }
    break;
    default:
        return false;
    }
    return true;
}
