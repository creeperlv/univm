#ifndef UNIVM_ASSEMBLER
#define UNIVM_ASSEMBLER
#include "../core/core.h"
typedef struct _compileTimeData {
  Program program;

} compileTimeData;
typedef compileTimeData CompileTimeData;
bool ScanLine(FILE *f, CompileTimeData data);

#endif
