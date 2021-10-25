using System;
using System.Runtime.InteropServices;
using EGL.Definitions;

namespace EGL
{
    using EGLConfig = IntPtr;
    using EGLContext = IntPtr;
    using EGLDisplay = IntPtr;
    using EGLSurface = IntPtr;
    
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate nint GetPlatformDisplayEXTHandler(uint platform, nint native_display, uint[] attrib_list);
    unsafe public static class ContextExtension
    {
        public static string GetVersion(EGLDisplay display) => Egl.QueryString(display, Definition.VERSION);
        public static string GetVendor(EGLDisplay display) => Egl.QueryString(display, Definition.VENDOR);
        public static string GetExtensions(EGLDisplay display) => Egl.QueryString(display, Definition.EXTENSIONS);
        public static string OffScreenExtensions => Egl.QueryString(IntPtr.Zero, Definition.EXTENSIONS);

        #region ctor

        #if !Win32
        #endif
        public static (EGLDisplay display, EGLContext context, EGLSurface surface, EGLConfig config, int major, int minor) CrateContext(RenderableSurfaceType surfaceType)
        {
            (EGLDisplay display, EGLContext context, EGLSurface surface, EGLConfig config, int major, int minor) result = default;

            return result;
        }
        #endregion

        
        public static nint GetConfig(EGLDisplay display, int[] desiredConfig)
        {
            int num_configs;
            var configs = new nint[1];
            if (!Egl.eglChooseConfig(display, desiredConfig, configs, 1, out num_configs) || num_configs < 1)
                throw new NotSupportedException(String.Format("[EGL] Failed to retrieve GraphicsMode, error {0}", Egl.eglGetError()));
            return configs[0];
        }
        public static nint[] GetAllConfigs(EGLDisplay display, int[] desiredConfig)
        {
            int num_configs;
            if (!Egl.eglChooseConfig(display, desiredConfig, null, 0, out num_configs) || num_configs == 0)
                throw new NotSupportedException(String.Format("[EGL] Failed to retrieve GraphicsMode, error {0}", Egl.eglGetError()));

            var configs = new nint[num_configs];
            if (!Egl.eglChooseConfig(display, null, configs, num_configs, out num_configs))
                throw new NotSupportedException(String.Format("[EGL] Failed to retrieve GraphicsMode, error {0}", Egl.eglGetError()));
            return configs;
        }
        public static void DumpAllConf(EGLDisplay display, int[] desiredConfig)
        {
            Console.Write("EGL Configs");
            nint[] configs = ContextExtension.GetAllConfigs(display, desiredConfig);
            int[] attribs = new int[] {
                (int)Definitions.Attribute.BufferSize,
                Definition.RED_SIZE,
                Definition.GREEN_SIZE,
                Definition.BLUE_SIZE,
                Definition.ALPHA_SIZE,
                (int)Definitions.Attribute.DepthSize,
                Definition.WIDTH,
                Definition.HEIGHT,
                (int)Definitions.Attribute.Samples,
                (int)Definitions.Attribute.SampleBuffers,
                (int)Definitions.Attribute.RenderableType,
                (int)Definitions.Attribute.SurfaceType,
                (int)Definitions.Attribute.Level,
                (int)Definitions.Attribute.ConfigCaveat,
            };

            for (int i = 0; i < configs.Length; i++)
            {
                nint conf = configs[i];
                Console.Write("{0,-3}:", i);
                for (int j = 0; j < attribs.Length; j++)
                {
                    int value;
                    Egl.eglGetConfigAttrib(display, conf, attribs[j], out value);
                    Console.Write((j == 0 ? string.Empty : ", ") + "{0} = {1}", Egl.EglConstToString((int)attribs[j]), value);
                }
                Console.Write("\n");
            }
        }

        public static void ResetMakeCurrent(EGLDisplay display)
        {
            if (!Egl.eglMakeCurrent(display, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero))
                Console.WriteLine("egl clear current ctx failed");
        }

    }
}

