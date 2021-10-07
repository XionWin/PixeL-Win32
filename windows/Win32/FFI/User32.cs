using System;
using System.Runtime.InteropServices;

namespace Win32.FFI
{
    public static class User32
    {
        public const int COLOR_WINDOW = 5;

        public const int CW_USEDEFAULT = unchecked((int)0x80000000);

        public const int IDC_ARROW = 32512;

        public const int IDI_APPLICATION = 32512;

        public const int PM_REMOVE = 0x0001;

        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;

        public const uint SWP_NOSIZE = 0x0001;
        public const uint SWP_NOMOVE = 0x0002;
        public const uint SWP_NOZORDER = 0x0004;

        public const uint WM_MOVE = 0x0003;
        public const uint WM_SIZE = 0x0005;
        public const uint WM_CLOSE = 0x0010;
        public const uint WM_QUIT = 0x0012;
        public const uint WM_DESTROY = 0x0002;
        public const uint WM_PAINT = 0x000F;
        public const uint WM_ERASEBKGND = 0x0014;
        public const uint WM_KEYDOWN = 0x0100;
        public const uint WM_KEYUP = 0x0101;
        public const uint WM_CHAR = 0x0102;
        public const uint WM_SYSKEYDOWN = 0x0104;
        public const uint WM_SYSKEYUP = 0x0105;
        public const uint WM_UNICHAR = 0x0109;
        public const uint WM_SYSCOMMAND = 0x0112;
        public const uint WM_MOUSEMOVE = 0x0200;
        public const uint WM_LBUTTONDOWN = 0x0201;
        public const uint WM_LBUTTONUP = 0x0202;
        public const uint WM_RBUTTONDOWN = 0x0204;
        public const uint WM_RBUTTONUP = 0x0205;
        public const uint WM_MBUTTONDOWN = 0x0207;
        public const uint WM_MBUTTONUP = 0x0208;
        public const uint WM_ENTERSIZEMOVE = 0x0231;
        public const uint WM_EXITSIZEMOVE = 0x0232;
        public const uint WM_MOUSEHOVER = 0x02A1;
        public const uint WM_MOUSELEAVE = 0x02A3;

        public const int WS_CAPTION = 0x00C00000;
        public const int WS_MAXIMIZEBOX = 0x00010000;
        public const int WS_MINIMIZEBOX = 0x00020000;
        public const int WS_OVERLAPPED = 0x00000000;
        public const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000;

        public const int WS_EX_APPWINDOW = 0x00040000;
        public const int WS_EX_WINDOWEDGE = 0x00000100;

        [StructLayout(LayoutKind.Sequential)]
        public struct WNDCLASSEX
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

        public delegate nint WndProc(nint hWnd, uint msg, nint wParam, nint lParam);

        [DllImport(Lib.User32, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateWindowEx(
            int exStyle,
            [MarshalAs(UnmanagedType.LPTStr)] string className,
            [MarshalAs(UnmanagedType.LPTStr)] string windowName,
            int style,
            int x,
            int y,
            int width,
            int height,
            nint hWndParent,
            nint hMenu,
            nint hInstance,
            nint pvParam);

        [DllImport(Lib.User32, EntryPoint = "DefWindowProc")]
        private static extern nint _DefWindowProc(nint hWnd, uint uMsg, nint wParam, nint lParam);

        [DllImport(Lib.User32)]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport(Lib.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        // Defining DefWindowProc like so allows us to pass it as an argument and call it.
        public static readonly WndProc DefWindowProc = _DefWindowProc;

        [DllImport(Lib.User32, CharSet = CharSet.Auto)]
        public static extern bool DestroyWindow(nint hwnd);

        [DllImport(Lib.User32)]
        public static extern nint DispatchMessage([In] ref MSG msg);

        [DllImport(Lib.User32, SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool GetClientRect(nint hWnd, out RECT lpRect);

        [DllImport(Lib.User32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool GetMessage(out MSG lpMsg, nint hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [DllImport(Lib.User32)]
        public static extern IntPtr LoadCursor(nint hInstance, int lpCursorName);

        [DllImport(Lib.User32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern IntPtr LoadIcon(nint hInstance, int lpIconName);

        [DllImport(Lib.User32)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PeekMessage(out MSG msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        [DllImport(Lib.User32)]
        public static extern void PostQuitMessage(int nExitCode);

        [DllImport(Lib.User32)]
        [return: MarshalAs(UnmanagedType.U2)]
        public static extern ushort RegisterClassEx([In] ref WNDCLASSEX lpwcx);

        [DllImport(Lib.User32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport(Lib.User32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool SetWindowPos(IntPtr hWnd, WndInsertAfter hWndInsertAfter, int X, int Y, int cx, int cy, PositionFlag flags);

        [DllImport(Lib.User32)]
        public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [DllImport(Lib.User32)]
        public static extern bool TranslateMessage([In] ref MSG msg);

        [DllImport(Lib.User32, SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool UpdateWindow(IntPtr hWnd);
    }
}
