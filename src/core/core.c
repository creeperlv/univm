#include "core.h"

Runtime CreateRT() {
  Runtime rt = malloc(sizeof(runtime));
  if (IsNull(rt)) {
    Panic(ID_MALLOC_FAIL);
    return NULL;
  }
  return rt;
}
Program CreateProgram() {
  Program prog = malloc(sizeof(program));
  if (IsNull(prog)) {
    Panic(ID_MALLOC_FAIL);
    return null;
  }
  return prog;
}
SysCallMap CreateSysCallMap() {
  SysCallMap map = malloc(sizeof(syscallMap));
  if (IsNull(map)) {
    Panic(ID_MALLOC_FAIL);
    return NULL;
  }
  return map;
}
SysCallMapDict CreateSysCallMapDict() {
  SysCallMapDict dict = malloc(sizeof(syscallMapDict));
  if (IsNull(dict)) {
    Panic(ID_MALLOC_FAIL);
    return NULL;
  }
  return dict;
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
  map->Calls = malloc(sizeof(Syscall) * SysCallMapBlockSize);
  if (IsNull(map->Calls)) {
    Panic(ID_REALLOC_FAIL);
    return false;
  }
  return true;
}
bool InitSysCallMapDict(SysCallMapDict dict) {
  dict->IDs = malloc(sizeof(uint32) * SysCallMapBlockSize);

  if (IsNull(dict->IDs)) {
    Panic(ID_MALLOC_FAIL);
    return false;
  }
  dict->Maps = malloc(sizeof(SysCallMap) * SysCallMapBlockSize);
  if (IsNull(dict->Maps)) {
    Panic(ID_MALLOC_FAIL);
    return false;
  }
  return true;
}
bool ExpandSysCallMap(SysCallMap map) {
  uint32 NewSize = map->SysCallMapBufSize + SysCallMapBlockSize;
  uint32 *newID = realloc(map->IDs, sizeof(uint32) * NewSize);
  if (IsNull(newID)) {
    Panic(ID_REALLOC_FAIL);
    return false;
  }
  Syscall *Calls = realloc(map->Calls, sizeof(Syscall) * NewSize);
  if (IsNull(Calls)) {
    Panic(ID_REALLOC_FAIL);
    return false;
  }
  map->IDs = newID;
  map->Calls = Calls;
  map->SysCallMapBufSize = NewSize;
  return true;
}
bool ExpandSysCallMapDict(SysCallMapDict dict) {
  uint32 NewSize = dict->DictBufSize + SysCallMapBlockSize;
  uint32 *newID = realloc(dict->IDs, sizeof(uint32) * NewSize);
  if (IsNull(newID)) {
    Panic(ID_REALLOC_FAIL);
    return false;
  }
  SysCallMap *Maps = realloc(dict->Maps, sizeof(SysCallMap) * NewSize);
  if (IsNull(Maps)) {
    Panic(ID_REALLOC_FAIL);
    return false;
  }
  dict->IDs = newID;
  dict->Maps = Maps;
  dict->DictBufSize = NewSize;
  return true;
}
bool SetSysCall(SysCallMapDict dict, Syscall call, uint32 Namespace,
                uint32 ID) {
  for (uint32 i = 0; i < dict->DictCount; i++) {
    uint32 _ID = dict->IDs[i];
    if (_ID == Namespace) {
      return SetSysCallInMap(dict->Maps[i], call, ID);
    }
  }
  if (dict->DictCount > dict->DictBufSize) {
    if (!ExpandSysCallMapDict(dict)) {
      return false;
    }
  }
  int CurrentIndex = dict->DictCount;
  dict->IDs[CurrentIndex] = ID;
  SysCallMap map = CreateSysCallMap();
  if (!InitSysCallMap(map)) {
    return false;
  }
  dict->Maps[CurrentIndex] = map;
  dict->DictCount = CurrentIndex + 1;
  return SetSysCallInMap(map, call, ID);
}
bool SetSysCallInMap(SysCallMap map, Syscall call, uint32 ID) {
  for (uint32 i = 0; i < map->SysCallCount; i++) {
    uint32 _ID = map->IDs[i];
    if (_ID == ID) {
      map->Calls[i] = call;
      return true;
    }
  }
  if (map->SysCallCount >= map->SysCallMapBufSize) {
    ExpandSysCallMap(map);
  }
  int CurrentIndex = map->SysCallCount;
  map->IDs[CurrentIndex] = ID;
  map->Calls[CurrentIndex] = call;
  map->SysCallCount = CurrentIndex + 1;
  return true;
}
