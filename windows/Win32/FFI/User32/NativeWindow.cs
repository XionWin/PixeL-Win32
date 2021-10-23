using System;
using System.Runtime.InteropServices;
using Win32.FFI.User32.Definition;

namespace Win32.FFI.User32
{
    public abstract class NativeWindow : IDisposable
    {
        private GCHandle unmanagedReference;
        private string className;
        private nint hInstance;
        private string title;
        private int width;
        private int height;
        private nint hWnd;
        private ExtendedWindowStyles extendedWindowStyles = ExtendedWindowStyles.WS_EX_APPWINDOW | ExtendedWindowStyles.WS_EX_WINDOWEDGE;
        private  WindowStyles windowStyles = BASIC_STYLES; //WindowStyles.WS_MINIMIZEBOX | WindowStyles.WS_MAXIMIZEBOX | WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_SYSMENU | WindowStyles.WS_OVERLAPPED | WindowStyles.WS_CAPTION;
        private bool isShow;
        private bool isDisposed;
        public unsafe NativeWindow(string title, int width, int height)
        {   
            this.title = title;
            this.width = width;
            this.height = height;

            this.unmanagedReference = GCHandle.Alloc(this);
            this.className = Guid.NewGuid().ToString();
            this.hInstance = FFI.Kernel32.GetModuleHandle(null);
            var wc = new WNDCLASSEX
            {
                cbSize = (uint)Marshal.SizeOf(typeof(WNDCLASSEX)),
                style = 0,
                lpfnWndProc = this.WndProc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = this.hInstance,
                hIcon = Native.LoadIcon(IntPtr.Zero, LoadIconA.IDI_APPLICATION),
                hCursor = Native.LoadCursor(IntPtr.Zero, LoadCursorA.IDC_ARROW),
                hbrBackground = (nint)SysColorA.COLOR_WINDOW + 1,
                lpszMenuName = null,
                lpszClassName = className,
                hIconSm = Native.LoadIcon(IntPtr.Zero, LoadIconA.IDI_APPLICATION)
            };

            if (Native.RegisterClassEx(ref wc) == 0)
                throw new InvalidOperationException($"RegisterClassEx failed.");

            this.hWnd = Native.CreateWindowEx(
                this.extendedWindowStyles,
                this.className,
                this.title,
                this.windowStyles,
                Constants.CW_USEDEFAULT,
                Constants.CW_USEDEFAULT,
                this.width,
                this.height,
                0,
                0,
                hInstance,
                0);

            if (this.hWnd == IntPtr.Zero)
                throw new InvalidOperationException($"CreateWindowEx failed.");
        }
        public nint Handle => this.hWnd;
        public bool IsShow
        {
            get => this.isShow;
            set 
            {
                this.isShow = Native.ShowWindow(this.hWnd, value ? ShowWindowFlags.SW_SHOWNORMAL : ShowWindowFlags.SW_HIDE);
            }
        }
        public string Title
        {
            get => this.title;
            set => this.title = Native.SetWindowText(this.hWnd,  title) ? value :  throw new InvalidOperationException($"SetWindowTitle failed.");
        }

        public virtual void Show()
        {
            while (Native.GetMessage(out var message, default, 0, 0))
            {
                Native.TranslateMessage(ref message);
                Native.DispatchMessage(ref message);
            }
            if (unmanagedReference.IsAllocated) { unmanagedReference.Free(); }
        }

        public void SetLocation(int x, int y) =>
            Native.SetWindowPos(this.hWnd, WndInsertAfter.HWND_TOP, x, y, 0, 0, SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_SHOWWINDOW);
        
        public WindowStyles GetWindowStyle()
        {
            return Native.GetWindowStyle(this.hWnd);
        }

        private const WindowStyles BASIC_STYLES = WindowStyles.WS_SYSMENU | WindowStyles.WS_CAPTION | WindowStyles.WS_CLIPSIBLINGS | WindowStyles.WS_VISIBLE;
        public void SetWindowStyle(WindowStyles windowStyles)
        {
            if (Native.SetWindowStyle(this.hWnd,  BASIC_STYLES | windowStyles) is false)
                throw new InvalidOperationException($"SetWindowStyle failed.");
        }
        
        public void SetUserData(object userData)
        {
            Native.SetUserData(this.hWnd, userData);
        }
        public T GetUserData<T>()
        {
            return (T)Native.GetUserData(this.hWnd);
        }
                

        protected virtual nint WndProc(nint hWnd, WndMessage msg, nint w, nint l)
        {
            switch (msg)
            {
                case WndMessage.WM_CLOSE:
                    Native.DestroyWindow(hWnd);
                    break;

                case WndMessage.WM_DESTROY:
                    Native.PostQuitMessage(0);
                    break;
                default:
                    return Native.DefWindowProc(hWnd, msg, w, l);
            }
            return default;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed is false)
            {
                if (disposing)
                {
                    if (unmanagedReference.IsAllocated) { unmanagedReference.Free(); }
                }

                isDisposed = true;
            }
        }

        public void Dispose() => Dispose(true);
    }
}