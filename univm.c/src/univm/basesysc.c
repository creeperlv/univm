#include "basesysc.h"
void ReleaseFILE(Resource res)
{
    if (res->DataType == _BSD_STYLE_SYSCALL_DATA_TYPE_FILE)
    {
        if (res->IsInited == true)
            if (IsNotNull(res->Data))
            {
                fclose((FILE *)res->Data);
                res->Data = NULL;
            }
    }
}
void InitResource_FILE(Resource resource, FILE *file)
{
    resource->DataType = _BSD_STYLE_SYSCALL_DATA_TYPE_FILE;
    resource->IsInited = true;
    resource->Data = file;
    resource->Release = ReleaseFILE;
}
//Resource stdin_res;
//Resource stdout_res;
//Resource stderr_res;
bool RedirectStdIO(VM vm){
    Resource res = malloc(sizeof(resource));
    InitResource_FILE(res, stdin);
    AttachResource(vm, res);
    res = malloc(sizeof(resource));
    InitResource_FILE(res, stdout);
    AttachResource(vm, res);
    res = malloc(sizeof(resource));
    InitResource_FILE(res, stderr);
    AttachResource(vm, res);
    return true;
}
void WRITE(VMCore core)
{
}
void READ(VMCore core)
{
}
void CHDIR(VMCore core)
{
}
void OPEN(VMCore core)
{
}
void CLOSE(VMCore core)
{
}
void FSYSC(VMCore core)
{
}
void MKDIR(VMCore core)
{
}
bool SetupSysCall_Base_0(SysCallMapDict dict)
{
    SetSysCall(dict, READ, 0, 3);
    SetSysCall(dict, WRITE, 0, 4);
    SetSysCall(dict, OPEN, 0, 5);
    SetSysCall(dict, CLOSE, 0, 6);
    SetSysCall(dict, CHDIR, 0, 12);
    SetSysCall(dict, FSYSC, 0, 95);
    SetSysCall(dict, MKDIR, 0, 136);
    return true;
}
