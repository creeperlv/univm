#ifndef UNIVM_BASEDATA
#define UNIVM_BASEDATA
#include "advstr.h"
#include "base.h"

#ifndef DICT_BLOC_SIZE
#define DICT_BLOC_SIZE 16
#endif

typedef struct _nameInt32Dict
{
    char **Names;
    int32 *Data;
    uint32 Size;
    uint32 Count;
} nameInt32Dict;
typedef struct _strStrDict
{
    char **Keys;
    char **Values;
    uint32 Size;
    uint32 Count;
} strStrDict;
typedef struct _nameUInt32Dict
{
    char **Names;
    uint32 *Data;
    uint32 Size;
    uint32 Count;
} nameUInt32Dict;

typedef nameInt32Dict *NameInt32Dict;
typedef strStrDict *StrStrDict;
typedef nameUInt32Dict *NameUInt32Dict;

NameInt32Dict CreateNameInt32Dict();
NameUInt32Dict CreateNameUInt32Dict();
StrStrDict CreateStrStrDict();

bool InitNameInt32Dict(NameInt32Dict dict);
bool InitNameUInt32Dict(NameUInt32Dict dict);
bool InitStrStrDict(StrStrDict dict);

bool ExpandNameInt32Dict(NameInt32Dict dict);
bool ExpandNameUInt32Dict(NameUInt32Dict dict);
bool ExpandStrStrDict(StrStrDict dict);

bool SetNameInt32Dict(NameInt32Dict dict, char *Name, int32 Data);
bool SetNameUInt32Dict(NameUInt32Dict dict, char *Name, uint32 Data);
bool SetStrStrDict(StrStrDict dict, char *Key, char *Value);

bool NameInt32Dict_ContainsKey(NameInt32Dict dict, char *Name);
bool NameUInt32Dict_ContainsKey(NameUInt32Dict dict, char *Name);
bool StrStrDict_ContainsKey(StrStrDict dict, char *Key);

void FreeNameInt32DictWithoutContent(NameInt32Dict dict);
void FreeNameUInt32DictWithoutContent(NameUInt32Dict dict);
void FreeStrStrDictWithoutContent(StrStrDict dict);

#endif
