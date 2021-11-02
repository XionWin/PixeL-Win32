using System;
using Pixel.Core;

namespace Pixel
{
    public delegate void DrawHandle();
    public class PixelGraphic
    {
        public event DrawHandle OnDraw;
        protected IWindow Window { get; private set; }
        protected IContext Context { get; private set; }
        protected IParam Param { get; private set; }
        protected PixelAnalystor Analystor { get; private set; }

        public bool ShowAnalysis { get; set; }
        
        public PixelGraphic(IWindow window, IContext context, IParam param)
        {
            this.Window = window;
            this.Context = context;
            this.Param = param;

            this.Analystor = new PixelAnalystor();

            this.Param.RenderCreateHandler(this.Context);

            window.OnPaint += () => {
                this.Param.RenderClearHandler();
                this.OnDraw?.Invoke();
                this.Param.RenderSwapBuffersHandler(this.Context.Display, this.Context.Surface);
                
                if (this.ShowAnalysis)
                {
                    this.Analystor.Tick();
                    this.Analystor.ShowResult();
                }
            };
        }

        public void ClearColor(IColor color) => this.Param.RenderClearColorHandler(color);

        public void Show()
        {
            this.Window.Show();
        }
    }
}
