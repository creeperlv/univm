#ifndef UNIVM_BASE
#define UNIVM_BASE
#include "panic.h"
//#include <endian.h>
#include <stdarg.h>
#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

#define null NULL
#define IsNull(x) x == NULL
#define IsNotNull(x) x != NULL

#ifndef CALLSTACK_BLOCK_SIZE
#define CALLSTACK_BLOCK_SIZE 16
#endif

#ifndef CALLSTACK_MAX_SIZE
#define CALLSTACK_MAX_SIZE 256
#endif

#ifndef ProgramCountBlockSize
#define ProgramCountBlockSize 16
#endif

#ifndef MAX_REGISTER_COUNT
#define MAX_REGISTER_COUNT 64
#endif


#ifndef MemBlockSize
#define MemBlockSize 16
#endif

#ifndef SysCallMapBlockSize
#define SysCallMapBlockSize 32
#endif

typedef uint8_t byte;
typedef uint32_t uint32;
typedef int32_t int32;
typedef int64_t int64;
typedef uint64_t uint64;

void SetInternalOutput(FILE *f);
void Log(char *fmt, ...);
void WriteLine(char *str);
void SetPanicHandler(void (*Func)(int));
void Panic(int ID);
void DefaultPanicHandler(int ID);
#endif
