using System;
using Core;
using EGL;
using EGL.Definitions;

namespace Renderer
{
    public class PixelRenderer : Core.IRenderer
    {
        public IContext Context { get; private set; }
        public PixelRenderer(IContext context)
        {
            this.Context = context;
        }

        public IRenderer Initialize()
        {
            if (this.Context.NativeWindow == IntPtr.Zero)
                throw new Exception("PixelRenderer initialize error. NativeWindow not found");
            this.Context.Display = Egl.eglGetDisplay(this.Context.NativeDisplay);
            
            if (this.Context.Display == IntPtr.Zero)
                throw new NotSupportedException("[EGL] GetDisplay failed.: " + Egl.eglGetError());

            if (!Egl.eglInitialize(this.Context.Display, out int major, out int minor))
                throw new NotSupportedException("[EGL] Failed to initialize EGL display. Error code: " + Egl.eglGetError());
            this.Context.Major = major;
            this.Context.Minor = minor;
            
            var isModifiersSupported = ContextExtension.GetExtensions(this.Context.Display).Contains("EGL_EXT_image_dma_buf_import_modifiers");

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
                Definition.SAMPLE_BUFFERS, 1,
                Definition.SAMPLES, 4,
                Definition.NONE
            };
            
            this.Context.Config = ContextExtension.GetConfig(this.Context.Display, desiredConfig);
            var contextAttrib = new[] {
                Definition.CONTEXT_CLIENT_VERSION, 3,
                Definition.NONE
            };
            this.Context.Context = Egl.CreateContext(this.Context.Display, this.Context.Config, IntPtr.Zero, contextAttrib);
            if (this.Context.Context == IntPtr.Zero)
                throw new NotSupportedException(String.Format("[EGL] Failed to create egl context, error {0}.", Egl.eglGetError()));

            this.Context.Surface = Egl.eglCreateWindowSurface(this.Context.Display, this.Context.Config, this.Context.NativeWindow, null);

            if (this.Context.Surface == IntPtr.Zero)
                throw new NotSupportedException(String.Format("[EGL] Failed to create egl surface, error {0}.", Egl.eglGetError()));

            if (!Egl.eglMakeCurrent(this.Context.Display, this.Context.Surface, this.Context.Surface, this.Context.Context))
                throw new NotSupportedException(String.Format("[EGL] Failed to make current, error {0}.", Egl.eglGetError()));

            OpenGLES.GL.glViewport(0, 0, this.Context.Width, this.Context.Height);
            return this;
        }

        public IRenderer Clear()
        {
            OpenGLES.GL.glClear(OpenGLES.Def.ClearBufferMask.ColorBufferBit);
            return this;
        }

        public IRenderer ClearColor(IColor color)
        {
            var (r, g, b, a) = color.ToRGBAF();
             OpenGLES.GL.glClearColor(r, g, b, a);
             return this;
        }

        public IRenderer SwapBuffers()
        {
            Egl.eglSwapBuffers ( this.Context.Display, this.Context.Surface );
            return this;
        }
    }
}
