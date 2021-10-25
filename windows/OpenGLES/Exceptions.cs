using System;

namespace OpenGLES
{
    public class OpenGLESException : Exception
    {
        public OpenGLESException()
        {

        }

        public OpenGLESException(string msg): base(msg)
        {
        }
    }
}
