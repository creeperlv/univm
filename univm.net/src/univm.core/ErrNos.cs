namespace univm.core
{
    public static class ErrNos
    {
        public const uint Unknown = 0xFFFF_FFFF;
        public const uint OutOfBoundary = 0x0000_0001;
        public const uint OutOfMemory = 0x0000_0002;
        public const uint MemIDNotExist = 0x0000_0003;
        public const uint SysCallNotExist = 0x0000_0004;
        public const uint ResourceNotFound= 0x0000_0005;
    }
}