using System;
using Pixel.Core;

namespace Pixel.Color
{
    public static class Extension
    {
        public static HSLA RGBAToHSLA(this RGBA rgba)
        {
            var(h, s, l, a) = (0f, 0f, 0f, 0f);
            // Convert RGB to a 0.0 to 1.0 range.
            float r = rgba.R / 255.0f;
            float g = rgba.G / 255.0f;
            float b = rgba.B / 255.0f;

            // Get the maximum and minimum RGB components.
            float max = r;
            if (max < g) max = g;
            if (max < b) max = b;

            float min = r;
            if (min > g) min = g;
            if (min > b) min = b;

            float diff = max - min;
            l = (max + min) / 2;
            if (Math.Abs(diff) < 0.00001)
            {
                s = 0;
                h = 0;  // H is really undefined.
            }
            else
            {
                if (l <= 0.5) s = diff / (max + min);
                else s = diff / (2 - max - min);

                float rd = (max - r) / diff;
                float gd = (max - g) / diff;
                float bd = (max - b) / diff;

                if (r == max) h = bd - gd;
                else if (g == max) h = 2 + rd - bd;
                else h = 4 + gd - rd;

                h *= 60;
                if (h < 0) h += 360;
            }
            return new HSLA(h, s, l, rgba.A);
        }

        // Convert an HSL value into an RGB value.
        public static RGBA HSLAToRGBA(this HSLA hsla)
        {
            float p2;
            if (hsla.L <= 0.5) p2 = hsla.L * (1 + hsla.S);
            else p2 = hsla.L + hsla.S - hsla.L * hsla.S;

            float p1 = 2 * hsla.L - p2;
            float r, g, b;
            if (hsla.S == 0)
            {
                r = hsla.L;
                g = hsla.L;
                b = hsla.L;
            }
            else
            {
                r = QqhToRgb(p1, p2, hsla.H + 120);
                g = QqhToRgb(p1, p2, hsla.H);
                b = QqhToRgb(p1, p2, hsla.H - 120);
            }

            // Convert RGB to the 0 to 255 range.
            return new RGBA((byte)(r * 255.0), (byte)(g * 255.0), (byte)(b * 255.0));
        }

        private static float QqhToRgb(float q1, float q2, float hue)
        {
            hue %= 360;
            if (hue < 0) hue += 360;
            // if (hue > 360) hue -= 360;
            // else if (hue < 0) hue += 360;

            if (hue < 60) return q1 + (q2 - q1) * hue / 60;
            if (hue < 180) return q2;
            if (hue < 240) return q1 + (q2 - q1) * (240 - hue) / 60;
            return q1;
        }

    }
}
