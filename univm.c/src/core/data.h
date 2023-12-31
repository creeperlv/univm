#ifndef UNIVM_DATA
#define UNIVM_DATA
#include "base.h"
Single ReadSingle(FILE *f, byte *buf);
Double ReadDouble(FILE *f, byte *buf);
int32 ReadInt32(FILE *f, byte *buf);
uint32 ReadUInt32(FILE *f, byte *buf);

#endif
