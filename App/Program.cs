using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var pixel = new Pixel.Pixel(1024, 640, "OpenGL ES 3.0");
            
            var hsl = new Color.HSLA(0, 1, 0.5);
            var angle = 0.0f;
            ulong counter = 0;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            pixel.OnDraw += () => {
                var (r, g, b, a) = hsl.ToRGBAF();
                OpenGLES.GL.glClearColor(r, g, b, a);
                hsl.H = angle += 0.05f;
                hsl.H = angle %= 360;
                counter++;
                if (watch.ElapsedMilliseconds >= 500)
                {
                    Console.WriteLine($"fps: {(double)counter / watch.ElapsedMilliseconds * 1000:#.00}");
                    counter = 0;
                    watch.Restart();
                }
            };
            pixel.Show();
        }
    }
}
