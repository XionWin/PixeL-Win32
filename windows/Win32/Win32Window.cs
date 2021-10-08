using System;
using System.Runtime.InteropServices;
using Win32.FFI.User32;
using Win32.FFI.User32.Definition;

namespace Win32
{
    public class Win32Window : IDisposable
    {
        public Win32Window(string title, int width, int height)
        {
            this.unmanagedReference = GCHandle.Alloc(this);
            Register(Guid.NewGuid().ToString());
            Create(title, width, height);
        }

        private string className;
        private nint hInstance;
        private string title;
        private nint hWnd;

        private bool isShow;
        private GCHandle unmanagedReference;
        private bool isDisposed;

        public nint Handle => this.hWnd;

        private void Register(string className)
        { 
            this.className = className;
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
                hbrBackground = (IntPtr)(Constants.COLOR_WINDOW + 1),
                lpszMenuName = null,
                lpszClassName = className,
                hIconSm = Native.LoadIcon(IntPtr.Zero, LoadIconA.IDI_APPLICATION)
            };
            
            var windowClass = Native.RegisterClassEx(ref wc);
            if (windowClass == 0)
                throw new InvalidOperationException($"RegisterClassEx failed.");
        }
        
        private void Create(
            string title, 
            int width = Constants.CW_USEDEFAULT, 
            int height = Constants.CW_USEDEFAULT
        )
        {
            this.title = title;
            this.hWnd = Native.CreateWindowEx(
                ExtendedWindowStyles.WS_EX_APPWINDOW | ExtendedWindowStyles.WS_EX_WINDOWEDGE,
                className,
                this.title,
                WindowStyles.WS_MINIMIZEBOX | WindowStyles.WS_MAXIMIZEBOX | WindowStyles.WS_OVERLAPPEDWINDOW | WindowStyles.WS_SYSMENU | WindowStyles.WS_OVERLAPPED | WindowStyles.WS_CAPTION,
                Constants.CW_USEDEFAULT,
                Constants.CW_USEDEFAULT,
                width,
                height,
                IntPtr.Zero,
                IntPtr.Zero,
                hInstance,
                IntPtr.Zero);
            if (this.hWnd is 0)
                throw new InvalidOperationException($"CreateWindowEx failed.");
        }

        public void SetLocation(int x, int y) =>
            Native.SetWindowPos(this.hWnd, WndInsertAfter.HWND_TOP, x, y, 0, 0, SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_SHOWWINDOW);
        
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

        public bool IsShow
        {
            get => this.isShow;
            set 
            {
                this.isShow = Native.ShowWindow(this.hWnd, value ? ShowWindowFlags.SW_SHOWNORMAL : ShowWindowFlags.SW_HIDE);
            }
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

        public void Dispose() => Dispose(true);
    }
}