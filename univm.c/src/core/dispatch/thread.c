#include "thread.h"

#ifdef WIN32THREAD
HANDLE threads[MAX_THREAD];
#else
pthread_t threads[MAX_THREAD];
#endif
uint32 CurrentThreadID;

void StartNewThread(UNIVM_TRETURN_TYPE (*func)(void *), void *data)
{
#ifdef WIN32THREAD
    threads[CurrentThreadID] = (HANDLE)_beginthread(func, 0, data);
#else
    pthread_create(&threads[CurrentThreadID], NULL, func, data);
#endif
}
