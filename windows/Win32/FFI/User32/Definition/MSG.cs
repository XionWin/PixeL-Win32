using System.Runtime.InteropServices;

namespace Win32.FFI.User32.Definition
{
    [StructLayout(LayoutKind.Sequential)]
    public struct MSG
    {
        public nint hWnd;
        public WndMessage msg;
        public nint wParam;
        public nint lParam;
        public uint time;
        public POINT pt;
    }
}