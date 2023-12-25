#include "vm.h"
bool ExecuteInst(VM vm, Instruction inst) {
  uint32_t _inst = inst->Inst;
  switch (_inst) {
  case HL_ALLOC:

    break;
  default:
    return false;
  }
  return true;
}
