using System;

namespace Context
{
    public class PixelContext : Core.IContext
    {
        public string Name { get; set; }
        public int Major { get; set; }
        public int Minor { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public nint NativeDisplay { get; set; }
        public nint NativeWindow { get; set; }
        public nint Display { get; set; }
        public nint Config { get; set; }
        public nint Context { get; set; }
        public nint Surface { get; set; }
    }
}
