#ifndef __univm_testbase
#define __univm_testbase
#include "../core/advstr.h"
#include "../core/core.h"
void SetWriteOutput(bool Value);
bool IsWriteOutput();
void FunctionPass(int *Result, char *FuncName);
void FunctionFail(int *Result, char *FuncName);
void FuncPass(char *FuncName);
void FuncFail(char *FuncName);
int Assert_IsTrue(bool v, char *FuncName);
int Assert_IsTrue_OnlyShowFail(bool v, char *FuncName);
#endif
