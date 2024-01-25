#include "core.h"

Runtime CreateRT()
{
    Runtime rt = malloc(sizeof(runtime));
    if (IsNull(rt))
    {
        Panic(ID_MALLOC_FAIL);
        return NULL;
    }
    rt->CoreCount = 0;
    return rt;
}
UniVMAsm CreateProgram()
{
    UniVMAsm prog = malloc(sizeof(uniVMAsm));
    if (IsNull(prog))
    {
        Panic(ID_MALLOC_FAIL);
        return null;
    }
    return prog;
}
bool InitMemBlock(MachineData data)
{
    data->Mem = malloc(sizeof(MemoryBlock) * MemBlockSize);
    if (IsNull(data->Mem))
    {
        Panic(ID_MALLOC_FAIL);
        return false;
    }
    data->MemCount = 0;
    data->MemBufSize = MemBlockSize;
    return true;
}
bool InitAsms(MachineData data)
{
    data->LoadedPrograms = malloc(sizeof(UniVMAsm) * AsmBufBlockSize);
    if (IsNull(data->LoadedPrograms))
    {
        Panic(ID_MALLOC_FAIL);
        return false;
    }
    data->ProgramCount = 0;
    data->ProgramSize = AsmBufBlockSize;
    return true;
}
bool ExpandMemBuf(MachineData data)
{
    uint32 NewSize = data->MemBufSize + MemBlockSize;
    MemoryBlock ptr = realloc(data->Mem, sizeof(memoryBlock) * NewSize);
    if (IsNull(ptr))
    {
        Panic(ID_REALLOC_FAIL);
        return false;
    }
    data->Mem = ptr;
    data->MemBufSize = NewSize;
    return true;
}
bool ExpandAsmBuf(MachineData data)
{
    uint32 NewSize = data->ProgramSize + AsmBufBlockSize;
    UniVMAsm *ptr = realloc(data->LoadedPrograms, sizeof(UniVMAsm) * NewSize);
    if (IsNull(ptr))
    {
        Panic(ID_REALLOC_FAIL);
        return false;
    }
    data->LoadedPrograms = ptr;
    data->ProgramSize = NewSize;
    return true;
}
void FreeCore(VMCore core)
{
    if (IsNull(core))
        return;
    core->CoreData->Machine = NULL;
    free(core->CoreData->CallStack.HEAD);
    free(core->CoreData);
}
bool ReadData(FILE *src, byte *buffer, size_t Size)
{
    int c;
    size_t i = 0;
    for (; i < Size; i++)
    {
        c = fgetc(src);
        if (c == EOF && i < Size - 1)
            return false;
        buffer[i] = (byte)c;
    }
    return true;
}
bool LoadProgram(FILE *src, UniVMAsm assembly)
{
    instruction inst;
    uint32 TextCount;
    uint32 LibCount;
    uint32 InstCount;
    uint32 GPSize;
    byte *Data;
    size_t i = 0;
    size_t index = 0;
    int d;
    if (IsNull(assembly))
        return false;
    if (ReadData(src, (byte *)(&TextCount), 4) == false)
    {
        return false;
    }
    if (ReadData(src, (byte *)(&LibCount), 4) == false)
    {
        return false;
    }
    if (ReadData(src, (byte *)(&InstCount), 4) == false)
    {
        return false;
    }
    if (ReadData(src, (byte *)(&GPSize), 4) == false)
    {
        return false;
    }
    assembly->TextCount = TextCount;
    assembly->Texts = malloc(sizeof(textItem) * TextCount);
    for (; i < TextCount; i++)
    {
        uint32 Length;
        if (ReadData(src, (byte *)(&Length), 4) == false)
        {
            return false;
        }
        Data = malloc(sizeof(byte) * Length);
        for (; index < Length; index++)
        {
            if ((d = fgetc(src)) != EOF)
            {
                Data[index] = (byte)d;
            }
        }
        assembly->Texts[i].Length = Length;
        assembly->Texts[i].Data = Data;
    }
    assembly->LibCount = LibCount;
    assembly->Libs = malloc(sizeof(textItem) * TextCount);
    for (i = 0; i < LibCount; i++)
    {
        uint32 Length;
        if (ReadData(src, (byte *)(&Length), 4) == false)
        {
            return false;
        }
        Data = malloc(sizeof(byte) * Length);
        for (index = 0; index < Length; index++)
        {
            if ((d = fgetc(src)) != EOF)
            {
                Data[index] = (byte)d;
            }
        }
        assembly->Libs[i].Length = Length;
        assembly->Libs[i].Data = Data;
    }
    assembly->Code.InstructionCount = InstCount;
    assembly->Code.Instructions = malloc(sizeof(instruction) * InstCount);
    for (i = 0; i < InstCount; i++)
    {
        if (ReadData(src, (byte *)(&inst), sizeof(instruction)))
        {
            assembly->Code.Instructions[i] = inst;
        }
        else
            return false;
    }
    assembly->GlobalMemorySize = GPSize;
    return true;
}
bool ExpandResourceBuf(MachineData data)
{
    uint32 NewSize = data->ResourceBufSize + ResourceBufBlockSize;
    void *ptr = realloc(data->resources, NewSize * sizeof(Resource));
    if (IsNull(ptr))
    {
        Panic(ID_REALLOC_FAIL);
        return false;
    }
    data->ResourceBufSize = NewSize;
    data->resources = ptr;
    return true;
}
uint32 AttachResource(VM vm, Resource res)
{
    MachineData data = &vm->CurrentRuntime->machine;
    uint32 currentCount = data->ResourceCount;
    uint32 i = 0;
    Resource _res;
    for (i = 0; i < currentCount; i++)
    {
        _res = data->resources[i];
        if (_res->IsInited == false || _res->Data == NULL)
        {
            data->resources[i] = res;
            return i;
        }
    }
    if (data->ResourceCount >= data->ResourceBufSize)
    {
        if (ExpandResourceBuf(data) == false)
        {
            return 0xFFFFFFFF;
        }
    }
    data->resources[currentCount] = res;
    currentCount++;
    data->ResourceCount = currentCount;
    return currentCount - 1;
}
SysCallMap CreateSysCallMap()
{
    SysCallMap map = malloc(sizeof(syscallMap));
    if (IsNull(map))
    {
        Panic(ID_MALLOC_FAIL);
        return NULL;
    }
    return map;
}
SysCallMapDict CreateSysCallMapDict()
{
    SysCallMapDict dict = malloc(sizeof(syscallMapDict));
    if (IsNull(dict))
    {
        Panic(ID_MALLOC_FAIL);
        return NULL;
    }
    dict->DictBufSize = SysCallMapDictBlockSize;
    dict->DictCount = 0;
    dict->IDs = malloc(SysCallMapDictBlockSize * sizeof(uint32));
    dict->Maps = malloc(SysCallMapDictBlockSize * sizeof(SysCallMap));
    return dict;
}
bool InitRT(Runtime runtime)
{
    int i = 0;
    runtime->machine.LoadedPrograms = malloc(sizeof(UniVMAsm) * ProgramCountBlockSize);
    if (IsNull(runtime->machine.LoadedPrograms))
    {
        Panic(ID_MALLOC_FAIL);
        return false;
    }
    for (; i < ProgramCountBlockSize; i++)
    {
        runtime->machine.LoadedPrograms[i] = null;
    }
    runtime->machine.Mem = malloc(sizeof(memoryBlock) * MemBlockSize);
    if (IsNull(runtime->machine.Mem))
    {
        Panic(ID_MALLOC_FAIL);
        return false;
    }
    for (i = 0; i < MemBlockSize; i++)
    {
        runtime->machine.Mem[i].IsAlloced = false;
        runtime->machine.Mem[i].length = 0;
        runtime->machine.Mem[i].Ptr = null;
    }
    return true;
}
bool InitSysCallMap(SysCallMap map)
{
    map->IDs = malloc(sizeof(uint32) * SysCallMapBlockSize);
    if (IsNull(map->IDs))
    {
        Panic(ID_MALLOC_FAIL);
        return false;
    }
    map->Calls = malloc(sizeof(Syscall) * SysCallMapBlockSize);
    if (IsNull(map->Calls))
    {
        Panic(ID_REALLOC_FAIL);
        return false;
    }
    return true;
}
bool InitSysCallMapDict(SysCallMapDict dict)
{
    dict->IDs = malloc(sizeof(uint32) * SysCallMapBlockSize);

    if (IsNull(dict->IDs))
    {
        Panic(ID_MALLOC_FAIL);
        return false;
    }
    dict->Maps = malloc(sizeof(SysCallMap) * SysCallMapBlockSize);
    if (IsNull(dict->Maps))
    {
        Panic(ID_MALLOC_FAIL);
        return false;
    }
    return true;
}
bool ExpandSysCallMap(SysCallMap map)
{
    uint32 NewSize = map->SysCallMapBufSize + SysCallMapBlockSize;
    uint32 *newID = realloc(map->IDs, sizeof(uint32) * NewSize);
    Syscall *Calls;
    if (IsNull(newID))
    {
        Panic(ID_REALLOC_FAIL);
        return false;
    }
    Calls = realloc(map->Calls, sizeof(Syscall) * NewSize);
    if (IsNull(Calls))
    {
        Panic(ID_REALLOC_FAIL);
        return false;
    }
    map->IDs = newID;
    map->Calls = Calls;
    map->SysCallMapBufSize = NewSize;
    return true;
}
bool ExpandSysCallMapDict(SysCallMapDict dict)
{
    SysCallMap *Maps;
    uint32 NewSize = dict->DictBufSize + SysCallMapBlockSize;
    uint32 *newID = realloc(dict->IDs, sizeof(uint32) * NewSize);
    if (IsNull(newID))
    {
        Panic(ID_REALLOC_FAIL);
        return false;
    }
    Maps = realloc(dict->Maps, sizeof(SysCallMap) * NewSize);
    if (IsNull(Maps))
    {
        Panic(ID_REALLOC_FAIL);
        return false;
    }
    dict->IDs = newID;
    dict->Maps = Maps;
    dict->DictBufSize = NewSize;
    return true;
}
bool SetSysCall(SysCallMapDict dict, Syscall call, uint32 Namespace, uint32 ID)
{
    uint32 i = 0;
    int CurrentIndex;
    SysCallMap map;
    for (; i < dict->DictCount; i++)
    {
        uint32 _ID = dict->IDs[i];
        if (_ID == Namespace)
        {
            return SetSysCallInMap(dict->Maps[i], call, ID);
        }
    }
    if (dict->DictCount >= dict->DictBufSize)
    {
        if (!ExpandSysCallMapDict(dict))
        {
            return false;
        }
    }
    CurrentIndex = dict->DictCount;
    dict->IDs[CurrentIndex] = ID;
    map = CreateSysCallMap();
    if (!InitSysCallMap(map))
    {
        return false;
    }
    dict->Maps[CurrentIndex] = map;
    dict->DictCount = CurrentIndex + 1;
    return SetSysCallInMap(map, call, ID);
}
bool SetSysCallInMap(SysCallMap map, Syscall call, uint32 ID)
{
    uint32 i = 0;
    uint32 _ID;
    int CurrentIndex;
    for (; i < map->SysCallCount; i++)
    {
        _ID = map->IDs[i];
        if (_ID == ID)
        {
            map->Calls[i] = call;
            return true;
        }
    }
    if (map->SysCallCount >= map->SysCallMapBufSize)
    {
        ExpandSysCallMap(map);
    }
    CurrentIndex = map->SysCallCount;
    map->IDs[CurrentIndex] = ID;
    map->Calls[CurrentIndex] = call;
    map->SysCallCount = CurrentIndex + 1;
    return true;
}
