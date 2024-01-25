#include "../core/core.h"
#include "../core/vm.h"
#include "basesysc.h"
int main(int argc, char **argv)
{
    uniVMAsm *MainModule;
    SetInternalOutput(stdout);
    SetPanicHandler(DefaultPanicHandler);
    if (argc == 1)
        WriteLine("No Input Module");
    else if (argc == 2)
    {
        char *name = argv[1];
        FILE *f = fopen(name, "rb");
        if (IsNull(f))
        {
            Log("Target file \"%s\" not exist!\n", name);
            return -1;
        }
        MainModule = CreateProgram();
        if (LoadProgram(f, MainModule) == false)
        {
            Log("Program Load Failed!\n");
        }
        else
        {
            vm _vm;
            InitVM(&_vm);
            SetupSysCall_Base_0(_vm.CallMap);
            RedirectStdIO(&_vm);
            Log("Current resources:%d\n", _vm.CurrentRuntime->machine.ResourceCount);

            ReleaseVM(&_vm);
        }
    }
    return 0;
}
