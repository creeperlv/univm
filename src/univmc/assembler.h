#ifndef UNIVM_ASSEMBLER
#define UNIVM_ASSEMBLER
#include "../core/advstr.h"
#include "../core/core.h"
typedef struct _compileTimeData {
  Program program;
  StrStrDict TextDictionary;
  StrStrDict ReplacementDictionary;
  NameInt32Dict LabelDictionary;
} compileTimeData;
typedef compileTimeData CompileTimeData;

#define ASSEMBLER_SCANLINE_OK 1
#define ASSEMBLER_SCANLINE_REACH_END 2
#define ASSEMBLER_SCANLINE_FAIL 0

uint32 ScanLine(FILE *f, CompileTimeData data);

#endif
