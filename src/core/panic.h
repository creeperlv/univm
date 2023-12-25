#ifndef UNIVM_PANIC
#define UNIVM_PANIC

#define ID_GENERIC 0x0000
#define ID_MALLOC_FAIL 0x0001
#define ID_REALLOC_FAIL 0x0002
#define ID_UNKNOWN_SYSCALL 0x0100
#define ID_UNKNOWN_INST 0x0101

#define MSG_GENERIC "Generic Failure"
#define MSG_MALLOC_FAIL "malloc() failed."
#define MSG_REALLOC_FAIL "realloc() failed."
#define MSG_UNKNOWN_SYSCALL "Unknown syscall."
#define MSG_UNKNOWN_INST "Unknown instruction."

char *QueryPanicMessage(int ID);

#endif
