using System.Runtime.InteropServices;

namespace Pixel.Windows
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public float x { get; set; }
        public float y { get; set; }
        public float u { get; set; }
        public float v { get; set; }
        public float a { get; set; }
    }
}