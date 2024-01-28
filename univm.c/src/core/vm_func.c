#include "vm_func.h"

bool vm_func_fl_alloc(VMCore core, uint32 target_register, uint32 size)
{
    uint32 ptr = 0;
    if (!vm_func_alloc(core->HostMachine, &ptr, size))
    {
        return false;
    }
    SetRegister_UInt32(core->CoreData, ptr, target_register);
    SetRegister_UInt32(core->CoreData, 0, target_register * sizeof(uint32));
    return true;
}

bool vm_func_alloc(VM vm, uint32 *ptr, uint32 size)
{
    bool Hit = false;
    uint32 i = 0;
    MachineData mdata = &vm->CurrentRuntime->machine;
    void *mem = malloc(size);
    if (IsNull(mem))
    {
#ifdef PANIC_VM_MALLOC_FAIL
        panic(ID_MALLOC_FAIL);
#endif
        return false;
    }
    else
    {
        for (; i < mdata->MemCount; i++)
        {
            if (mdata->Mem[i].IsAlloced == false || mdata->Mem[i].Ptr == NULL)
            {
                ptr[0] = i;
                Hit = true;
            }
        }
        if (!Hit)
        {

            if (mdata->MemCount >= mdata->MemBufSize)
            {
                if (ExpandMemBuf(mdata) == false)
                {
                    return false;
                }
            }
            ptr[0] = mdata->MemCount;
            mdata->MemCount++;
        }
        mdata->Mem[ptr[0]].Ptr = mem;
        mdata->Mem[ptr[0]].IsAlloced = true;
        mdata->Mem[ptr[0]].length = size;
    }
    return true;
}
