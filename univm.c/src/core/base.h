#ifndef UNIVM_BASE
#define UNIVM_BASE
#include "panic.h"
// #include <endian.h>
#include <stdarg.h>
#ifdef _MSC_VER
#if _MSC_VER <= 1500
#define NOSTDBOL 1
#define NOSTDINT 1
#endif
#endif
#ifndef NOSTDBOL
#include <stdbool.h>
#else
#define bool char
#define true 1
#define false 0
#endif

#ifndef NOSTDINT
#include <stdint.h>
#else
#include "types.h"
#endif
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#ifdef _WIN32
#include <io.h>
#else
#include <unistd.h>
#endif
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

#ifndef SysCallMapDictBlockSize
#define SysCallMapDictBlockSize 4
#endif
#ifndef ResourceBufBlockSize
#define ResourceBufBlockSize 4
#endif

typedef float Single;
typedef double Double;

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
void InterruptiveStdOutPanicHandler(int ID);
#endif
