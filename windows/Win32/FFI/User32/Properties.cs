
using System.Runtime.InteropServices;

namespace Win32.FFI.User32
{
    internal static class Properties
    {
#if !UNICODE
        public const CharSet BuildCharSet = CharSet.Ansi;
#else
        public const CharSet BuildCharSet = CharSet.Unicode;
#endif
    }
}