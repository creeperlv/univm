#include "basedata.h"

NameInt32Dict CreateNameInt32Dict()
{
	NameInt32Dict dict = malloc(sizeof(nameInt32Dict));
	if (IsNull(dict))
	{
		Panic(ID_MALLOC_FAIL);
		return null;
	}
	dict->Names = null;
	dict->Data = null;
	dict->Size = 0;
	dict->Count = 0;
	return dict;
}
NameUInt32Dict CreateNameUInt32Dict()
{
	NameUInt32Dict dict = malloc(sizeof(nameUInt32Dict));
	if (IsNull(dict))
	{
		Panic(ID_MALLOC_FAIL);
		return null;
	}
	dict->Names = null;
	dict->Data = null;
	dict->Size = 0;
	dict->Count = 0;
	return dict;
}
StrStrDict CreateStrStrDict()
{
	StrStrDict dict = malloc(sizeof(strStrDict));
	if (IsNull(dict))
	{
		Panic(ID_MALLOC_FAIL);
		return null;
	}
	dict->Keys = null;
	dict->Values = null;
	dict->Size = 0;
	dict->Count = 0;
	return dict;
}

bool InitNameInt32Dict(NameInt32Dict dict)
{
	dict->Names = malloc(sizeof(char *) * DICT_BLOC_SIZE);
	if (IsNull(dict->Names))
	{
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	dict->Data = malloc(sizeof(int) * DICT_BLOC_SIZE);
	if (IsNull(dict->Data))
	{
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	dict->Size = DICT_BLOC_SIZE;
	dict->Count = 0;
	return true;
}
bool InitNameUInt32Dict(NameUInt32Dict dict)
{
	dict->Names = malloc(sizeof(char *) * DICT_BLOC_SIZE);
	if (IsNull(dict->Names))
	{
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	dict->Data = malloc(sizeof(uint32) * DICT_BLOC_SIZE);
	if (IsNull(dict->Data))
	{
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	dict->Size = DICT_BLOC_SIZE;
	dict->Count = 0;
	return true;
}
bool InitStrStrDict(StrStrDict dict)
{
	dict->Keys = malloc(sizeof(char *) * DICT_BLOC_SIZE);
	if (IsNull(dict->Keys))
	{
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	dict->Values = malloc(sizeof(char *) * DICT_BLOC_SIZE);
	if (IsNull(dict->Values))
	{
		Panic(ID_MALLOC_FAIL);
		return false;
	}
	dict->Size = DICT_BLOC_SIZE;
	dict->Count = 0;
	return true;
}

bool ExpandNameInt32Dict(NameInt32Dict dict)
{
	uint32 Size = dict->Size + DICT_BLOC_SIZE;
	int *Data;
	char **Names = realloc(dict->Names, sizeof(char *) * Size);
	if (IsNull(Names))
	{
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	Data = realloc(dict->Data, sizeof(int) * Size);
	if (IsNull(Data))
	{
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	dict->Names = Names;
	dict->Data = Data;
	dict->Size = Size;
	return true;
}
bool ExpandNameUInt32Dict(NameUInt32Dict dict)
{
	uint32 Size = dict->Size + DICT_BLOC_SIZE;
	uint32 *Data;
	char **Names = realloc(dict->Names, sizeof(char *) * Size);
	if (IsNull(Names))
	{
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	Data = realloc(dict->Data, sizeof(uint32) * Size);
	if (IsNull(Data))
	{
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	dict->Names = Names;
	dict->Data = Data;
	dict->Size = Size;
	return true;
}
bool ExpandStrStrDict(StrStrDict dict)
{
	uint32 Size = dict->Size + DICT_BLOC_SIZE;
	char **Values;
	char **Keys = realloc(dict->Keys, sizeof(char *) * Size);
	if (IsNull(Keys))
	{
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	Values = realloc(dict->Values, sizeof(char *) * Size);
	if (IsNull(Values))
	{
		Panic(ID_REALLOC_FAIL);
		return false;
	}
	dict->Keys = Keys;
	dict->Values = Values;
	dict->Size = Size;
	return true;
}

bool SetNameInt32Dict(NameInt32Dict dict, char *Name, int32 Data)
{
	uint32 i = 0;
	for (; i < dict->Size; i++)
	{
		if (CStrIsEqualCStr(dict->Names[i], Name))
		{
			dict->Data[i] = Data;
			return true;
		}
	}
	if (dict->Count >= dict->Size)
	{
		if (!ExpandNameInt32Dict(dict))
		{
			return false;
		}
	}
	dict->Names[dict->Count] = Name;
	dict->Data[dict->Count] = Data;
	dict->Count++;
	return true;
}
bool SetNameUInt32Dict(NameUInt32Dict dict, char *Name, uint32 Data)
{
	uint32 i = 0;
	for (; i < dict->Size; i++)
	{
		if (CStrIsEqualCStr(dict->Names[i], Name))
		{
			dict->Data[i] = Data;
			return true;
		}
	}
	if (dict->Count >= dict->Size)
	{
		if (!ExpandNameUInt32Dict(dict))
		{
			return false;
		}
	}
	dict->Names[dict->Count] = Name;
	dict->Data[dict->Count] = Data;
	dict->Count++;
	return true;
}
bool SetStrStrDict(StrStrDict dict, char *Key, char *Value)
{
	uint32 i = 0;
	for (; i < dict->Size; i++)
	{
		if (CStrIsEqualCStr(dict->Keys[i], Key))
		{
			dict->Values[i] = Value;
			return true;
		}
	}
	if (dict->Count >= dict->Size)
	{
		if (!ExpandStrStrDict(dict))
		{
			return false;
		}
	}
	dict->Keys[dict->Count] = Key;
	dict->Values[dict->Count] = Value;
	dict->Count++;
	return true;
}

bool NameInt32Dict_ContainsKey(NameInt32Dict dict, char *Name)
{
	uint32 i = 0;
	for (; i < dict->Size; i++)
	{
		if (CStrIsEqualCStr(dict->Names[i], Name))
		{
			return true;
		}
	}
	return false;
}
bool NameUInt32Dict_ContainsKey(NameUInt32Dict dict, char *Name)
{
	uint32 i = 0;
	for (; i < dict->Size; i++)
	{
		if (CStrIsEqualCStr(dict->Names[i], Name))
		{
			return true;
		}
	}
	return false;
}
bool StrStrDict_ContainsKey(StrStrDict dict, char *Key)
{
	uint32 i = 0;
	for (; i < dict->Size; i++)
	{
		if (CStrIsEqualCStr(dict->Keys[i], Key))
		{
			return true;
		}
	}
	return false;
}

void FreeNameInt32DictWithoutContent(NameInt32Dict dict)
{
	uint32 i = 0;
	for (; i < dict->Count; i++)
	{
		dict->Names[i] = null;
	}
	free(dict);
}
void FreeNameUInt32DictWithoutContent(NameUInt32Dict dict)
{
	uint32 i = 0;
	for (; i < dict->Count; i++)
	{
		dict->Names[i] = null;
	}
	free(dict);
}
void FreeStrStrDictWithoutContent(StrStrDict dict)
{
	uint32 i = 0;
	for (; i < dict->Count; i++)
	{
		dict->Keys[i] = null;
		dict->Values[i] = null;
	}
	free(dict);
}
