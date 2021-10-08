using System.Runtime.InteropServices;
using Win32.FFI.User32.Definition;

namespace Win32.FFI.User32
{
    public class Native
    {
        [DllImport(Lib.User32)]
        public static extern nint LoadCursor(nint hInstance, LoadCursorA lpCursorName);

        [DllImport(Lib.User32)]
        public static extern nint LoadIcon(nint hInstance, LoadIconA lpIconName);

        [DllImport(Lib.User32)]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern ushort RegisterClassEx([In] ref WNDCLASSEX wndClassEx);
        
        [DllImport(Lib.User32)]
        public unsafe static extern nint CreateWindowEx(
            ExtendedWindowStyles exStyle,
            string className,
            string windowName,
            WindowStyles style,
            int x,
            int y,
            int width,
            int height,
            nint hWndParent,
            nint hMenu,
            nint hInstance,
            nint pvParam);

        [DllImport(Lib.User32)]
        public static extern nint DefWindowProc(nint hWnd, WndMessage msg, nint wParam, nint lParam);


        [DllImport(Lib.User32)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport(Lib.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(nint hWnd, out RECT lpRect);

        [DllImport(Lib.User32)]
        public static extern bool GetClientRect(nint hWnd, out RECT lpRect);

        [DllImport(Lib.User32)]
        public static extern nint DispatchMessage([In] ref MSG msg);

        [DllImport(Lib.User32)]
        public static extern bool GetMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
        
        [DllImport(Lib.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out MSG msg, nint hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);
        
        [DllImport(Lib.User32)]
        public static extern bool TranslateMessage([In] ref MSG msg);
        
        [DllImport(Lib.User32)]
        public static extern void PostQuitMessage(int nExitCode);
        
        
        [DllImport(Lib.User32)]
        public static extern bool ShowWindow(nint hWnd, ShowWindowFlags flags);

        [DllImport(Lib.User32)]
        public static extern bool SetWindowText(nint hWnd, string lpString);

        [DllImport(Lib.User32)]
        public static extern bool SetWindowPos(nint hWnd, WndInsertAfter hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags flags);

        [DllImport(Lib.User32)]
        public static extern bool UpdateWindow(nint hWnd);
        
        [DllImport(Lib.User32)]
        public static extern bool DestroyWindow(nint hwnd);
    }
}