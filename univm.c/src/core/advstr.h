#ifndef UNIVM_ADVSTR
#define UNIVM_ADVSTR
#include "base.h"
#include <string.h>
typedef struct _strPtrArr
{
    char **HEAD;
    uint32 Count;
    uint32 Size;
} strPtrArr;
typedef struct _vStr
{
    char *HEAD;
    uint32 Count;
    uint32 Size;
} vStr;
typedef vStr *VStr;

#ifndef VSTR_BLOCK_SIZE
#define VSTR_BLOCK_SIZE 32
#endif
VStr CreateVStr();
VStr CreateVStrValue(char *Value);
bool InitVStr(VStr str);
bool ExpandVStr(VStr str);
bool AppendVStr(VStr str, char c);
bool FreeVStr(VStr str);
bool VStrIsEqualsToCStr(VStr L, char *R);
bool VStrIsStartWithCStr(VStr L, char *R);
typedef strPtrArr *StrPtrArr;
#ifndef STR_PTR_ARR_BLOCK_SIZE
#define STR_PTR_ARR_BLOCK_SIZE 4
#endif
StrPtrArr AllocStrPtrArr();
bool ExpandStrPtrArr(StrPtrArr arr);
bool AppendStrPtrArr(StrPtrArr arr, char *string);
bool TrimUnusedStrPtrArrAllocation(StrPtrArr arr);
bool CStrIsEqualCStr(char *L, char *R);
#endif
