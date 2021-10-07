using System;
using System.Runtime.InteropServices;

namespace Win32.FFI
{
    public static class Kernel32
    {

        [DllImport(Lib.Kernel32, EntryPoint = "GetModuleHandle")]
        public static extern nint GetModuleHandle([MarshalAs(UnmanagedType.LPStr)] string module);
    }
}