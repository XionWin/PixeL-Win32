using System;
using Pixel.Core;

namespace Pixel.Color
{
    public class RGBA: IColor
    {
        public RGBA(byte r, byte g, byte b, byte a = 255)
        {
            this.R = r;
            this.G = g;
            this.B = b;
            this.A = a;
        }
        
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }

        public (byte r, byte g, byte b) ToRGB() => (this.R, this.G, this.B);
        public (byte r, byte g, byte b, byte a) ToRGBA() => (this.R, this.G, this.B, this.A);
        public (float r, float g, float b) ToRGBF() => (this.R / 255.0f, this.G / 255.0f, this.B / 255.0f);
        public (float r, float g, float b, float a) ToRGBAF() => (this.R / 255.0f, this.G / 255.0f, this.B / 255.0f, this.A / 255.0f);

    }
}
