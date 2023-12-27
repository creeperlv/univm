#include "assembler.h"
uint32 ScanLine(FILE *f, CompileTimeData data) {
  vStr str;
  uint32 Result = ASSEMBLER_SCANLINE_OK;
  if (!InitVStr(&str)) {
    return ASSEMBLER_SCANLINE_FAIL;
  }
  while (1) {
    char c = fgetc(f);
    if (c == EOF) {
      Result = ASSEMBLER_SCANLINE_REACH_END;
      goto LINE_DONE;
    }
    if (c == '\n' || c == '\r') {
      goto LINE_DONE;
    }
    if (!AppendVStr(&str, c)) {
      return ASSEMBLER_SCANLINE_FAIL;
    }
  }
LINE_DONE:;
CLEAN_UP:
  free(str.HEAD);
  return Result;
}
