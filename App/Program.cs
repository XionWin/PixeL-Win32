using System;
using System.Runtime.InteropServices;
using Renderer.Definitions;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var pixel = new Pixel.Pixel(1024, 640, "OpenGL ES 3.0");
            pixel.ShowAnalysis = true;
            
            Console.WriteLine($"GL Extensions: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Extensions)}");
            Console.WriteLine($"GL Version: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Version)}");
            Console.WriteLine($"GL Sharding Language Version: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.ShadingLanguageVersion)}");
            Console.WriteLine($"GL Vendor: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Vendor)}");
            Console.WriteLine($"GL Renderer: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Renderer)}");

            
            uint size = 3;
            const float TRIANGLE_SIZE = 0.8f;

            var vertices = new Vertex[size];
            vertices[0].x = -.5f;
            vertices[0].y = .5f;
            vertices[0].r = 1f;
            vertices[0].g = .0f;
            vertices[0].b = .0f;
            vertices[0].a = 1f;

            vertices[1].x = -.5f;
            vertices[1].y = -.5f;
            vertices[1].r = .0f;
            vertices[1].g = 1f;
            vertices[1].b = .0f;
            vertices[1].a = 1f;

            vertices[2].x = .5f;
            vertices[2].y = -.5f;
            vertices[2].r = .0f;
            vertices[2].g = .0f;
            vertices[2].b = 1f;
            vertices[2].a = 1f;
            
            var indices = new short[]
            {
                0, 1, 2
            };

            var vbos = new uint[2];

            unsafe
            {
                fixed (Vertex* ptrVertex = vertices)
                fixed (short* ptrIndices = indices)
                {
                    for (int i = 0; i < size; i++)
                    {
                        vertices[i].x = (float)Math.Cos(-i * (2.0f * Math.PI / (size))) * TRIANGLE_SIZE;
                        vertices[i].y = (float)Math.Sin(-i * (2.0f * Math.PI / (size))) * TRIANGLE_SIZE;
                    }
                    
                    OpenGLES.GL.glBindVertexArray(0);

                    OpenGLES.GL.GenBuffers((uint)vbos.Length, out vbos);
                    OpenGLES.GL.glBindBuffer(OpenGLES.Def.BufferTarget.ArrayBuffer, vbos[0]);
                    OpenGLES.GL.glBufferData(OpenGLES.Def.BufferTarget.ArrayBuffer, (int)(Marshal.SizeOf(typeof(Vertex)) * size), (nint)ptrVertex, OpenGLES.Def.BufferUsageHint.StreamDraw);

                    OpenGLES.GL.glBindBuffer(OpenGLES.Def.BufferTarget.ElementArrayBuffer, vbos[1]);
                    OpenGLES.GL.glBufferData(OpenGLES.Def.BufferTarget.ElementArrayBuffer, (int)(Marshal.SizeOf(typeof(short)) * 3), (nint)ptrIndices, OpenGLES.Def.BufferUsageHint.StreamDraw);


                
                    OpenGLES.GL.glGenVertexArrays(1, out var vao);
                    OpenGLES.GL.glBindVertexArray(vao);

                    OpenGLES.GL.glBindVertexArray(0);


                    using (var program = new OpenGLES.GFX.GfxProgram(@"Shader/simplevertshader_v3.glsl", @"Shader/simplefragshader_v3.glsl"))
                    {
                        OpenGLES.GL.glUseProgram(program);
                        OpenGLES.GL.glBindVertexArray(0);

                        uint posAttrib = OpenGLES.GL.glGetAttribLocation(program, "a_position");
                        OpenGLES.GL.glEnableVertexAttribArray(posAttrib);
                        OpenGLES.GL.glVertexAttribPointerN(posAttrib, 2, false, (uint)Marshal.SizeOf(typeof(Vertex)), 0);


                        uint colorAttrib = OpenGLES.GL.glGetAttribLocation(program, "a_color");
                        OpenGLES.GL.glEnableVertexAttribArray(colorAttrib);
                        OpenGLES.GL.glVertexAttribPointerN(colorAttrib, 4, false, (uint)Marshal.SizeOf(typeof(Vertex)), Marshal.SizeOf(typeof(float)) * 2);

                        // var proj_mat_location = OpenGLES.GL.glGetUniformLocation(program, "proj_mat");
                        // var model_mat_location = OpenGLES.GL.glGetUniformLocation(program, "model_mat");

                        
                        var hsl = new Color.HSLA(0, 1, 0.5f);
                        var angle = 0.0f;
                        pixel.OnDraw += () => {
                            pixel.ClearColor(hsl);
                            hsl.H = angle += 0.05f;
                            hsl.H = angle %= 360;

                            OpenGLES.GL.glBindVertexArray(0);
                            // SetRotationMatrix(angle / 360d * Math.PI * 2, model_mat_location);
                            OpenGLES.GL.glDrawElements(OpenGLES.Def.BeginMode.Triangles, 3, OpenGLES.Def.DrawElementsType.UnsignedShort, 0);
                            
                        };

                    }
                }
            }

            

            pixel.Show();
        }
    }
}
