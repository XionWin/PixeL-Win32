using System.Runtime.InteropServices;

namespace Renderer.Definitions
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex
    {
        public float x { get; set; }
        public float y { get; set; }
        public float r { get; set; }
        public float g { get; set; }
        public float b { get; set; }
        public float a { get; set; }
    }
}