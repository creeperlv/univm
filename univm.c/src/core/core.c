#include "core.h"

Runtime CreateRT() {
	Runtime rt = malloc(sizeof(runtime));
	if (IsNull(rt)) {
		Panic(ID_MALLOC_FAIL);
		return NULL;
	}
	return rt;
}
UniVMAsm CreateProgram() {
	UniVMAsm prog = malloc(sizeof(uniVMAsm));
	if (IsNull(prog)) {
		Panic(ID_MALLOC_FAIL);
		return null;
	}
	return prog;
}
bool ReadData(FILE* src, byte* buffer, size_t Size) {
	int c;
	for (size_t i = 0; i < Size; i++)
	{
		c = fgetc(src);
		if (c == EOF&&i<Size-1)
			return false;
		buffer[i] = (byte)c;
	}
	return true;
}
bool LoadProgram(FILE* src, UniVMAsm asm) {
	if (IsNull(asm))
		return false;
	instruction inst;
	uint32 TextCount;
	uint32 LibCount;
	uint32 InstCount;
	uint32 GPSize;
	if (ReadData(src, (byte*)(&TextCount),4) == false) {
		return false;
	}
	if (ReadData(src, (byte*)(&LibCount),4) == false) {
		return false;
	}
	if (ReadData(src, (byte*)(&InstCount),4) == false) {
		return false;
	}
	if (ReadData(src, (byte*)(&GPSize),4) == false) {
		return false;
	}
	int d;
	asm->TextCount = TextCount;
	asm->Texts = malloc(sizeof(textItem) * TextCount);
	for (size_t i = 0; i < TextCount; i++)
	{
		uint32 Length;
		if (ReadData(src, (byte*)(&Length),4) == false) {
			return false;
		}
		byte* Data = malloc(sizeof(byte) * Length);
		for (size_t index = 0; index < Length; index++)
		{
			if ((d = fgetc(src)) != EOF)
			{
				Data[index] = (byte)d;
			}
		}
		asm->Texts[i].Length = Length;
		asm->Texts[i].Data = Data;
	}
	asm->LibCount = LibCount;
	asm->Libs = malloc(sizeof(textItem) * TextCount);
	for (size_t i = 0; i < LibCount; i++)
	{
		uint32 Length;
		if (ReadData(src, (byte*)(&Length), 4) == false) {
			return false;
		}
		byte* Data = malloc(sizeof(byte) * Length);
		for (size_t index = 0; index < Length; index++)
		{
			if ((d = fgetc(src)) != EOF)
			{
				Data[index] = (byte)d;
			}
		}
		asm->Libs[i].Length = Length;
		asm->Libs[i].Data = Data;
	}
	asm->Code.InstructionCount = InstCount;
	asm->Code.Instructions = malloc(sizeof(instruction) * InstCount);
	for (size_t i = 0; i < InstCount; i++)
	{
		if (ReadData(src, (byte*)(&inst), sizeof(instruction))) {
			asm->Code.Instructions[i] = inst;
		}
		else return false;
	}
	asm->GlobalMemorySize = GPSize;
	//char** Texts=
	return true;
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

	runtime->machine.LoadedPrograms =
		malloc(sizeof(UniVMAsm) * ProgramCountBlockSize);
	if (IsNull(runtime->machine.LoadedPrograms)) {
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	for (int i = 0; i < ProgramCountBlockSize; i++) {
		runtime->machine.LoadedPrograms[i] = null;
	}
	runtime->machine.Mem = malloc(sizeof(memoryBlock) * MemBlockSize);
	if (IsNull(runtime->machine.Mem)) {
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	for (int i = 0; i < MemBlockSize; i++) {
		runtime->machine.Mem[i].IsAlloced = false;
		runtime->machine.Mem[i].length = 0;
		runtime->machine.Mem[i].Ptr = null;
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
	uint32* newID = realloc(map->IDs, sizeof(uint32) * NewSize);
	if (IsNull(newID)) {
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	Syscall* Calls = realloc(map->Calls, sizeof(Syscall) * NewSize);
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
	uint32* newID = realloc(dict->IDs, sizeof(uint32) * NewSize);
	if (IsNull(newID)) {
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	SysCallMap* Maps = realloc(dict->Maps, sizeof(SysCallMap) * NewSize);
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
