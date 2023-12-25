#include "core.h"

int32 GetInt32FromMemoryPtr(Runtime rt, memoryPtr ptr) {
  byte *base = rt->Mem[ptr.MemID].Ptr;
  base += ptr.Offset;
  return ((int32 *)base)[0];
}
uint32 GetUInt32FromMemoryPtr(Runtime rt, memoryPtr ptr) {
  byte *base = rt->Mem[ptr.MemID].Ptr;
  base += ptr.Offset;
  return ((uint32 *)base)[0];
}

int64 GetInt64FromMemoryPtr(Runtime rt, memoryPtr ptr) {
  byte *base = rt->Mem[ptr.MemID].Ptr;
  base += ptr.Offset;
  return ((int64 *)base)[0];
}
uint64 GetUInt64FromMemoryPtr(Runtime rt, memoryPtr ptr) {
  byte *base = rt->Mem[ptr.MemID].Ptr;
  base += ptr.Offset;
  return ((uint64 *)base)[0];
}

int32 GetInt32FromLE(byte *buffer, int offset) {
  byte *base = buffer + offset;
#ifndef BIG_ENDIAN
  return ((int32 *)base)[0];
#else
  byte buf[4] = {base[3], base[2], base[1], base[0]};
  return ((int32 *)(&buf))[0];
#endif
}
uint32 GetUInt32FromLE(byte *buffer, int offset) {
  byte *base = buffer + offset;
#ifndef BIG_ENDIAN
  return ((uint32 *)base)[0];
#else
  byte buf[4] = {base[3], base[2], base[1], base[0]};
  return ((uint32 *)(&buf))[0];
#endif
}
int64 GetInt64FromLE(byte *buffer, int offset) {
  byte *base = buffer + offset;
#ifndef BIG_ENDIAN
  return ((int64 *)base)[0];
#else
  byte buf[8] = {base[7], base[6], base[5], base[4],
                 base[3], base[2], base[1], base[0]};
  return ((int64 *)(&buf))[0];
#endif
}
uint64 GetUInt64FromLE(byte *buffer, int offset) {
  byte *base = buffer + offset;
#ifndef BIG_ENDIAN
  return ((uint64 *)base)[0];
#else
  byte buf[8] = {base[7], base[6], base[5], base[4],
                 base[3], base[2], base[1], base[0]};
  return ((uint64 *)(&buf))[0];
#endif
}
