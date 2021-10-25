using System;
using System.Linq;

namespace EGL
{
    using EGLConfig = IntPtr;
    using EGLContext = IntPtr;
    using EGLDisplay = IntPtr;
    using EGLSurface = IntPtr;

    public delegate void PageFilpHandler(int fd, uint frame, uint sec, uint usec, ref int data);
    unsafe public class Context : IDisposable
    {
        // public DRM.Drm Drm { get; private set; }
        // public GBM.Gbm Gbm { get; private set; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public EGLDisplay EglDisplay { get; private set; }
        public EGLContext EglContext { get; private set; }
        public EGLSurface EglSurface { get; private set; }
        public EGLConfig EGLConfig { get; private set; }
        public int Major { get; private set; }
        public int Minor { get; private set; }

        public bool VerticalSynchronization { get; set; }
        
        public RenderableSurfaceType RenderableSurfaceType { get; init; }

        #region ctor
        public Context(int fd, RenderableSurfaceType surfaceType)
        {
            var (display, context, surface, config, major, minor) = ContextExtension.CrateContext(surfaceType);
            this.EglDisplay = display;
            this.EglContext = context;
            this.EglSurface = surface;
            this.EGLConfig = config;
            this.Major = major;
            this.Minor = minor;
        }
        #endregion

        public Context Initialize(Action initFunc)
        {
            return this;
        }
        nint page_flip_handler = System.Runtime.InteropServices.Marshal.GetFunctionPointerForDelegate(new PageFilpHandler(
            (int fd, uint frame, uint sec, uint usec, ref int data) =>
            {
                data = 0;
            }
        ));


        const int DRM_CONTEXT_VERSION = 2;
        public void Render(Action renderFunc)
        {
            
        }


        #region IDisposable implementation
        ~Context()
        {
            Dispose(false);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (this.EglContext != IntPtr.Zero)
                    Egl.eglDestroyContext(this.EglDisplay, this.EglContext);
                if (this.EglDisplay != IntPtr.Zero)
                    Egl.eglTerminate(this.EglDisplay);
            }
            catch (Exception ex)
            {
                Console.WriteLine("error disposing egl context: {0}", ex.ToString());
            }
            finally
            {
                this.EglContext = IntPtr.Zero;
                this.EglDisplay = IntPtr.Zero;
            }
        }
        #endregion
    }
}

