using Win32.FFI.User32.Definition;

namespace Win32
{
    public class Window: FFI.User32.NativeWindow
    {
        public Window(string title, int width, int height)
        :base(title, width, height)
        {
            
        }

    }
}