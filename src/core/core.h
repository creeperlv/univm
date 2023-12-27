#ifndef UNIVM_CORE
#define UNIVM_CORE
#include "base.h"
#include "basedata.h"
#include "inst.h"
typedef struct __instruction {
  uint32_t Inst;
  uint32_t Data[3];
} instruction;
typedef instruction *Instruction;
typedef struct _code {
  Instruction Instructions;
  int InstructionCount;
} code;
typedef struct _program {
  int TextCount;
  char **Texts;
  code Code;
} program;
typedef program *Program;
typedef struct _memoryBlock {
  bool IsAlloced;
  uint32 length;
  byte *Ptr;
} memoryBlock;
typedef memoryBlock *MemoryBlock;
typedef struct _callStackItem {
  uint32 ProgramID;
  uint32 PC;
} callStackItem;
typedef callStackItem *CallStackItem;
typedef struct _callStack {
  CallStackItem HEAD;
  int CurrentPos;
  int CurrentSize;
} callStack;
typedef struct _runtime {
  Program *LoadedPrograms;
  int ProgramBufferSize;
  uint8_t Registers[MAX_REGISTER_COUNT * 8];
  MemoryBlock Mem;
  callStack CallStack;
} runtime;
typedef runtime *Runtime;
typedef struct _memoryPtr {
  int32 MemID;
  uint32 Offset;
} memoryPtr;
typedef void (*Syscall)(Runtime);
typedef struct _syscallMap {
  uint32 *IDs;
  Syscall *Calls;
  uint32 SysCallMapBufSize;
  uint32 SysCallCount;
} syscallMap;
typedef syscallMap *SysCallMap;
typedef struct _syscallMapDict {
  uint32 *IDs;
  SysCallMap *Maps;
  uint32 DictBufSize;
  uint32 DictCount;
} syscallMapDict;
typedef syscallMapDict *SysCallMapDict;
typedef struct _vm {
  SysCallMapDict CallMap;
  Runtime CurrentRuntime;
} vm;
typedef vm *VM;
Runtime CreateRT();
SysCallMap CreateSysCallMap();
SysCallMapDict CreateSysCallMapDict();
bool ExpandSysCallMapDict(SysCallMapDict dict);
bool InitRT(Runtime runtime);
bool InitSysCallMapDict(SysCallMapDict dict);
bool InitSysCallMap(SysCallMap map);
bool ExpandSysCallMap(SysCallMap map);
bool SetSysCall(SysCallMapDict dict, Syscall call, uint32 Namespace, uint32 ID);
bool SetSysCallInMap(SysCallMap map, Syscall, uint32 ID);
//  Internal Memory Use, DO NOT use when data exchange with other arch machines.
int32 GetInt32FromMemoryPtr(Runtime rt, memoryPtr ptr);
uint32 GetUInt32FromMemoryPtr(Runtime rt, memoryPtr ptr);
int64 GetInt64FromMemoryPtr(Runtime rt, memoryPtr ptr);
uint64 GetUInt64FromMemoryPtr(Runtime rt, memoryPtr ptr);
int32 GetInt32FromRegister(Runtime rt, uint32 startIndex);
uint32 GetUInt32FromRegister(Runtime rt, uint32 startIndex);
bool WriteInt32ToRegister(Runtime rt, int32 Data, uint32 startIndex);
bool WriteUInt32ToRegister(Runtime rt, uint32 Data, uint32 startIndex);
int32 GetInt32FromLE(byte *buffer, int offset);
uint32 GetUInt32FromLE(byte *buffer, int offset);
int64 GetInt64FromLE(byte *buffer, int offset);
uint64 GetUInt64FromLE(byte *buffer, int offset);

#endif
