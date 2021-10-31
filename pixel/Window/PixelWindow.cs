using System;
using Core;

namespace Window
{
    public class PixelWindow: Win32.NativeWindow, Core.IWindow
    {
        public event PaintHandle OnPaint;

        public IContext Context {get; private set; }

        public PixelWindow(IContext context): base(context.Width, context.Height, context.Name)
        {
            this.Context = context;
            this.Context.NativeWindow = this.Handle;
        }

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
