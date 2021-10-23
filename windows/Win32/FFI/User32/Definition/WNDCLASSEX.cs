using System.Runtime.InteropServices;

namespace Win32.FFI.User32.Definition
{
    public delegate nint WndProc(nint hWnd, WndMessage msg, nint wParam, nint lParam);

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct WNDCLASSEX
    {
        public uint cbSize;
        public uint style;
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public WndProc lpfnWndProc;
        public int cbClsExtra;
        public int cbWndExtra;
        public nint hInstance;
        public nint hIcon;
        public nint hCursor;
        public nint hbrBackground;
        public string lpszMenuName;
        public string lpszClassName;
        public nint hIconSm;
    }
}