namespace univm.core.Definitions
{
    public static class ErrNos
    {
        public const uint Unknown = 0xFFFF_FFFF;
        public const uint UndefinedBehaviour = 0xFFFF_FFFE;
        public const uint OutOfBoundary = 0x0000_0001;
        public const uint OutOfMemory = 0x0000_0002;
        public const uint MemIDNotExist = 0x0000_0003;
        public const uint SysCallNotExist = 0x0000_0004;
        public const uint ResourceNotFound = 0x0000_0005;
    }
    public static class ArchInfoIDs
    {
        public const uint VM_VER_MAJOR = 0x00000000;
        public const uint VM_VER_MINOR = 0x00000001;
        public const uint VM_VER_BUILD = 0x00000002;
        public const uint VM_VER_PATCH = 0x00000003;
        public const uint ISA_VER_MAJOR = 0x00000004;
        public const uint ISA_VER_MINOR = 0x00000005;
        public const uint ISA_VER_BUILD = 0x00000006;
        public const uint ISA_VER_PATCH = 0x00000007;
        public const uint ManufacturerID = 0x00000008;
        public const uint ManufacturerName = 0x00000009;
        public const uint ProductID = 0x0000000A;
        public const uint ProductName = 0x0000000B;
        public const uint ProductMinorStep = 0x0000000C;
        public const uint ProductRevision = 0x0000000D;
        public const uint MaxDispatchers = 0x0000000E;
    }
    public static class Infos
    {
        public const uint VM_VER_MAJOR = 1;
        public const uint VM_VER_MINOR = 0;
        public const uint VM_VER_BUILD = 1;
        public const uint VM_VER_PATCH = 0;
        public const uint ISA_VER_MAJOR = 1;
        public const uint ISA_VER_MINOR = 0;
        public const uint ISA_VER_BUILD = 0;
        public const uint ISA_VER_PATCH = 0;

        public const uint ManufacturerID = 0;
        public const string ManufacturerName = "Generic";
        public const uint ProductID = 0;
        public const string ProductName = "Generic";
    }
}