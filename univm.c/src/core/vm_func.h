#ifndef UNIVM_VM_FUNC
#define UNIVM_VM_FUNC
#include "core.h"
bool vm_func_alloc_reg(VMCore vm, uint32 target_register, uint32 size);
bool vm_func_alloc(VM vm, uint32 *ptr, uint32 size);
bool vm_func_free(VM vm, uint32 memID);
bool vm_func_realloc(VM vm, uint32 memID, uint32 NewSize);
#endif
