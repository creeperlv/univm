#ifndef __UNIVM_THREADING
#define __UNIVM_THREADING
#include "../core.h"

#ifndef UNIVM_MAX_THREAD
#define UNIVM_MAX_THREAD 32
#endif

#ifndef THREAD_WAIT_SLEEP
#define THREAD_WAIT_SLEEP 10
#endif
#ifdef _MSC_VER
#define WIN32THREAD 1
#endif
#ifndef WIN32THREAD
#define UniVMThreadHandle pthread_t
#define UNIVM_TRETURN_TYPE void *
#define UNIVM_TRETURN return NULL
#include <pthread.h>
#include <time.h>
#else
#define UniVMThreadHandle HANDLE
#include <process.h>
#include <windows.h>
#define UNIVM_TRETURN_TYPE void
#define UNIVM_TRETURN return
typedef void (*ThreadFunc)(void *);
// void StartNewThread(void (*func)(void *), void *data);
#endif
typedef struct _univm_thread_data
{
    UniVMThreadHandle handle;
    bool IsRunning;
    void (*func)(void *);
    void *data;
} univm_thread_data;
typedef univm_thread_data *UniVMThreadData;
void mssleep(int ms);
bool UniVMThreadIsRunning(uint32 id);
int UniVMStartNewThread(void (*func)(void *), void *data);
#endif
