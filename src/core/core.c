#include "core.h"

Runtime CreateRT() {
  Runtime rt = malloc(sizeof(runtime));
  if (IsNull(rt)) {
    Panic(ID_MALLOC_FAIL);
    return NULL;
  }
  return rt;
}
SysCallMap CreateSysCallMap() {
  SysCallMap map = malloc(sizeof(syscallMap));
  if (IsNull(map)) {
    Panic(ID_MALLOC_FAIL);
    return NULL;
  }
  return map;
}
bool InitRT(Runtime runtime) {
  runtime->LoadedPrograms = malloc(sizeof(Program) * ProgramCountBlockSize);
  if (IsNull(runtime->LoadedPrograms)) {
    Panic(ID_MALLOC_FAIL);
    return false;
  }
  for (int i = 0; i < ProgramCountBlockSize; i++) {
    runtime->LoadedPrograms[i] = null;
  }
  runtime->Mem = malloc(sizeof(memoryBlock) * MemBlockSize);
  if (IsNull(runtime->Mem)) {
    Panic(ID_MALLOC_FAIL);
    return false;
  }
  for (int i = 0; i < MemBlockSize; i++) {
    runtime->Mem[i].IsAlloced = false;
    runtime->Mem[i].length = 0;
    runtime->Mem[i].Ptr = null;
  }
  return true;
}
bool InitSysCallMap(SysCallMap map) {
  map->IDs = malloc(sizeof(uint32) * SysCallMapBlockSize);
  if (IsNull(map->IDs)) {
    Panic(ID_MALLOC_FAIL);
    return false;
  }
  return true;
}
bool ExpandSysCallMap(SysCallMap map) {
  uint32 NewSize = map->SysCallMapBufSize + SysCallMapBlockSize;
  uint32 *newID = realloc(map->IDs, NewSize);
  if (IsNull(newID)) {
    Panic(ID_REALLOC_FAIL);
    return false;
  }
  Syscall *Calls = realloc(map->Calls, NewSize);
  if (IsNull(Calls)) {
    Panic(ID_REALLOC_FAIL);
    return false;
  }
  map->IDs = newID;
  map->Calls = Calls;
  map->SysCallMapBufSize = NewSize;
  return true;
}
bool SetSysCall(SysCallMap map, Syscall call, int ID) {
  if (map->SysCallCount >= map->SysCallMapBufSize) {
    ExpandSysCallMap(map);
  }
  int CurrentIndex = map->SysCallCount;
  map->IDs[CurrentIndex] = ID;
  map->Calls[CurrentIndex] = call;
  map->SysCallCount = CurrentIndex + 1;
  return true;
}
