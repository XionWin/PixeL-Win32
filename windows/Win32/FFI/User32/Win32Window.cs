using System;
using System.Runtime.InteropServices;
using Win32.FFI.User32.Definition;

namespace Win32.FFI.User32
{
    public abstract class Win32Window : IDisposable
    {
        private GCHandle unmanagedReference;
        private string className;
        private WindowClassEx windowClassEx;
        private nint hInstance;
        private string title;
        private nint hWnd;
        private bool isDisposed;


        public unsafe Win32Window(int width, int height, string title)
        {   
            this.Width = width;
            this.Height = height;
            this.title = title;

            this.unmanagedReference = GCHandle.Alloc(this);
            this.className = Guid.NewGuid().ToString();
            this.hInstance = FFI.Kernel32.GetModuleHandle(null);

            this.windowClassEx = new WindowClassEx
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

            if (Native.RegisterClassEx(ref this.windowClassEx) is false)
                throw new InvalidOperationException($"RegisterClassEx failed.");

            this.hWnd = Native.CreateWindow(
                this.className,
                this.title,
                WindowStyles.WS_OVERLAPPED | WindowStyles.WS_CAPTION | WindowStyles.WS_SYSMENU,
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
            Native.ShowWindow(this.hWnd, ShowWindowFlags.SW_SHOWNORMAL);
            Native.UpdateWindow(this.hWnd);

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

        // public void SetWindowStyle(WindowStyles windowStyles)
        // {
        //     if (Native.SetWindowStyle(this.hWnd,  BASIC_STYLES | windowStyles) is false)
        //         throw new InvalidOperationException($"SetWindowStyle failed.");
        // }
        
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