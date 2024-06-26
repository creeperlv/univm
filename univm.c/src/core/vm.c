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
	uint32 PTR;
	if (AppenndAsm(mdata, module, &ID) == false)
	{
		return false;
	}
	if (vm_func_alloc(vm, &PTR, module->GlobalMemorySize) == false)
	{
		return false;
	}
	module->GlobalMemPtr = PTR;
	return true;
}
void UniVMDefaultExecutionLoop(VMCore core)
{
	return;
}
bool UniVMCallSync(VM vm, uint32 AsmID, uint32 PC)
{
	VMCore core = CreateCore(vm);

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
			vm_func_alloc_reg(vmCore, register0, size);
		}
		break;
		case HL_FREE: {
			uint32 _register = inst->Data[0];
			uint32 MemID = GetRegister_UInt32(cData, _register);
			vm_func_free(vm, MemID);
		}
		break;
		case BASE_SET32: {

		}
		break;
		case BASE_ADD: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			int32 L = GetRegister_Int32(cData, R1);
			int32 R = GetRegister_Int32(cData, R2);
			int32 V;
			if (__of_add_int32(&V, L, R))
			{
				vmCore->OverFlowFlag = true;
			}
			SetRegister_Int32(cData, V, R0);
		}
		break;
		case BASE_SUB: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			int32 L = GetRegister_Int32(cData, R1);
			int32 R = GetRegister_Int32(cData, R2);
			int32 V;
			if (__of_sub_int32(&V, L, R))
			{
				vmCore->OverFlowFlag = true;
			}
			SetRegister_Int32(cData, V, R0);
		}
		break;
		case BASE_MUL: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			int32 L = GetRegister_Int32(cData, R1);
			int32 R = GetRegister_Int32(cData, R2);
			int32 V;
			if (__of_mul_int32(&V, L, R))
			{
				vmCore->OverFlowFlag = true;
			}
			SetRegister_Int32(cData, V, R0);
		}
		break;
		case BASE_DIV: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			int32 L = GetRegister_Int32(cData, R1);
			int32 R = GetRegister_Int32(cData, R2);
			int32 V;
			if (__of_div_int32(&V, L, R))
			{
				vmCore->OverFlowFlag = true;
			}
			SetRegister_Int32(cData, V, R0);
		}
		break;
		case BASE_ADD_16: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			int16 L = GetRegister_Int16(cData, R1);
			int16 R = GetRegister_Int16(cData, R2);
			int16 V;
			if (__of_add_int16(&V, L, R))
			{
				vmCore->OverFlowFlag = true;
			}
			SetRegister_Int16(cData, V, R0);
		}
		break;
		case BASE_SUB_16: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			int32 L = GetRegister_Int32(cData, R1);
			int32 R = GetRegister_Int32(cData, R2);
			int32 V;
			if (__of_sub_int32(&V, L, R))
			{
				vmCore->OverFlowFlag = true;
			}
			SetRegister_Int32(cData, V, R0);
		}
		break;
		case BASE_MUL_16: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			int32 L = GetRegister_Int32(cData, R1);
			int32 R = GetRegister_Int32(cData, R2);
			int32 V;
			if (__of_mul_int32(&V, L, R))
			{
				vmCore->OverFlowFlag = true;
			}
			SetRegister_Int32(cData, V, R0);
		}
		break;
		case BASE_DIV_16: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			int32 L = GetRegister_Int32(cData, R1);
			int32 R = GetRegister_Int32(cData, R2);
			int32 V;
			if (__of_div_int32(&V, L, R))
			{
				vmCore->OverFlowFlag = true;
			}
			SetRegister_Int32(cData, V, R0);
		}
		break;
		case BASE_ADD_S: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			Single L = GetRegister_Single(cData, R1);
			Single R = GetRegister_Int32(cData, R2);
			SetRegister_Single(cData, L + R, R0);
		}
		break;
		case BASE_SUB_S: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			Single L = GetRegister_Single(cData, R1);
			Single R = GetRegister_Int32(cData, R2);
			SetRegister_Single(cData, L - R, R0);
		}
		break;
		case BASE_MUL_S: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			Single L = GetRegister_Single(cData, R1);
			Single R = GetRegister_Int32(cData, R2);
			SetRegister_Single(cData, L * R, R0);
		}
		break;
		case BASE_DIV_S: {
			uint32 R0 = inst->Data[0];
			uint32 R1 = inst->Data[0];
			uint32 R2 = inst->Data[0];
			Single L = GetRegister_Single(cData, R1);
			Single R = GetRegister_Int32(cData, R2);
			SetRegister_Single(cData, L / R, R0);
		}
		break;
		default:
			return false;
	}
	return true;
}
