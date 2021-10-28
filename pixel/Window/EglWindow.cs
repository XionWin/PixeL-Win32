using System;
using EGL;
using EGL.Definitions;
using Win32.FFI.User32.Definition;

namespace Window
{
    public class EglWindow: Win32.Window
    {
        private ESContext context;
        public EglWindow(): base("OpenGL ES 3.0", 320, 320)
        {
            this.SetLocation(100, 100);

            initialize();
        }

        protected virtual void initialize()
        {
            this.context.EglNativeWindow = this.Handle;
            this.context.EglDisplay = Egl.eglGetDisplay(this.context.EglNativeDisplay);
            
            if (this.context.EglDisplay == IntPtr.Zero)
                throw new NotSupportedException("[EGL] GetDisplay failed.: " + Egl.eglGetError());

            if (!Egl.eglInitialize(this.context.EglDisplay, out int major, out int minor))
                throw new NotSupportedException("[EGL] Failed to initialize EGL display. Error code: " + Egl.eglGetError());
            this.context.Major = major;
            this.context.Minor = minor;
            
            var isModifiersSupported = ContextExtension.GetExtensions(this.context.EglDisplay).Contains("EGL_EXT_image_dma_buf_import_modifiers");

            if (!Egl.eglBindAPI(EGL.Definitions.RenderApi.GLES))
                throw new NotSupportedException("[EGL] Failed to bind EGL Api: " + Egl.eglGetError());
                
            var desiredConfig = new[] {
                Definition.SURFACE_TYPE, (int)RenderableSurfaceType.Window,
                Definition.RENDERABLE_TYPE, Definition.OPENGL_ES3_BIT,
                Definition.RED_SIZE, 8,
                Definition.GREEN_SIZE, 8,
                Definition.BLUE_SIZE, 8,
                Definition.ALPHA_SIZE, 8,
                Definition.STENCIL_SIZE, 8,
                Definition.SAMPLE_BUFFERS, 0,
                Definition.SAMPLES, 0,
                Definition.NONE
            };
            
            this.context.EglConfig = ContextExtension.GetConfig(this.context.EglDisplay, desiredConfig);
            var contextAttrib = new[] {
                Definition.CONTEXT_CLIENT_VERSION, 2,
                Definition.NONE
            };
            this.context.EglContext = Egl.CreateContext(this.context.EglDisplay, this.context.EglConfig, IntPtr.Zero, contextAttrib);
            if (this.context.EglContext == IntPtr.Zero)
                throw new NotSupportedException(String.Format("[EGL] Failed to create egl context, error {0}.", Egl.eglGetError()));

                
            this.context.EglSurface = Egl.eglCreateWindowSurface(this.context.EglDisplay, this.context.EglConfig, this.context.EglNativeWindow, null);

            if (this.context.EglSurface == IntPtr.Zero)
                throw new NotSupportedException(String.Format("[EGL] Failed to create egl surface, error {0}.", Egl.eglGetError()));

                
            if (!Egl.eglMakeCurrent(this.context.EglDisplay, this.context.EglSurface, this.context.EglSurface, this.context.EglContext))
                throw new NotSupportedException(String.Format("[EGL] Failed to make current, error {0}.", Egl.eglGetError()));


            OpenGLES.GL.glViewport(0, 0, this.Width, this.Height);
        }
        
        protected override nint WndProc(nint hWnd, WndMessage msg, nint w, nint l)
        {
            switch (msg)
            {
                case WndMessage.WM_PAINT:
                    return Render();
                default:
                    return base.WndProc(hWnd, msg, w, l);
            }
        }

        public virtual nint Render()
        {
            OpenGLES.GL.glClear(OpenGLES.Def.ClearBufferMask.ColorBufferBit);
            if (DateTime.Now.Second % 2 == 0)
            {
                OpenGLES.GL.glClearColor(0.0f, 1.0f, 0.0f, 1.0f);
            }
            else
            {
                OpenGLES.GL.glClearColor(1.0f, 0.0f, 0.0f, 1.0f);
            }

            Egl.eglSwapBuffers ( this.context.EglDisplay, this.context.EglSurface );
            return 0;
        }
    }
}
