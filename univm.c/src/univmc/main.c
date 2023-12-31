#include "univmc_cli.h"
int main(int argc, char **argv) {
  SetPanicHandler(InterruptiveStdOutPanicHandler);
  SetInternalOutput(stdout);
  cliOptions options;
  if (!ParseOptions(&options, argc, argv)) {
    WriteLine("Wrong Parameters");
  }
  WriteLine("UNIVM ISA Assembler");
  for (uint32 i = 0; i < options.input_file->Count; i++) {
    Log("Source:%s\n", options.input_file->HEAD[i]);
  }
  for (uint32 i = 0; i < options.SearchLib->Count; i++) {
    Log("Lib:%s\n", options.SearchLib->HEAD[i]);
  }
  return 0;
}
