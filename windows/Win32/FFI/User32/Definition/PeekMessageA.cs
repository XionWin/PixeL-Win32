using System;

namespace Win32.FFI.User32.Definition
{
    public enum PeekMessageA
    {
        // Messages are not removed from the queue after processing by PeekMessage.
        PM_NOREMOVE = 0x0000,
        // Messages are removed from the queue after processing by PeekMessage.
        PM_REMOVE = 0x0001,
        // Prevents the system from releasing any thread that is waiting for the caller to go idle (see WaitForInputIdle). Combine this value with either PM_NOREMOVE or PM_REMOVE.
        PM_NOYIELD = 0x0002,
    }
}