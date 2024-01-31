#include "core.h"

int32 GetInt32FromMemoryPtr(Runtime rt, memoryPtr ptr)
{
	byte *base = rt->machine.Mem[ptr.MemID].Ptr;
	base += ptr.Offset;
	return ((int32 *)base)[0];
}
uint32 GetUInt32FromMemoryPtr(Runtime rt, memoryPtr ptr)
{
	byte *base = rt->machine.Mem[ptr.MemID].Ptr;
	base += ptr.Offset;
	return ((uint32 *)base)[0];
}

int32 GetRegister_Int32(CoreData core, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	return ((int32 *)base)[0];
}
int16 GetRegister_Int16(CoreData core, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	return ((int16 *)base)[0];
}
int64 GetRegister_Int64(CoreData core, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	return ((int64 *)base)[0];
}
uint64 GetRegister_UInt64(CoreData core, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	return ((int64 *)base)[0];
}
Single GetRegister_Single(CoreData core, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	return ((Single *)base)[0];
}
Double GetRegister_Double(CoreData core, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	return ((Double *)base)[0];
}
uint32 GetRegister_UInt32(CoreData core, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	return ((uint32 *)base)[0];
}

bool SetRegister_Int16(CoreData core, int16 Data, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	((int16 *)base)[0] = Data;
	return true;
}
bool SetRegister_Int32(CoreData core, int32 Data, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	((int32 *)base)[0] = Data;
	return true;
}
bool SetRegister_UInt32(CoreData core, uint32 Data, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	((uint32 *)base)[0] = Data;
	return true;
}
bool SetRegister_Single(CoreData core, Single Data, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	((Single *)base)[0] = Data;
	return true;
}
bool SetRegister_Double(CoreData core, Double Data, uint32 startIndex)
{
	uint8_t *base = core->Registers;
	base += startIndex;
	((Double *)base)[0] = Data;
	return true;
}
int64 GetInt64FromMemoryPtr(Runtime rt, memoryPtr ptr)
{
	byte *base = rt->machine.Mem[ptr.MemID].Ptr;
	base += ptr.Offset;
	return ((int64 *)base)[0];
}
uint64 GetUInt64FromMemoryPtr(Runtime rt, memoryPtr ptr)
{
	byte *base = rt->machine.Mem[ptr.MemID].Ptr;
	base += ptr.Offset;
	return ((uint64 *)base)[0];
}

int32 GetInt32FromLE(byte *buffer, int offset)
{
	byte *base = buffer + offset;
#ifndef BIG_ENDIAN
	return ((int32 *)base)[0];
#else
	byte buf[4] = {base[3], base[2], base[1], base[0]};
	return ((int32 *)(&buf))[0];
#endif
}
uint32 GetUInt32FromLE(byte *buffer, int offset)
{
	byte *base = buffer + offset;
#ifndef BIG_ENDIAN
	return ((uint32 *)base)[0];
#else
	byte buf[4] = {base[3], base[2], base[1], base[0]};
	return ((uint32 *)(&buf))[0];
#endif
}
int64 GetInt64FromLE(byte *buffer, int offset)
{
	byte *base = buffer + offset;
#ifndef BIG_ENDIAN
	return ((int64 *)base)[0];
#else
	byte buf[8] = {base[7], base[6], base[5], base[4], base[3], base[2], base[1], base[0]};
	return ((int64 *)(&buf))[0];
#endif
}
uint64 GetUInt64FromLE(byte *buffer, int offset)
{
	byte *base = buffer + offset;
#ifndef BIG_ENDIAN
	return ((uint64 *)base)[0];
#else
	byte buf[8] = {base[7], base[6], base[5], base[4], base[3], base[2], base[1], base[0]};
	return ((uint64 *)(&buf))[0];
#endif
}
