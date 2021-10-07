using System;
using System.Runtime.InteropServices;

namespace Win32
{
    [StructLayout(LayoutKind.Sequential)]
	unsafe struct WndClassExW
	{
		public uint cbSize;
		// public WNDCLASS_STYLES style;
		public unsafe delegate* unmanaged[Stdcall]<IntPtr, uint, nuint, nint, nint> lpfnWndProc;
		public int cbClsExtra;
		public int cbWndExtra;
		public IntPtr hInstance;
		public IntPtr hIcon;
		public IntPtr hCursor;
		public IntPtr hbrBackground;
		public char* lpszMenuName;
		public char* lpszClassName;
		public IntPtr hIconSm;
	}
}
