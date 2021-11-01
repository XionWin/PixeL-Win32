using System;
using System.Collections.Generic;

namespace OpenGLES.GFX
{
    public abstract class GfxObject: IDisposable
    {
        public uint Id { get; init; }

        protected abstract void Release();

        public void Dispose() => this.Release();
    }

    static class GfxObjectExtension {
        public static T Check<T>(this T obj)
        where T: GfxObject
        {
            if(GL.glGetError() is var errorCode && errorCode != 0) throw new OpenGLESException($"Check GfxObject Error: {errorCode}");
            return obj;
        }
    }
}
