using System;
using Pixel.Core;

namespace Pixel.Windows
{
    public class PixelContext : IContext
    {
        public PixelContext(IWindow window) => this.NativeWindow = window.Handle;

        public int Major { get; set; }
        public int Minor { get; set; }
        public nint NativeDisplay { get; set; }
        public nint NativeWindow { get; set; }
        public nint Display { get; set; }
        public nint Config { get; set; }
        public nint Context { get; set; }
        public nint Surface { get; set; }
    }
}
