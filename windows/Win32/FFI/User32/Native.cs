using System;
using System.Runtime.InteropServices;
using Win32.FFI.User32.Definition;

namespace Win32.FFI.User32
{
    public class Native
    {
        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern nint LoadCursor(nint hInstance, LoadCursorA lpCursorName);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern nint LoadIcon(nint hInstance, LoadIconA lpIconName);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern ushort RegisterClassEx([In] ref WindowClassEx wndClassEx);
        
        public static nint CreateWindow(
            string className,
            string title,
            WindowStyles windowStyles,
            int x,
            int y,
            int width,
            int height,
            nint hWndParent,
            nint hMenu,
            nint hInstance,
            nint pvParam) => 
            CreateWindowEx(ExtendedWindowStyles.WS_EX_APPWINDOW, className, title, windowStyles, x, y, width, height, hWndParent, hMenu, hInstance, pvParam);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern nint CreateWindowEx(
            ExtendedWindowStyles extendedWindowStyles,
            string className,
            string title,
            WindowStyles windowStyles,
            int x,
            int y,
            int width,
            int height,
            nint hWndParent,
            nint hMenu,
            nint hInstance,
            nint pvParam);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern nint DefWindowProc(nint hWnd, WndMessage msg, nint wParam, nint lParam);


        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(nint hWnd, out RECT lpRect);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern bool GetClientRect(nint hWnd, out RECT lpRect);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern nint DispatchMessage([In] ref MSG msg);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
        
        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out MSG msg, nint hWnd, uint messageFilterMin, uint messageFilterMax, PeekMessageA flags);
        
        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern bool TranslateMessage([In] ref MSG msg);
        
        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern void PostQuitMessage(int nExitCode);
        
        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern nint SendMessage(IntPtr hWnd, WndMessage Msg, nuint wParam, nint lParam);
        
        
        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern bool ShowWindow(nint hWnd, ShowWindowFlags flags);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern bool SetWindowText(nint hWnd, string lpString);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern bool SetWindowPos(nint hWnd, WndInsertAfter hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags flags);


        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        private static extern nint GetWindowLongPtr(nint hWnd, GWL index);

        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        private static extern nint SetWindowLongPtr(nint hWnd, GWL index, nint dwNewLong);
        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern bool UpdateWindow(nint hWnd);
        
        [DllImport(Lib.User32, CharSet = Properties.BuildCharSet)]
        public static extern bool DestroyWindow(nint hwnd);

        
        
        public static WindowStyles GetWindowStyle(nint hWnd) =>
            (WindowStyles)GetWindowLongPtr(hWnd, GWL.GWL_STYLE);

        public static bool SetWindowStyle(nint hWnd, WindowStyles windowStyles) =>
            SetWindowLongPtr(hWnd, GWL.GWL_STYLE, (int)windowStyles) == 0;

        public static object GetUserData(nint hWnd) =>
            GCHandle.FromIntPtr(GetWindowLongPtr(hWnd, GWL.GWL_USERDATA)).Target;

        public static bool SetUserData<T>(nint hWnd, T data) =>
            SetWindowLongPtr(hWnd, GWL.GWL_USERDATA, (nint)GCHandle.Alloc(data)) == 0;
    }
}