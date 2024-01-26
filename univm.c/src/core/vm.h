#ifndef UNIVM_VM
#define UNIVM_VM
#include "core.h"
bool InitVM(VM vm);
VMCore CreateCore(VM vm);
bool AddUniVMAsm(VM vm, UniVMAsm asm);
bool UniVMCallSync(VM vm, uint32 AsmID, uint32 PC);
bool UniVMCallAsync(VM vm, uint32 AsmID, uint32 PC);
bool InitMachineData(MachineData data);
bool ReleaseVM(VM vm);
bool ExecuteInst(VMCore core, Instruction inst);
#endif
