#ifndef __UNIVM_DISPATCHER
#define __UNIVM_DISPATCHER
#include "../core.h"
#include "thread.h"
#define GENERIC_DATA_ID_DEF_DISPATCHER 0x00001000
typedef struct _defaultDispatcherData
{
    VMCore *cores;
    uint32 CoreSize;
    uint32 CoreCount;
} defaultDispatcherData;
typedef defaultDispatcherData *DefaultDispatcherData;
void SetupDefaultDispatcherCreator(VM vm);
#endif
