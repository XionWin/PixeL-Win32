using System;

namespace Pixel.Core
{
    public delegate void WindowPaintHandle();
    public interface IWindow
    {
        string Title { get; }
        int Width { get; }
        int Height { get; }
        nint Handle { get; }
        event WindowPaintHandle OnPaint;
        IWindow Show();
    }
}
