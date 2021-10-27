using System;

namespace Window
{
    using EGLNativeDisplayType = IntPtr;
    using EGLNativeWindowType = IntPtr;
    // using EGLNativePixmapType = IntPtr;
    using EGLConfig = IntPtr;
    using EGLContext = IntPtr;
    using EGLDisplay = IntPtr;
    using EGLSurface = IntPtr;
    public struct ESContext
    {
        public int Major { get; set; }
        public int Minor { get; set; }
        public object PlatformData { get; set; }
        public object UserData { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public EGLNativeDisplayType EglNativeDisplay { get; set; }
        public EGLNativeWindowType EglNativeWindow { get; set; }
        public EGLDisplay  EglDisplay { get; set; }
        public EGLConfig  EglConfig { get; set; }
        public EGLContext  EglContext { get; set; }
        public EGLSurface  EglSurface { get; set; }

    }
}