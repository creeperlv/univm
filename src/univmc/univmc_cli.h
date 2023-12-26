#ifndef UNIVMC_CLI
#define UNIVMC_CLI
#include "../core/advstr.h"
#include "../core/core.h"
typedef struct _cliOptions {
  StrPtrArr input_file;
  StrPtrArr SearchLib;
  char *output_file;
} cliOptions;
typedef cliOptions *CliOptions;
bool ParseOptions(CliOptions options, int argc, char **argv);
#endif
