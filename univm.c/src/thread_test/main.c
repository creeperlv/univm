#include "../core/dispatch/thread.h"

UNIVM_TRETURN_TYPE TestFunc(void *data)
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
        printf("%s\n", (char *)data);
    }
    UNIVM_TRETURN;
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
