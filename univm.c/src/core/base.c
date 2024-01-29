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
