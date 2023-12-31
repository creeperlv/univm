#include "vm.h"
#include "vm_func.h"
bool ExecuteInst(VM vm, Instruction inst) {
  uint32_t _inst = inst->Inst;
  Runtime RT = vm->CurrentRuntime;
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
    int32 L = GetInt32FromRegister(RT, R1);
    int32 R = GetInt32FromRegister(RT, R2);
    WriteInt32ToRegister(RT, L + R, R0);
  } break;
  case BSAE_SUB: {
    uint32 R0 = inst->Data[0];
    uint32 R1 = inst->Data[0];
    uint32 R2 = inst->Data[0];
    int32 L = GetInt32FromRegister(RT, R1);
    int32 R = GetInt32FromRegister(RT, R2);
    WriteInt32ToRegister(RT, L - R, R0);
  } break;
  case BSAE_MUL: {
    uint32 R0 = inst->Data[0];
    uint32 R1 = inst->Data[0];
    uint32 R2 = inst->Data[0];
    int32 L = GetInt32FromRegister(RT, R1);
    int32 R = GetInt32FromRegister(RT, R2);
    WriteInt32ToRegister(RT, L * R, R0);
  } break;
  case BSAE_DIV: {
    uint32 R0 = inst->Data[0];
    uint32 R1 = inst->Data[0];
    uint32 R2 = inst->Data[0];
    int32 L = GetInt32FromRegister(RT, R1);
    int32 R = GetInt32FromRegister(RT, R2);
    WriteInt32ToRegister(RT, L / R, R0);
  } break;
  default:
    return false;
  }
  return true;
}
