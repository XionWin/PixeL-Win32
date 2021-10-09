using System;

namespace Win32.FFI.User32.Definition
{
    public enum GWL
    {
        // Sets a new extended window style.
        GWL_EXSTYLE = -20,

        // Sets a new application instance handle.
        GWL_HINSTANCE = -6,

        // Sets a new identifier of the child window. The window cannot be a top-level window.
        GWL_ID = -12,

        // Sets a new window style.
        GWL_STYLE = -16,

        // Sets the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
        GWL_USERDATA = -21,

        // Sets a new address for the window procedure.
        // You cannot change this attribute if the window does not belong to the same process as the calling thread.
        GWL_WNDPROC = -4,
    }
}