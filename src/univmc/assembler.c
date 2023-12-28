#include "assembler.h"
uint32 LoadAsmDefinition(FILE *f, CompileTimeData data) {
  byte buf4[4];
  byte buf8[8];
  uint32 TextCount = ReadUInt32(f, buf4);
  uint32 IDCount = ReadUInt32(f, buf4);
}
uint32 ScanLine(FILE *f, CompileTimeData data) {
  vStr str;
  uint32 Result = ASSEMBLER_SCANLINE_OK;
  if (!InitVStr(&str)) {
    return ASSEMBLER_SCANLINE_FAIL;
  }
  bool IsComment = false;
  while (1) {
    int c = fgetc(f);
    if (c == EOF) {
      Result = ASSEMBLER_SCANLINE_REACH_END;
      goto LINE_DONE;
    }
    if (c == ';' || c == '#') {
      IsComment = true;
    }
    if (c == '\n' || c == '\r') {
      goto LINE_DONE;
    }
    if (!IsComment)
      if (!AppendVStr(&str, c)) {
        return ASSEMBLER_SCANLINE_FAIL;
      }
  }
LINE_DONE:;
  // CLEAN_UP
  free(str.HEAD);
  return Result;
}
