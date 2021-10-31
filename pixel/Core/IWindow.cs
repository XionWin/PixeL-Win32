using System;

namespace Core
{
    public delegate void PaintHandle();
    public interface IWindow
    {
        event PaintHandle OnPaint;
        IContext Context {get; }
        IWindow Show();
    }
}
