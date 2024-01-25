
#include "../core/dispatch/thread.h"

void TestFunc(void *_data)
{
    int i = 0;
    int a = 0;
    int c = 0;
    for (i = 0; i < 50; i++)
    {
        for (a = 0; a < 2000; a++)
        {
            c = i * a;
        }
        mssleep(10);
        printf("%s\n", (char *)_data);
    }
 
}
int main()
{
    uint32 id0;
    uint32 id1;
    char *str0 = "Thread0";
    char *str1 = "Thread1";
    id0 = UniVMStartNewThread(TestFunc, str0);
    id1 = UniVMStartNewThread(TestFunc, str1);
    while (1)
    {
        if (!UniVMThreadIsRunning(id0)&&!UniVMThreadIsRunning(id1))
        {
            return 0;
        }
    }
    //    fgetc(stdin);
    return 0;
}
