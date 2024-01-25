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
#define UNIVM_TRETURN_TYPE void *
#define UNIVM_TRETURN return NULL
#include <pthread.h>
#else
#include <windows.h>
#include <process.h>
#define UNIVM_TRETURN_TYPE void
#define UNIVM_TRETURN return
typedef void (*ThreadFunc)(void *);
//void StartNewThread(void (*func)(void *), void *data);
#endif
void StartNewThread(UNIVM_TRETURN_TYPE (*func)(void *), void *data);
void ThreadPoolInit();
#endif
