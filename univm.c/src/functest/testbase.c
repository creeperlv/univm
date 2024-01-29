#include "testbase.h"

bool WriteOutput;
bool IsWriteOutput()
{
	return WriteOutput;
}
void SetWriteOutput(bool Value){
	WriteOutput=Value;
}
void FunctionPass(int *Result, char *FuncName)
{
	if (WriteOutput)
		Log("%s:\x1b[92m pass\x1b[0m\n", FuncName);
}
void FunctionFail(int *Result, char *FuncName)
{
	if (WriteOutput)
		Log("%s:\x1b[91m fail\x1b[0m\n", FuncName);
	Result[0] = 1;
}
