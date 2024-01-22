#include "../core/core.h"
int main(int argc, char **argv) {
  SetInternalOutput(stdout);
  SetPanicHandler(DefaultPanicHandler);
  if (argc == 1)
    WriteLine("No Input Module");
  else if (argc == 2) {
    char *name = argv[1];
    FILE *f = fopen(name, "rb");
    if (IsNull(f)) {
      Log("Target file \"%s\" not exist!\n", name);
    }
    UniVMAsm MainModule = CreateProgram();
    if (LoadProgram(f, MainModule) == false) {
        Log("Program Load Failed!\n");
    }
    else {

    }
  }
  return 0;
}
