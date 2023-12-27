#ifndef UNIVM_ASSEMBLER
#define UNIVM_ASSEMBLER
#include "../core/core.h"
typedef struct _compileTimeData {
  Program program;
  StrStrDict TextDictionary;
  StrStrDict ReplacementDictionary;
  NameInt32Dict LabelDictionary;
} compileTimeData;
typedef compileTimeData CompileTimeData;
bool ScanLine(FILE *f, CompileTimeData data);

#endif
