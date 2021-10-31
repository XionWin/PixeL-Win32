using System;

namespace Core
{
    public interface IRenderer
    {
        IContext Context { get; }

        IRenderer Initialize();
        IRenderer Clear();
        IRenderer ClearColor(IColor color);
        IRenderer SwapBuffers();
    }
}
