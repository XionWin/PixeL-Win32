using System;

namespace Pixel.Core
{
    public interface IMethod
    {
        Action<IParam> RenderCreate { get; } 
        Action RenderClear { get; } 
        Action<IColor> RenderClearColor { get; } 
        Action<nint, nint> RenderSwapBuffers { get; } 
        Action<nint, nint> RenderDelete { get; } 
    }
}