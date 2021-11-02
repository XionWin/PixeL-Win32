using System;

namespace Pixel.Core
{
    public interface IParam
    {
        Action<IContext> RenderCreateHandler { get; } 
        Action RenderClearHandler { get; } 
        Action<IColor> RenderClearColorHandler { get; } 
        Action<nint, nint> RenderSwapBuffersHandler { get; } 
        Action<nint, nint> RenderDeleteHandler { get; } 

        Action WindowCreateHandler { get; } 
        Action WindowShowHandler { get; } 
        event Action WindowOnPaint;
        Action WindowDeleteHandler { get; } 
    }
}