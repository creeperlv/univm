#include "dispatch.h"
void __def_init(UniVMDispatcherInterface _data)
{
    DefaultDispatcherData data; 
    _data->Data.TypeID = GENERIC_DATA_ID_DEF_DISPATCHER;
    data = (DefaultDispatcherData)malloc(sizeof(defaultDispatcherData));
    _data->Data.Data = data;
}
void __def_real_run(UniVMDispatcherInterface _data)
{
    uint32 i;
    uint32 Count;
    DefaultDispatcherData data;
    CoreData cdata;
    data = (DefaultDispatcherData)_data->Data.Data;
    while (1)
    {
        Count = data->CoreCount;
        for (i = 0; i < Count; i++)
        {
            cdata = data->cores[i]->CoreData;
        }
    }
}

UNIVM_TRETURN_TYPE __def_run_thread(void *data)
{
    __def_real_run(data);
    UNIVM_TRETURN;
}
void __def_run(UniVMDispatcherInterface _data)
{
    StartNewThread(__def_run_thread, _data);
}
UniVMDispatcherInterface ____create__dispatcher()
{
    UniVMDispatcherInterface _data = malloc(sizeof(dispatcherInterface));
    _data->Init = __def_init;
    return _data;
}
void SetupDefaultDispatcherCreator(VM vm)
{
    vm->CurrentRuntime->DispatcherCreator.CreateDispatcher = ____create__dispatcher;
}
