#include "base.h"

FILE *output;

void SetInternalOutput(FILE *f)
{
	output = f;
}
void __printf(char *fmt, va_list args)
{
	if (IsNull(output))
	{
	}
	else
	{
		vfprintf(output, fmt, args);
		fflush(output);
	}
}
void Log(char *fmt, ...)
{
	va_list l;
	va_start(l, fmt);
	__printf(fmt, l);
	va_end(l);
}
void WriteLine(char *str)
{
	if (IsNull(output))
	{
	}
	else
	{
		fprintf(output, "%s\n", str);
		fflush(output);
	}
}
void (*PanicHandler)(int);
void SetPanicHandler(void (*Func)(int))
{
	PanicHandler = Func;
}
void Panic(int ID)
{
	if (PanicHandler != NULL)
	{
		PanicHandler(ID);
	}
}

void DefaultPanicHandler(int ID)
{
	printf("PANIC:%s\n", QueryPanicMessage(ID));
}
void InterruptiveStdOutPanicHandler(int ID)
{
	printf("PANIC:%s\n", QueryPanicMessage(ID));
	exit(ID);
}

bool __of_add_int8(int8_t *V, int8_t L, int8_t R)
{
	int16 _V = L + R;
	V[0] = ((int8_t *)&_V)[0];
	if (_V > INT8_MAX)
	{
		return true;
	}
	return false;
}
bool __of_sub_int8(int8_t *V, int8_t L, int8_t R)
{
	int16 _V = L - R;
	V[0] = ((int8_t *)&_V)[0];
	if (_V < INT8_MIN)
	{
		return true;
	}
	return false;
}
bool __of_mul_int8(int8_t *V, int8_t L, int8_t R)
{
	int16 _V = L * R;
	V[0] = ((int8_t *)&_V)[0];
	if (_V > INT8_MAX || _V < INT8_MIN)
	{
		return true;
	}
	return false;
}
bool __of_div_int8(int8_t *V, int8_t L, int8_t R)
{
	int16 _V = L / R;
	V[0] = ((int8_t *)&_V)[0];
	if (_V < INT8_MIN || _V > INT8_MAX)
	{
		return true;
	}
	return false;
}
bool __of_add_uint8(uint8 *V, uint8 L, uint8 R)
{
	uint16 _V = L + R;
	V[0] = ((uint8 *)&_V)[0];
	if (_V>UINT8_MAX)
	{
		return true;
	}
	return false;
}
bool __of_sub_uint8(uint8 *V, uint8 L, uint8 R)
{
	uint8 _V = L - R;
	V[0] = _V;
	if (_V > L)
	{
		return true;
	}
	return false;
}
bool __of_mul_uint8(uint8 *V, uint8 L, uint8 R)
{
	uint16 _V = L * R;
	V[0] = ((uint8 *)&_V)[0];
	if (_V > UINT8_MAX)
	{
		return true;
	}
	return false;
}
bool __of_div_uint8(uint8 *V, uint8 L, uint8 R)
{
	uint16 _V = L / R;
	V[0] = ((uint8 *)&_V)[0];
	if (_V > UINT8_MAX)
	{
		return true;
	}
	return false;
}
bool __of_add_int32(int32 *V, int32 L, int32 R)
{
#ifdef no__builtin___overflow
	#ifdef NEW_MSVC_INTRINSICS
	return _add_overflow_i32(0, L, R, V) != 0;
	#endif
	long _v = L + R;
	V[0] = ((int32 *)&_v)[0];
	if (_v > INT32_MAX)
		return true;
	return false;
#else
	return __builtin_add_overflow(L, R, V);
#endif
}
bool __of_mul_int32(int32 *V, int32 L, int32 R)
{
#ifdef no__builtin___overflow
	#ifdef NEW_MSVC_INTRINSICS
	return _add_overflow_i32(0, L, R, V) != 0;
	#endif
	int64 _v = L * R;
	V[0] = ((int32 *)&_v)[0];
	if (_v > INT32_MAX || _v < INT32_MIN)
		return true;
	return false;
#else
	return __builtin_mul_overflow(L, R, V);
#endif
}
bool __of_div_int32(int32 *V, int32 L, int32 R)
{
	int64 _v = L / R;
	V[0] = ((int32 *)&_v)[0];
	if (_v > INT32_MAX || _v < INT32_MIN)
		return true;
	return false;
}
bool __of_sub_int32(int32 *V, int32 L, int32 R)
{
#ifdef no__builtin___overflow
	#ifdef NEW_MSVC_INTRINSICS
	return _sub_overflow_i32(0, L, R, V) != 0;
	#endif
	long _v = L - R;
	V[0] = ((int32 *)&_v)[0];
	if (_v < INT32_MIN)
		return true;
	return false;
#else
	return __builtin_sub_overflow(L, R, V);
#endif
}