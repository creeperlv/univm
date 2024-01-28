#ifndef UNIVM_VM_FUNC
#define UNIVM_VM_FUNC
#include "core.h"
bool vm_func_fl_alloc(VMCore vm, uint32 target_register, uint32 size);
bool vm_func_alloc(VM vm, uint32 *ptr, uint32 size);
bool vm_func_free(VM vm, int memID);
#endif
