using System;

namespace Color
{
    public class HSLA: Core.IColor
    {
        public HSLA()
        {
        }

        public HSLA(double h, double s, double l, byte a = 255): this()
        {
            this.H = h;
            this.S = s;
            this.L = l;
            this.A = a;
        }
        
        public double H { get; set; }
        public double S { get; set; }
        public double L { get; set; }
        public byte A { get; set; }

        public (byte r, byte g, byte b) ToRGB() => this.HSLToRGB() is RGBA rgba ? (rgba.R, rgba.G, rgba.B): throw new Exception("ToRGB error");
        public (byte r, byte g, byte b, byte a) ToRGBA() => this.HSLToRGB() is RGBA rgba ? (rgba.R, rgba.G, rgba.B, rgba.A): throw new Exception("ToRGBA error");
        public (float r, float g, float b) ToRGBF() => this.HSLToRGB() is RGBA rgba ? (rgba.R / 255.0f, rgba.G / 255.0f, rgba.B / 255.0f): throw new Exception("ToRGBF error");
        public (float r, float g, float b, float a) ToRGBAF() => this.HSLToRGB() is RGBA rgba ? (rgba.R / 255.0f, rgba.G / 255.0f, rgba.B / 255.0f, rgba.A / 255.0f): throw new Exception("ToRGBAF error");

    }
}
