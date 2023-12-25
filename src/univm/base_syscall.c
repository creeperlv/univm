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
bool SetupSysCall_Base_0(SysCallMap map) {
  SetSysCall(map, STATFS, 43);
  SetSysCall(map, FSTATFS, 44);
  SetSysCall(map, TRUNCATE, 45);
  SetSysCall(map, FTRUNCATE, 46);
  SetSysCall(map, FALLOCATE, 47);
  SetSysCall(map, CHDIR, 49);
  SetSysCall(map, LSEEK, 62);
  SetSysCall(map, READ, 63);
  SetSysCall(map, WRITE, 64);
  return true;
}
