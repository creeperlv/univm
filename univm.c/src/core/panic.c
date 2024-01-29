#include "panic.h"

char *QueryPanicMessage(int ID)
{
	switch (ID)
	{
		case ID_UNKNOWN_SYSCALL:
			return MSG_UNKNOWN_SYSCALL;
		case ID_UNKNOWN_INST:
			return MSG_UNKNOWN_INST;
		case ID_MALLOC_FAIL:
			return MSG_MALLOC_FAIL;
		case ID_REALLOC_FAIL:
			return MSG_REALLOC_FAIL;
		case ID_GENERIC:
		default:
			return MSG_GENERIC;
	}
}
