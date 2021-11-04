using System;
using System.Runtime.InteropServices;
using Pixel.Core;

namespace Pixel.Windows
{
    public class PixelGLShader
    {
        public OpenGLES.GFX.GfxProgram Program { get; set; }
        public uint PosAttrib { get; set; }
        public uint ColorAttrib { get; set; }


        public PixelGLShader()
        {
            this.Program = new OpenGLES.GFX.GfxProgram(@"Shader/simplevertshader_v3.glsl", @"Shader/simplefragshader_v3.glsl");
            
            this.PosAttrib = OpenGLES.GL.glGetAttribLocation(this.Program, "a_position");


            this.ColorAttrib = OpenGLES.GL.glGetAttribLocation(this.Program, "a_color");
        } 

        public void Use()
        {
            OpenGLES.GL.glUseProgram(this.Program);
            OpenGLES.GL.glBindVertexArray(0);
            OpenGLES.GL.glEnableVertexAttribArray(this.PosAttrib);
            OpenGLES.GL.glVertexAttribPointerN(this.PosAttrib, 2, false, (uint)Marshal.SizeOf(typeof(Vertex)), 0);
            OpenGLES.GL.glEnableVertexAttribArray(this.ColorAttrib);
            OpenGLES.GL.glVertexAttribPointerN(this.ColorAttrib, 4, false, (uint)Marshal.SizeOf(typeof(Vertex)), Marshal.SizeOf(typeof(float)) * 2);
        }
    }
}
