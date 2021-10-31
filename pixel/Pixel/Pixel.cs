using System;

namespace Pixel
{
    public delegate void DrawHandle();
    public class Pixel
    {
        public event DrawHandle OnDraw;
        protected Core.IContext Context { get; private set; }
        protected Core.IRenderer Renderer { get; private set; }
        protected Core.IWindow Window { get; private set; }
        
        public Pixel(int width, int height, string name)
        {
            this.Context = new Context.PixelContext();
            this.Context.Width = width;
            this.Context.Height = height;
            this.Context.Name = name;

            this.Renderer = new Renderer.PixelRenderer(this.Context);
            this.Window = new Window.PixelWindow(this.Context);

            this.Renderer.Initialize();

            this.Window.OnPaint += () => {
                this.Renderer.Clear();
                this.OnDraw?.Invoke();
                this.Renderer.SwapBuffers();
            };
        }

        public void Show()
        {
            this.Window.Show();
        }
    }
}
