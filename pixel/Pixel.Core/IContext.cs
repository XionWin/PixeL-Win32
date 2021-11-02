using System;

namespace Pixel.Core
{
    public interface IContext
    {
        int Major { get; set; }
        int Minor { get; set; }
        nint NativeDisplay { get; set; }
        nint NativeWindow { get; set; }
        nint Display { get; set; }
        nint Config { get; set; }
        nint Context { get; set; }
        nint Surface { get; set; }
    }
}
