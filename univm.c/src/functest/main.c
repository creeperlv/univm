#include "../core/advstr.h"
#include "../core/core.h"
bool WriteOutput;
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
void TestVMCoreData(int *Result)
{
	coreData data;
	vmCore core;
	int Data_Int32 = 0;
	uint32 Data_UInt32 = 0;
	int TestCount = 256;
	int i = 0;
	int Delta_Int32 = INT32_MAX / TestCount;
	uint32 Delta_UInt32 = UINT32_MAX / TestCount;
	for (; i < TestCount; i++)
	{
		Data_Int32 = i * Delta_Int32;
		SetRegister_Int32(&data, Data_Int32, i);
		if (GetRegister_Int32(&data, i) != Data_Int32)
		{
			FunctionFail(Result, "SetRegister_Int32() and GetRegister_Int32()");
			if (Result[0] != 0)
			{
				return;
			}
		}
	}
	FunctionPass(Result, "SetRegister_Int32() and GetRegister_Int32()");
	for (i = 0; i < TestCount; i++)
	{
		Data_Int32 = i * Delta_UInt32;
		SetRegister_UInt32(&data, Data_Int32, i);
		if (GetRegister_UInt32(&data, i) != Data_Int32)
		{
			FunctionFail(Result, "SetRegister_UInt32() and GetRegister_UInt32()");
			if (Result[0] != 0)
			{
				return;
			}
		}
	}
	FunctionPass(Result, "SetRegister_UInt32() and GetRegister_UInt32()");
}
int main(int ac, char **av)
{
	vStr str;
	char *output = getenv("NO_OUTPUT");
	int Result = 0;
	WriteOutput = true;
	if (output != NULL)
	{
		if (output[0] == '1')
		{
			WriteOutput = false;
		}
		else
		{
			WriteOutput = true;
		}
	}
	SetInternalOutput(stdout);
	InitVStr(&str);
	AppendVStr(&str, 'a');
	AppendVStr(&str, 'b');
	AppendVStr(&str, 'c');
	if (WriteOutput)
		WriteLine("Internal Function Test");
	if (VStrIsEqualsToCStr(&str, "abc") && (!VStrIsEqualsToCStr(&str, "abcd")))
	{
		FunctionPass(&Result, "VStrIsEqualCStr()");
	}
	else
	{
		FunctionFail(&Result, "VStrIsEqualCStr()");
	}
	if (Result != 0)
		return Result;
	if (VStrIsStartWithCStr(&str, "abc") && VStrIsStartWithCStr(&str, "ab") && (!VStrIsStartWithCStr(&str, "abcd")))
	{
		FunctionPass(&Result, "VStrIsStartWithCStr()");
	}
	else
	{
		FunctionFail(&Result, "VStrIsStartWithCStr()");
	}
	if (Result != 0)
		return Result;
	TestVMCoreData(&Result);
	free(str.HEAD);
	return Result;
}
