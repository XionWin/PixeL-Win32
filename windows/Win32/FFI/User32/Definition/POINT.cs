using System.Runtime.InteropServices;

namespace Win32.FFI.User32.Definition
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public int x;
        public int y;
    }
}