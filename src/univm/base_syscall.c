#include "base_syscall.h"
void WRITE(Runtime rt) {}
void READ(Runtime rt) {}
void CHDIR(Runtime rt) {}
void LSEEK(Runtime rt) {}
void STATFS(Runtime rt) {}
void FSTATFS(Runtime rt) {}
void TRUNCATE(Runtime rt) {}
void FTRUNCATE(Runtime rt) {}
void FALLOCATE(Runtime rt) {}
bool SetupSysCall_Base_0(SysCallMapDict dict) {
  SetSysCall(dict, STATFS, 0, 43);
  SetSysCall(dict, FSTATFS, 0, 44);
  SetSysCall(dict, TRUNCATE, 0, 45);
  SetSysCall(dict, FTRUNCATE, 0, 46);
  SetSysCall(dict, FALLOCATE, 0, 47);
  SetSysCall(dict, CHDIR, 0, 49);
  SetSysCall(dict, LSEEK, 0, 62);
  SetSysCall(dict, READ, 0, 63);
  SetSysCall(dict, WRITE, 0, 64);
  return true;
}
