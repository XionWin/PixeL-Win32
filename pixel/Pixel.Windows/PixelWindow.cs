using System;
using Pixel.Core;

namespace Pixel.Windows
{
    public class PixelWindow: Win32.NativeWindow, IWindow
    {
        public event WindowPaintHandle OnPaint;

        public PixelWindow(int width, int height, string title): base(width, height, title) {}
        
        protected override void Paint()
        {
            this.OnPaint?.Invoke();
            base.Paint();
        }

        IWindow IWindow.Show()
        {
            this.Show();
            return this;
        }
    }
}
