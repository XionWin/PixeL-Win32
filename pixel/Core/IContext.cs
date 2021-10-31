using System;

namespace Core
{
    public interface IContext
    {
        string Name { get; set; }
        int Major { get; set; }
        int Minor { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        nint NativeDisplay { get; set; }
        nint NativeWindow { get; set; }
        nint  Display { get; set; }
        nint  Config { get; set; }
        nint  Context { get; set; }
        nint  Surface { get; set; }
    }
}
