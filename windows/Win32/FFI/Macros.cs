namespace Win32.FFI
{
    public static class Macros
    {
        public static ushort LOWORD(nint value)
        {
            return (ushort)(uint)value;
        }

        public static ushort HIWORD(nint value)
        {
            return (ushort)((uint)value >> 16);
        }
    }
}