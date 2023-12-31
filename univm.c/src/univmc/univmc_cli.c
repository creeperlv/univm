#include "univmc_cli.h"

bool ParseOptions(CliOptions options, int argc, char **argv) {
  options->input_file = AllocStrPtrArr();
  if (options->input_file == NULL) {
    WriteLine("Fail to initialize input files array!");
    return false;
  }
  options->SearchLib = AllocStrPtrArr();
  for (int i = 1; i < argc; i++) {
    char *item = argv[i];
    if (CStrIsEqualCStr(item, "-o")) {
      i++;
      item = argv[i];
      options->output_file = item;
    } else if (CStrIsEqualCStr(item, "-I")) {
      i++;
      item = argv[i];
      AppendStrPtrArr(options->SearchLib, item);
    } else {
      if (!AppendStrPtrArr(options->input_file, item)) {
        WriteLine("Fail to append input file!");
      }
    }
  }
  if (!TrimUnusedStrPtrArrAllocation(options->input_file)) {
    WriteLine("Trim Input files failed!");
    return false;
  }
  if (!TrimUnusedStrPtrArrAllocation(options->SearchLib)) {
    WriteLine("Trim search lib failed!");
    return false;
  }
  return true;
}
