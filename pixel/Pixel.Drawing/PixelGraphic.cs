using System;
using Pixel.Core;

namespace Pixel.Drawing
{
    public delegate void DrawHandle();
    public class PixelGraphic
    {
        public event DrawHandle OnDraw;
        protected IWindow Window { get; private set; }
        protected IParam Context { get; private set; }
        protected IMethod Param { get; private set; }
        protected PixelAnalystor Analystor { get; private set; }

        public bool ShowAnalysis { get; set; } = true;
        
        public PixelGraphic(IWindow window, IParam context, IMethod param)
        {
            this.Window = window;
            this.Context = context;
            this.Param = param;

            this.Analystor = new PixelAnalystor();

            this.Param.RenderCreate(this.Context);

            window.OnPaint += () => {
                this.Param.RenderClear();
                this.OnDraw?.Invoke();
                this.Param.RenderSwapBuffers(this.Context.Display, this.Context.Surface);
                
                if (this.ShowAnalysis)
                {
                    this.Analystor.Tick();
                    this.Analystor.ShowResult();
                }
            };
        }

        public void ClearColor(IColor color) => this.Param.RenderClearColor(color);

        public void Show()
        {
            this.Window.Show();
        }
    }
}
