using Win32.FFI.User32.Definition;

namespace Win32
{
    public class NativeWindow: FFI.User32.Win32Window
    {
        public NativeWindow(int width, int height, string title)
        :base(width, height, title)
        {
            
        }

        protected override nint WndProc(nint hWnd, WndMessage msg, nint w, nint l)
        {
            switch (msg)
            {
                case WndMessage.WM_PAINT:
                    this.Paint();
                    return System.IntPtr.Zero;
            }
            return base.WndProc(hWnd, msg, w, l);
        }

        protected virtual void Paint()
        {
            
        }
    }
}