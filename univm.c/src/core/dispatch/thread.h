#ifndef __UNIVM_THREADING
#define __UNIVM_THREADING
#include "../core.h"

#ifdef _MSC_VER
#define WIN32THREAD 1
#endif
#ifndef MAX_THREAD
#define MAX_THREAD 32
#endif
#ifndef WIN32THREAD
#include <pthread.h>
void StartNewThread(void *(*func)(void*), void* data);
#else
#include <windows.h>
#include <process.h>
typedef void (*ThreadFunc)(void *);
void StartNewThread(void (*func)(void *), void *data);
#endif
void ThreadPoolInit();
#endif
