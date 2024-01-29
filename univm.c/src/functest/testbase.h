#ifndef __univm_testbase
#define __univm_testbase
#include "../core/advstr.h"
#include "../core/core.h"
void SetWriteOutput(bool Value);
bool IsWriteOutput();
void FunctionPass(int *Result, char *FuncName);
void FunctionFail(int *Result, char *FuncName);
#endif
