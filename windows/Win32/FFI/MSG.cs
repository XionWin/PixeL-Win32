using System.Runtime.InteropServices;

namespace Win32.FFI
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        public nint hWnd;
        public uint msg;
        public nint wParam;
        public nint lParam;
        public uint time;
        public POINT pt;
    }
}