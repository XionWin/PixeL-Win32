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
        private nint hWnd;
        private  WindowStyles windowStyles = WindowStyles.WS_OVERLAPPEDWINDOW; // | WindowStyles.WS_MINIMIZEBOX | WindowStyles.WS_MAXIMIZEBOX | WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_SYSMENU | WindowStyles.WS_OVERLAPPED | WindowStyles.WS_CAPTION;
        private bool isDisposed;
        public unsafe NativeWindow(string title, int width, int height)
        {   
            this.title = title;
            this.Width = width;
            this.Height = height;

            this.unmanagedReference = GCHandle.Alloc(this);
            this.className = Guid.NewGuid().ToString();
            this.hInstance = FFI.Kernel32.GetModuleHandle(null);
            var wc = new WindowClassEx
            {
                Size = (uint)Marshal.SizeOf(typeof(WindowClassEx)),
                ClassName = this.className,
                CursorHandle = Native.LoadCursor(IntPtr.Zero, LoadCursorA.IDC_ARROW),
                IconHandle = Native.LoadIcon(IntPtr.Zero, LoadIconA.IDI_APPLICATION),
                Styles = WindowClassStyles.CS_HREDRAW | WindowClassStyles.CS_VREDRAW,
                BackgroundBrushHandle = new IntPtr((int) StockObject.WHITE_BRUSH),
                WndProc = this.WndProc,
                InstanceHandle = this.hInstance
            };

            if (Native.RegisterClassEx(ref wc) == 0)
                throw new InvalidOperationException($"RegisterClassEx failed.");

            this.hWnd = Native.CreateWindow(
                this.className,
                this.title,
                this.windowStyles,
                Constants.CW_USEDEFAULT,
                Constants.CW_USEDEFAULT,
                this.Width,
                this.Height,
                0,
                0,
                hInstance,
                0);

            if (this.hWnd == IntPtr.Zero)
                throw new InvalidOperationException($"CreateWindowEx failed.");
                
            Native.ShowWindow(this.hWnd, ShowWindowFlags.SW_SHOWNORMAL);
            Native.UpdateWindow(this.hWnd);
        }
        public nint Handle => this.hWnd;

        public int Width { get; set; }
        public int Height { get; set; }

        public string Title
        {
            get => this.title;
            set => this.title = Native.SetWindowText(this.hWnd,  title) ? value :  throw new InvalidOperationException($"SetWindowTitle failed.");
        }

        public virtual void Show()
        {
            while (Native.GetMessage(out var message, IntPtr.Zero, 0, 0))
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
            // switch (msg)
            // {
            //     case WndMessage.WM_CLOSE:
            //         Native.DestroyWindow(hWnd);
            //         return 0;
            //     case WndMessage.WM_DESTROY:
            //         Native.PostQuitMessage(0);
            //         return 0;
            //     default:
            //         return Native.DefWindowProc(hWnd, msg, w, l);
            // }


            switch (msg)
            {
                case WndMessage.WM_ERASEBKGND:
                    return 1;
                case WndMessage.WM_CLOSE:
                {
                    Native.PostQuitMessage(0);
                    break;
                }
            }
            return Native.DefWindowProc(hWnd, msg, w, l);
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