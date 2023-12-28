#ifndef UNIVM_ASSEMBLER
#define UNIVM_ASSEMBLER
#include "../core/advstr.h"
#include "../core/core.h"
#include "../core/data.h"

typedef struct _asmDefinition {
  NameUInt32Dict Lables;
  StrStrDict TextDictionary;
  StrStrDict ReplacementDictionary;
} asmDefinition;

typedef asmDefinition AsmDefinition;

typedef struct _compileTimeData {
  Program program;
  StrStrDict TextDictionary;
  StrStrDict ReplacementDictionary;
  NameInt32Dict LabelDictionary;
  AsmDefinition AsmDefinitions;
} compileTimeData;
typedef compileTimeData CompileTimeData;

#define ASSEMBLER_SCANLINE_OK 1
#define ASSEMBLER_SCANLINE_REACH_END 2
#define ASSEMBLER_SCANLINE_FAIL 0

uint32 LoadAsmDefinition(FILE *f, CompileTimeData data);
uint32 ScanLine(FILE *f, CompileTimeData data);

#endif
