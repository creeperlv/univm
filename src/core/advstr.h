#ifndef UNIVM_ADVSTR
#define UNIVM_ADVSTR
#include "base.h"
#include <string.h>
typedef struct _strPtrArr {
  char **HEAD;
  uint32 Count;
  uint32 Size;
} strPtrArr;
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
