#include "thread.h"

#ifdef WIN32THREAD
HANDLE threads[MAX_THREAD];
#else
pthread_t threads[MAX_THREAD];
#endif
uint32 CurrentThreadID;
#ifdef WIN32THREAD

void StartNewThread(void (*func)(void *), void *data)
#else
void StartNewThread(void *(*func)(void *), void *data)
#endif
{
#ifdef WIN32THREAD
    threads[CurrentThreadID] = (HANDLE)_beginthread(func, 0, data);
#else
    pthread_create(&threads[CurrentThreadID], NULL, func, data);
#endif
}
