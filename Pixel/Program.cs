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
            System.Threading.Tasks.Task.Run(() => {
                var counter = 0;
               while (true)
               {
                    window.Title = $"Hello world {counter}";
                    counter++;
                    System.Threading.Thread.Sleep(1000);
               } 
            });
            window.Show();
        }
    }
}


