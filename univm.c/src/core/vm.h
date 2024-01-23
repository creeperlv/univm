#ifndef UNIVM_VM
#define UNIVM_VM
#include "core.h"
bool InitVM(VM vm);
bool ExecuteInst(VMCore core, Instruction inst);
#endif
