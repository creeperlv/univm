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
typedef struct _univmasm {
  int TextCount;
  char **Texts;
  code Code;
} uniVMAsm;
typedef uniVMAsm *UniVMAsm;
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
typedef struct _coreData *CoreData;
typedef struct _machineData *MachineData;
typedef struct _coreData {
  uint8_t Registers[MAX_REGISTER_COUNT * 8];
  int ProgramBufferSize;
  callStack CallStack;
  MachineData Machine;
} coreData;
typedef struct _machineData {
  UniVMAsm *LoadedPrograms;
  MemoryBlock Mem;
  CoreData Cores;
} machineData;
typedef struct _runtime {
  machineData machine;
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
  bool (*RunAsm)(UniVMAsm);
} vm;
typedef vm *VM;
typedef struct _vmCore *VMCore;
typedef struct _vmCore {
  CoreData CoreData;
  VM HostMachine;
  bool (*ExecuteInst)(VMCore core, Instruction inst);
} vmCore;
Runtime CreateRT();
UniVMAsm CreateProgram();
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
int32 GetRegister_Int32(CoreData core, uint32 startIndex);
uint32 GetRegister_UInt32(CoreData core, uint32 startIndex);
int64 GetRegister_Int64(CoreData core, uint32 startIndex);
uint64 GetRegister_UInt64(CoreData core, uint32 startIndex);
bool SetRegister_Int32(CoreData core, int32 Data, uint32 startIndex);
bool SetRegister_UInt32(CoreData core, uint32 Data, uint32 startIndex);
int32 GetInt32FromLE(byte *buffer, int offset);
uint32 GetUInt32FromLE(byte *buffer, int offset);
int64 GetInt64FromLE(byte *buffer, int offset);
uint64 GetUInt64FromLE(byte *buffer, int offset);

#endif
