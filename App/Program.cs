using System;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var window = new Window.EglWindow();
            window.Show();

        }
    }
}
