using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var pixel = new Pixel.Pixel(1024, 640, "OpenGL ES 3.0");
            pixel.ShowAnalysis = true;
            
            var hsl = new Color.HSLA(0, 1, 0.5f);
            var angle = 0.0f;
            pixel.OnDraw += () => {
                pixel.ClearColor(hsl);
                hsl.H = angle += 0.05f;
                hsl.H = angle %= 360;
            };
            pixel.Show();
        }
    }
}
