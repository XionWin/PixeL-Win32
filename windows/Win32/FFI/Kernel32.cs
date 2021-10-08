using System;
using System.Runtime.InteropServices;

namespace Win32.FFI
{
    public static class Kernel32
    {

        [DllImport(Lib.Kernel32)]
        public static extern nint GetModuleHandle([MarshalAs(UnmanagedType.LPStr)] string module);
    }
}