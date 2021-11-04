using System;
using Pixel.Core;
using EGL;
using EGL.Definitions;

namespace Pixel.Windows
{
    public class PixelMethod : IMethod
    {
        public Action<IParam> RenderCreate => context => {
            if (context.NativeWindow == IntPtr.Zero)
                throw new Exception("PixelRenderer initialize error. NativeWindow not found");
            context.Display = Egl.eglGetDisplay(context.NativeDisplay);
            
            if (context.Display == IntPtr.Zero)
                throw new NotSupportedException("[EGL] GetDisplay failed.: " + Egl.eglGetError());

            if (!Egl.eglInitialize(context.Display, out int major, out int minor))
                throw new NotSupportedException("[EGL] Failed to initialize EGL display. Error code: " + Egl.eglGetError());
            context.Major = major;
            context.Minor = minor;
            
            var isModifiersSupported = ContextExtension.GetExtensions(context.Display).Contains("EGL_EXT_image_dma_buf_import_modifiers");

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
            
            context.Config = ContextExtension.GetConfig(context.Display, desiredConfig);
            var contextAttrib = new[] {
                Definition.CONTEXT_CLIENT_VERSION, 3,
                Definition.NONE
            };
            context.Context = Egl.CreateContext(context.Display, context.Config, IntPtr.Zero, contextAttrib);
            if (context.Context == IntPtr.Zero)
                throw new NotSupportedException(String.Format("[EGL] Failed to create egl context, error {0}.", Egl.eglGetError()));

            context.Surface = Egl.eglCreateWindowSurface(context.Display, context.Config, context.NativeWindow, null);

            if (context.Surface == IntPtr.Zero)
                throw new NotSupportedException(String.Format("[EGL] Failed to create egl surface, error {0}.", Egl.eglGetError()));

            if (!Egl.eglMakeCurrent(context.Display, context.Surface, context.Surface, context.Context))
                throw new NotSupportedException(String.Format("[EGL] Failed to make current, error {0}.", Egl.eglGetError()));
        };
        
        public Action<int, int> RenderSetViewPortHandler => (width, height) => OpenGLES.GL.glViewport(0, 0, width, height);

        public Action RenderClear => () => OpenGLES.GL.glClear(OpenGLES.Def.ClearBufferMask.ColorBufferBit);
        public Action<IColor> RenderClearColor => color => {
            var (r, g, b, a) = color.ToRGBAF();
            OpenGLES.GL.glClearColor(r, g, b, a);
        };

        public Action<nint, nint> RenderSwapBuffers => (display, surface) => Egl.eglSwapBuffers (display, surface);
        public Action<nint, nint> RenderDelete { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
