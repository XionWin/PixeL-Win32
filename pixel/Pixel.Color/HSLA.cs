using System;
using Pixel.Core;

namespace Pixel.Color
{
    public class HSLA: IColor
    {
        public HSLA(float h, float s, float l, byte a = 255)
        {
            this.H = h;
            this.S = s;
            this.L = l;
            this.A = a;
        }
        
        public float H { get; set; }
        public float S { get; set; }
        public float L { get; set; }
        public byte A { get; set; }

        public (byte r, byte g, byte b) ToRGB() => this.HSLAToRGBA() is RGBA rgba ? rgba.ToRGB(): throw new Exception("ToRGB error");
        public (byte r, byte g, byte b, byte a) ToRGBA() => this.HSLAToRGBA() is RGBA rgba ? rgba.ToRGBA(): throw new Exception("ToRGBA error");
        public (float r, float g, float b) ToRGBF() => this.HSLAToRGBA() is RGBA rgba ? rgba.ToRGBF(): throw new Exception("ToRGBF error");
        public (float r, float g, float b, float a) ToRGBAF() => this.HSLAToRGBA() is RGBA rgba ? rgba.ToRGBAF(): throw new Exception("ToRGBAF error");

    }
}
