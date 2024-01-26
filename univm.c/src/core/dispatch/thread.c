#include "thread.h"

univm_thread_data threads[UNIVM_MAX_THREAD];

int CurrentThreadID;
bool UniVMThreadIsRunning(uint32 id)
{
    return threads[id].IsRunning;
}

void mssleep(int ms)
{
#ifndef WIN32THREAD
    time_t sec = ms / 1000;
    long ns = (ms % 1000) * 1000000;
    struct timespec req = {sec, ns};
    nanosleep(&req, NULL);
#else
    Sleep(ms);
#endif
}
UNIVM_TRETURN_TYPE WrapperThreadFunc(void *_data)
{

    UniVMThreadData data = (UniVMThreadData)_data;
    data->func(data->data);
    data->IsRunning = false;
    UNIVM_TRETURN;
}
int UniVMStartNewThread(void (*func)(void *), void *data)
{
    if (CurrentThreadID >= UNIVM_MAX_THREAD)
    {
        CurrentThreadID = 0;
    }
    if (threads[CurrentThreadID].IsRunning)
    {
        CurrentThreadID = 0;
        while (1)
        {
            if (threads[CurrentThreadID].IsRunning == false)
            {
                break;
            }
            else
            {
                CurrentThreadID++;
            }
            if (CurrentThreadID >= UNIVM_MAX_THREAD)
            {
                CurrentThreadID = 0;
#ifndef CANCEL_WHEN_FULL_TREHAD_POOL
                mssleep(THREAD_WAIT_SLEEP);
#else
                return -1;
#endif
            }
        }
    }
    threads[CurrentThreadID].data = data;
    threads[CurrentThreadID].IsRunning = true;
    threads[CurrentThreadID].func = func;
#ifdef WIN32THREAD
    threads[CurrentThreadID].handle = (HANDLE)_beginthread(WrapperThreadFunc, 0, &threads[CurrentThreadID]);
#else
    pthread_create(&threads[CurrentThreadID].handle, NULL, WrapperThreadFunc, &threads[CurrentThreadID]);
#endif
    CurrentThreadID++;
    return CurrentThreadID - 1;
}
