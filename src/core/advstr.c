#include "advstr.h"

StrPtrArr AllocStrPtrArr() {
  StrPtrArr arr = malloc(sizeof(strPtrArr));
  if (IsNull(arr)) {
    Panic(ID_MALLOC_FAIL);
    return NULL;
  }
  char **HEAD = malloc(sizeof(char *) * STR_PTR_ARR_BLOCK_SIZE);
  if (IsNull(HEAD)) {
    free(arr);
    Panic(ID_MALLOC_FAIL);
    return NULL;
  }
  arr->HEAD = HEAD;
  arr->Count = 0;
  arr->Size = STR_PTR_ARR_BLOCK_SIZE;
  return arr;
}
bool ExpandStrPtrArr(StrPtrArr arr) {
  uint32 newSize = arr->Size + STR_PTR_ARR_BLOCK_SIZE;
  char **HEAD = realloc(arr->HEAD, sizeof(char *) * newSize);
  if (IsNull(HEAD)) {
    Panic(ID_REALLOC_FAIL);
    return false;
  }
  arr->HEAD = HEAD;
  arr->Size = newSize;
  return true;
}
bool AppendStrPtrArr(StrPtrArr arr, char *string) {
  if (arr->Count >= arr->Size) {
    if (ExpandStrPtrArr(arr)) {
      return AppendStrPtrArr(arr, string);
    }
    return false;
  } else {
    arr->HEAD[arr->Count] = string;
    arr->Count++;
    return true;
  }
}
bool CStrIsEqualCStr(char *L, char *R) {
  uint32 LL = strlen(L);
  uint32 RL = strlen(R);
  if (LL != RL)
    return false;
  for (uint32 i = 0; i < LL; i++) {
    if (L[i] != R[i])
      return false;
  }
  return true;
}
bool TrimUnusedStrPtrArrAllocation(StrPtrArr arr) {

  uint32 newSize = arr->Count;
  if (newSize == arr->Size) {
    return true;
  }
  char **HEAD = realloc(arr->HEAD, sizeof(char *) * newSize);
  if (HEAD == NULL) {
    if (newSize != 0) {

      Panic(ID_REALLOC_FAIL);
      return false;
    }
  }
  arr->HEAD = HEAD;
  arr->Size = newSize;
  return true;
}
