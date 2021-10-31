using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var pixel = new Pixel.Pixel(1024, 640, "OpenGL ES 3.0");
            
            ulong counter = 0;
            bool flag = true;
            System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            pixel.OnDraw += () => {
                if (flag)
                {
                    OpenGLES.GL.glClearColor(0.05f, 0.05f, 0.05f, 1.0f);
                }
                else
                {
                    OpenGLES.GL.glClearColor(0.0f, 0.0f, 0.0f, 1.0f);
                }
                flag = !flag;
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
