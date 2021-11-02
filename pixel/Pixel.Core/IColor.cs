using System;

namespace Pixel.Core
{
    public interface IColor
    {
        (byte r, byte g, byte b) ToRGB();
        (byte r, byte g, byte b, byte a) ToRGBA();
        (float r, float g, float b) ToRGBF();
        (float r, float g, float b, float a) ToRGBAF();
    }
}
