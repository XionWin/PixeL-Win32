namespace Win32.FFI.User32.Definition
{
    public class Constants
    {
        public const int CW_USEDEFAULT = unchecked((int)0x80000000);

        public const int COLOR_WINDOW = 5;

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
    }
}