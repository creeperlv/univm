#include "advstr.h"

bool InitVStr(VStr str)
{
	str->HEAD = malloc(sizeof(char) * VSTR_BLOCK_SIZE);
	if (IsNull(str->HEAD))
	{
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	str->Size = VSTR_BLOCK_SIZE;
	str->Count = 0;
	return true;
}
bool ExpandVStr(VStr str)
{
	uint32 Size = str->Size + VSTR_BLOCK_SIZE;
	char *HEAD = realloc(str->HEAD, sizeof(char) * Size);
	if (IsNull(HEAD))
	{
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	str->HEAD = HEAD;
	str->Size = Size;
	return true;
}
bool AppendVStr(VStr str, char c)
{
	if (str->Count >= str->Size)
	{
		if (!ExpandVStr(str))
		{
			return false;
		}
	}
	str->HEAD[str->Count] = c;
	str->Count++;
	return true;
}
bool FreeVStr(VStr str)
{
	free(str->HEAD);
	free(str);
	return true;
}

bool VStrIsEqualsToCStr(VStr L, char *R)
{
	uint32 RLen = (uint32)strlen(R);
	uint32 i = 0;
	if (L->Count != RLen)
	{
		return false;
	}
	for (; i < RLen; i++)
	{
		if (L->HEAD[i] != R[i])
			return false;
	}
	return true;
}
bool VStrIsStartWithCStr(VStr L, char *R)
{
	uint32 RLen = (uint32)strlen(R);
	uint32 i = 0;
	if (L->Count < RLen)
	{
		return false;
	}
	for (; i < RLen; i++)
	{
		if (L->HEAD[i] != R[i])
			return false;
	}
	return true;
}

StrPtrArr AllocStrPtrArr()
{
	StrPtrArr arr = malloc(sizeof(strPtrArr));
	char **HEAD;
	if (IsNull(arr))
	{
		Panic(ID_MALLOC_FAIL);
		return NULL;
	}
	HEAD = malloc(sizeof(char *) * STR_PTR_ARR_BLOCK_SIZE);
	if (IsNull(HEAD))
	{
		free(arr);
		Panic(ID_MALLOC_FAIL);
		return NULL;
	}
	arr->HEAD = HEAD;
	arr->Count = 0;
	arr->Size = STR_PTR_ARR_BLOCK_SIZE;
	return arr;
}
bool ExpandStrPtrArr(StrPtrArr arr)
{
	uint32 newSize = arr->Size + STR_PTR_ARR_BLOCK_SIZE;
	char **HEAD = realloc(arr->HEAD, sizeof(char *) * newSize);
	if (IsNull(HEAD))
	{
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	arr->HEAD = HEAD;
	arr->Size = newSize;
	return true;
}
bool AppendStrPtrArr(StrPtrArr arr, char *string)
{
	if (arr->Count >= arr->Size)
	{
		if (ExpandStrPtrArr(arr))
		{
			return AppendStrPtrArr(arr, string);
		}
		return false;
	}
	else
	{
		arr->HEAD[arr->Count] = string;
		arr->Count++;
		return true;
	}
}
bool CStrIsEqualCStr(char *L, char *R)
{
	uint32 LL = (uint32)strlen(L);
	uint32 RL = (uint32)strlen(R);
	uint32 i = 0;
	if (LL != RL)
		return false;
	for (; i < LL; i++)
	{
		if (L[i] != R[i])
			return false;
	}
	return true;
}
bool TrimUnusedStrPtrArrAllocation(StrPtrArr arr)
{
	char **HEAD;
	uint32 newSize = arr->Count;
	if (newSize == arr->Size)
	{
		return true;
	}
	HEAD = realloc(arr->HEAD, sizeof(char *) * newSize);
	if (HEAD == NULL)
	{
		if (newSize != 0)
		{

			Panic(ID_REALLOC_FAIL);
			return false;
		}
	}
	arr->HEAD = HEAD;
	arr->Size = newSize;
	return true;
}
