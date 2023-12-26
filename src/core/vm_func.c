#include "vm_func.h"

bool vm_func_fl_alloc(VM vm, uint32 target_register, uint32 size) {
  void *mem = malloc(size);
  if (IsNull(mem)) {
#ifdef PANIC_VM_MALLOC_FAIL
    panic(ID_MALLOC_FAIL);
#endif
    return false;
  } else {
  }
  return true;
}
