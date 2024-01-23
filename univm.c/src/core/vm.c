#include "vm.h"
#include "core.h"
#include "vm_func.h"
bool InitVM(VM vm) {
    vm->CallMap = CreateSysCallMapDict();
    vm -> CurrentRuntime = CreateRT();
    return true;
}
bool ExecuteInst(VMCore vmCore, Instruction inst) {
  uint32_t _inst = inst->Inst;
  VM vm = vmCore->HostMachine;
  CoreData cData = vmCore->CoreData;
  // Runtime RT = vm->CurrentRuntime;
  switch (_inst) {
  case HL_ALLOC: {

    uint32 register0 = inst->Data[0];
    uint32 size = inst->Data[1];
    vm_func_fl_alloc(vm, register0, size);
  } break;
  case BSAE_ADD: {
    uint32 R0 = inst->Data[0];
    uint32 R1 = inst->Data[0];
    uint32 R2 = inst->Data[0];
    int32 L = GetRegister_Int32(cData, R1);
    int32 R = GetRegister_Int32(cData, R2);
    SetRegister_Int32(cData, L + R, R0);
  } break;
  case BSAE_SUB: {
    uint32 R0 = inst->Data[0];
    uint32 R1 = inst->Data[0];
    uint32 R2 = inst->Data[0];
    int32 L = GetRegister_Int32(cData, R1);
    int32 R = GetRegister_Int32(cData, R2);
    SetRegister_Int32(cData, L - R, R0);
  } break;
  case BSAE_MUL: {
    uint32 R0 = inst->Data[0];
    uint32 R1 = inst->Data[0];
    uint32 R2 = inst->Data[0];
    int32 L = GetRegister_Int32(cData, R1);
    int32 R = GetRegister_Int32(cData, R2);
    SetRegister_Int32(cData, L * R, R0);
  } break;
  case BSAE_DIV: {
    uint32 R0 = inst->Data[0];
    uint32 R1 = inst->Data[0];
    uint32 R2 = inst->Data[0];
    int32 L = GetRegister_Int32(cData, R1);
    int32 R = GetRegister_Int32(cData, R2);
    SetRegister_Int32(cData, L / R, R0);
  } break;
  default:
    return false;
  }
  return true;
}
