#include "testbase.h"
void TestVMCoreData(int *Result)
{
	int _r;
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
		_r = Assert_IsTrue_OnlyShowFail(GetRegister_Int32(&data, i) == Data_Int32,
										"SetRegister_Int32() and GetRegister_Int32()");
		if (_r != 0)
		{
			Result[0] = _r;
			return;
		}
	}
	FuncPass("SetRegister_Int32() and GetRegister_Int32()");
	for (i = 0; i < TestCount; i++)
	{
		Data_Int32 = i * Delta_UInt32;
		SetRegister_UInt32(&data, Data_Int32, i);
		Result[0] = Assert_IsTrue_OnlyShowFail(GetRegister_UInt32(&data, i) == Data_Int32,
											   "SetRegister_UInt32() and GetRegister_UInt32()");
		if (_r != 0)
		{
			Result[0] = _r;
			return;
		}
	}
	FunctionPass(Result, "SetRegister_UInt32() and GetRegister_UInt32()");
}
void Int32Overflow(int *Result)
{
	int32 L;
	int32 R;
	int32 RET_V;
	int32 TGT_V;
	bool IsOF;
	{
		L = INT32_MAX;
		R = 10;
		TGT_V = L + R;
		IsOF = __of_add_int32(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_add_int32()");
		}
		else
		{
			FunctionPass(Result, "__of_add_int32()");
		}
	}
	{
		L = INT32_MIN;
		R = 10;
		TGT_V = L - R;
		IsOF = __of_sub_int32(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_sub_int32()");
		}
		else
			FunctionPass(Result, "__of_sub_int32()");
	}
	{
		L = INT32_MAX;
		R = 10;
		TGT_V = L * R;
		IsOF = __of_mul_int32(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_mul_int32()");
		}
		else
			FunctionPass(Result, "__of_mul_int32()");
	}
}
int UInt8Overflow()
{
	int Result = 0;
	int _r;
	uint8 L;
	uint8 R;
	uint8 RET_V;
	uint8 TGT_V;
	bool IsOF;
	{
		L = UINT8_MAX;
		R = 10;
		TGT_V = L + R;
		IsOF = __of_add_uint8(&RET_V, L, R);
		_r = Assert_IsTrue((IsOF && TGT_V == RET_V), "__of_add_uint8()");
		if (_r != 0)
			Result = _r;
	}
	{
		L = 0;
		R = 10;
		TGT_V = L - R;
		IsOF = __of_sub_uint8(&RET_V, L, R);
		_r =  Assert_IsTrue((IsOF && TGT_V == RET_V), "__of_sub_uint8()");
		if (_r != 0)
			Result = _r;
	}
	{
		L = UINT8_MAX;
		R = 10;
		TGT_V = L * R;
		IsOF = __of_mul_uint8(&RET_V, L, R);
		_r = Assert_IsTrue((IsOF && TGT_V == RET_V), "__of_mul_uint8()");
		if (_r != 0)
			Result = _r;
	}
	return Result;
}
void Int16Overflow(int *Result)
{
	int16 L;
	int16 R;
	int16 RET_V;
	int16 TGT_V;
	bool IsOF;
	{
		L = INT16_MAX;
		R = 10;
		TGT_V = L + R;
		IsOF = __of_add_int16(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_add_int16()");
		}
		else
		{
			FunctionPass(Result, "__of_add_int16()");
		}
	}
	{
		L = INT16_MIN;
		R = 10;
		TGT_V = L - R;
		IsOF = __of_sub_int16(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_sub_int16()");
		}
		else
			FunctionPass(Result, "__of_sub_int16()");
	}
	{
		L = INT16_MAX;
		R = 10;
		TGT_V = L * R;
		IsOF = __of_mul_int16(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_mul_int16()");
		}
		else
			FunctionPass(Result, "__of_mul_int16()");
	}
	{
		L = INT16_MIN;
		R = -1;
		TGT_V = L / R;
		MathResult mr = __of_div_int16(&RET_V, L, R);
		if (mr != MATH_RESULT_OVERFLOW || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_div_int16()");
		}
		else
		{
			R = 0;
			mr = __of_div_int16(&RET_V, L, R);
			if (mr == MATH_RESULT_DIVIDE_BY_ZERO)
			{
				FunctionPass(Result, "__of_div_int16()");
			}
			else
			{
				FunctionFail(Result, "__of_div_int16()");
			}
		}
	}
}
void UInt16Overflow(int *Result)
{
	uint16 L;
	uint16 R;
	uint16 RET_V;
	uint16 TGT_V;
	bool IsOF;
	{
		L = UINT16_MAX;
		R = 10;
		TGT_V = L + R;
		IsOF = __of_add_uint16(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_add_uint16()");
		}
		else
		{
			FunctionPass(Result, "__of_add_uint16()");
		}
	}
	{
		L = 0;
		R = 10;
		TGT_V = L - R;
		IsOF = __of_sub_uint16(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_sub_uint16()");
		}
		else
			FunctionPass(Result, "__of_sub_uint16()");
	}
	{
		L = UINT16_MAX;
		R = 10;
		TGT_V = L * R;
		IsOF = __of_mul_uint16(&RET_V, L, R);
		if (IsOF == false || TGT_V != RET_V)
		{
			FunctionFail(Result, "__of_mul_uint16()");
		}
		else
			FunctionPass(Result, "__of_mul_uint16()");
	}
}
void OverflowTest(int *Result)
{
	Int32Overflow(Result);
	Result[0] = UInt8Overflow();
	Int16Overflow(Result);
	UInt16Overflow(Result);
}
int main(int ac, char **av)
{
	vStr str;
	char *output = getenv("NO_OUTPUT");
	int Result = 0;
	SetWriteOutput(true);
	if (output != NULL)
	{
		if (output[0] == '1')
		{
			SetWriteOutput(false);
		}
		else
		{
			SetWriteOutput(true);
		}
	}
	SetInternalOutput(stdout);
	InitVStr(&str);
	AppendVStr(&str, 'a');
	AppendVStr(&str, 'b');
	AppendVStr(&str, 'c');
	if (IsWriteOutput())
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
	if (Result != 0)
	{
		return Result;
	}
	OverflowTest(&Result);
	if (Result != 0)
	{
		return Result;
	}
	free(str.HEAD);
	return Result;
}
