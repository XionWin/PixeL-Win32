using System;

namespace Pixel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var window = new Win32.Win32Window("Hello", 800, 600);
            window.SetLocation(0, 0);
            window.Show();
        }
    }

}


