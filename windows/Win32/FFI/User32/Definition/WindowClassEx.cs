using System.Runtime.InteropServices;

namespace Win32.FFI.User32.Definition
{
    public delegate nint WndProc(nint hWnd, WndMessage msg, nint wParam, nint lParam);

    [StructLayout(LayoutKind.Sequential, CharSet = Properties.BuildCharSet)]
    public struct WindowClassEx
    {
        public uint Size;
        public WindowClassStyles Styles;
        [MarshalAs(UnmanagedType.FunctionPtr)] public WndProc WndProc;
        public int ClassExtraBytes;
        public int WindowExtraBytes;
        public nint InstanceHandle;
        public nint IconHandle;
        public nint CursorHandle;
        public nint BackgroundBrushHandle;
        public string MenuName;
        public string ClassName;
        public nint SmallIconHandle;
    }
}