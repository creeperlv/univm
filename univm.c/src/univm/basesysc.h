#ifndef UNIVM_BASE_SYSCALLS
#define UNIVM_BASE_SYSCALLS
#include "../core/core.h"
#define _BSD_STYLE_SYSCALL_DATA_TYPE_FILE 0x00000010
#define _BSD_STYLE_SYSCALL_DATA_TYPE_DIRE 0x00000011
bool RedirectStdIO(VM vm);
void InitResource_FILE(Resource resource, FILE *file);
bool SetupSysCall_Base_0(SysCallMapDict dict);
#endif
