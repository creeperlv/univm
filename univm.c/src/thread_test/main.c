#include "../core/dispatch/thread.h"
#ifdef WIN32THREAD
void TestFunc(void *data)
{
#else
void *TestFunc(void *data)
{
#endif
    int i = 0;
    int a = 0;
    int c = 0;
    for (i = 0; i < 50; i++)
    {
        for (a = 0; a < 2000; a++)
        {
            c = i * a;
        }
        printf("%s\n", (char *)data);
    }
#ifndef WIN32THREAD
    return NULL;
#endif
}
int main()
{
    char *str0 = "Thread0";
    char *str1 = "Thread1";
    StartNewThread(TestFunc, str0);
    StartNewThread(TestFunc, str1);
    fgetc(stdin);
    return 0;
}
