using System;

namespace Pixel.Core
{
    public class PixelAnalystor
    {
        ulong fpsCounter = 0;
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
        public void Tick()
        {
            if(watch.IsRunning is false)
                watch.Start();
            fpsCounter++;
        }
        public void ShowResult()
        {
            if (watch.ElapsedMilliseconds >= 500)
            {
                Console.WriteLine($"fps: {(double)fpsCounter / watch.ElapsedMilliseconds * 1000:#.00}");
                fpsCounter = 0;
                watch.Restart();
            }
        }

    }
}
