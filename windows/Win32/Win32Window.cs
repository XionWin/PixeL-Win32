using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Win32
{
    public class Win32Window : IDisposable
    {
        // [UnmanagedCallersOnly(CallConvs = new Type[] { typeof(CallConvStdcall) })]
        // private static unsafe nint UnmanagedProcedure(nint window_handle, uint message, nuint w, nint l)
        // {
        //     void* userData = null;

        //     if (message is FFI.Lib.WM_CREATE)
        //     {
        //         var createStruct = (CREATESTRUCTW*)l;
        //         userData = createStruct->lpCreateParams;
        //         _ = Edited.User32.SetWindowLongPtr(window_handle, Edited.Constants.GWLP_USERDATA, userData);
        //     }
        //     else { userData = Edited.User32.GetWindowLongPtr(window_handle, Edited.Constants.GWLP_USERDATA); }

        //     if (userData is not null)
        //     {
        //         var unmanagedReference = GCHandle.FromIntPtr((IntPtr)userData);
        //         var win32Window = unmanagedReference.Target as Win32Window;
        //         return win32Window.Procedure(window_handle, message, w, l);
        //     }

        //     return User32.DefWindowProc((HWND)window_handle, message, w, l).Value;
        // }

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
            var wc = new FFI.User32.WNDCLASSEX
            {
                cbSize = (uint)Marshal.SizeOf(typeof(FFI.User32.WNDCLASSEX)),
                style = 0,
                lpfnWndProc = this.WndProc,
                cbClsExtra = 0,
                cbWndExtra = 0,
                hInstance = this.hInstance,
                hIcon = FFI.User32.LoadIcon(IntPtr.Zero, FFI.User32.IDI_APPLICATION),
                hCursor = FFI.User32.LoadCursor(IntPtr.Zero, FFI.User32.IDC_ARROW),
                hbrBackground = (IntPtr)(FFI.User32.COLOR_WINDOW + 1),
                lpszMenuName = null,
                lpszClassName = className,
                hIconSm = FFI.User32.LoadIcon(IntPtr.Zero, FFI.User32.IDI_APPLICATION)
            };
            
            var windowClass = FFI.User32.RegisterClassEx(ref wc);
            if (windowClass == 0)
                throw new InvalidOperationException($"RegisterClassEx failed.");
        }
        
        private void Create(
            string title, 
            int width = FFI.User32.CW_USEDEFAULT, 
            int height = FFI.User32.CW_USEDEFAULT
        )
        {
            this.title = title;
            this.hWnd = FFI.User32.CreateWindowEx(
            FFI.User32.WS_EX_APPWINDOW | FFI.User32.WS_EX_WINDOWEDGE,
            this.className,
            title,
            FFI.User32.WS_MINIMIZEBOX | FFI.User32.WS_MAXIMIZEBOX | FFI.User32.WS_OVERLAPPEDWINDOW | FFI.User32.WS_SYSMENU | FFI.User32.WS_OVERLAPPED | FFI.User32.WS_CAPTION,
            FFI.User32.CW_USEDEFAULT,
            FFI.User32.CW_USEDEFAULT,
            width,
            height,
            IntPtr.Zero,
            IntPtr.Zero,
            hInstance,
            IntPtr.Zero);
        }

        public void SetLocation(int x, int y) =>
            FFI.User32.SetWindowPos(this.hWnd, FFI.WndInsertAfter.HWND_TOP, x, y, 0, 0, FFI.PositionFlag.SWP_NOZORDER | FFI.PositionFlag.SWP_NOSIZE | FFI.PositionFlag.SWP_SHOWWINDOW);
        
        protected virtual nint WndProc(IntPtr hWnd, uint msg, nint w, nint l)
        {
            switch (msg)
            {
                case FFI.User32.WM_CLOSE:
                    FFI.User32.DestroyWindow(hWnd);
                    break;

                case FFI.User32.WM_DESTROY:
                    FFI.User32.PostQuitMessage(0);
                    break;
                default:
                    return FFI.User32.DefWindowProc(hWnd, msg, w, l);
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
                this.isShow = FFI.User32.ShowWindow(this.hWnd, value ? FFI.User32.SW_SHOWNORMAL : FFI.User32.SW_HIDE);
            }
        }

        public virtual void Show()
        {
            while (FFI.User32.GetMessage(out var message, default, 0, 0))
            {
                FFI.User32.TranslateMessage(ref message);
                FFI.User32.DispatchMessage(ref message);
            }
            if (unmanagedReference.IsAllocated) { unmanagedReference.Free(); }
        }

        public void Dispose() => Dispose(true);
    }
}