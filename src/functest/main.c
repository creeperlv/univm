#include "../core/advstr.h"
#include "../core/core.h"
void FunctionPass(int *Result, char *FuncName) {
  Log("%s:\x1b[92m pass\x1b[0m\n", FuncName);
}
void FunctionFail(int *Result, char *FuncName) {
  Log("%s:\x1b[91m fail\x1b[0m\n", FuncName);
  Result[0] = 1;
}
int main(int ac, char **av) {
  vStr str;
  SetInternalOutput(stdout);
  InitVStr(&str);
  AppendVStr(&str, 'a');
  AppendVStr(&str, 'b');
  AppendVStr(&str, 'c');
  int Result = 0;
  WriteLine("Internal Function Test");
  if (VStrIsEqualsToCStr(&str, "abc") && (!VStrIsEqualsToCStr(&str, "abcd"))) {
    FunctionPass(&Result, "VStrIsEqualCStr()");
  } else {
    FunctionFail(&Result, "VStrIsEqualCStr()");
  }
  if (VStrIsStartWithCStr(&str, "abc") && VStrIsStartWithCStr(&str, "ab") &&
      (!VStrIsStartWithCStr(&str, "abcd"))) {
    FunctionPass(&Result, "VStrIsStartWithCStr()");
  } else {
    FunctionFail(&Result, "VStrIsStartWithCStr()");
  }

  free(str.HEAD);
  return Result;
}
