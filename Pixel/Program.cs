using System;

namespace Pixel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var window = new Win32.Window("OpenGL ES 3.0", 800, 600);
            window.SetLocation(100, 100);
            
            window.Show();
        }
    }
}


