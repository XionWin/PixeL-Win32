using System;
using Win32.FFI.User32.Definition;

namespace Window
{
    public class EglWindow: Win32.Window
    {
        public EglWindow(): base("OpenGL ES 3.0", 800, 600)
        {
            this.SetLocation(100, 100);
        }
        
        protected override nint WndProc(nint hWnd, WndMessage msg, nint w, nint l)
        {
            switch (msg)
            {
                case WndMessage.WM_PAINT:
                    return Render();
                default:
                    return base.WndProc(hWnd, msg, w, l);
            }
        }

        public virtual nint Render()
        {
            var r = EGL.Egl.eglGetDisplay(IntPtr.Zero);
            return 0;
        }
    }
}
