using System;
using System.Runtime.InteropServices;
using Pixel.Drawing;
using Pixel.Color;
using Pixel.Windows;
using System.Collections.Generic;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var (width, height) = (1024, 640);
            var window = new PixelWindow(width, height, "OpenGL ES 3.0");
            var context = new PixelParam(window);
            var param = new PixelMethod();

            var pixel = new PixelGraphic(window, context, param);

            Console.WriteLine($"GL Extensions: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Extensions)}");
            Console.WriteLine($"GL Version: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Version)}");
            Console.WriteLine($"GL Sharding Language Version: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.ShadingLanguageVersion)}");
            Console.WriteLine($"GL Vendor: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Vendor)}");
            Console.WriteLine($"GL Renderer: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Renderer)}");


            var shader = new PixelGLShader();

            
            const float TRIANGLE_SIZE = 0.8f;

            var vs = new List<Vertex>() {
                new Vertex(){
                    x = 0f,
                    y = 0f,
                    u = 0f,
                    v = 1f,
                },
                new Vertex(){
                    x = 0f,
                    y = 320f,
                    u = 1f,
                    v = .0f,
                },
                new Vertex(){
                    x = 512f,
                    y = 0f,
                    u = 1f,
                    v = .0f,
                },
                // new Vertex(){
                //     x = 407f / width / 2,
                //     y = 392f / height / 2,
                //     r = 1f,
                //     g = .0f,
                //     b = .0f,
                //     a = 1f,
                // }
            };

            
            int size = vs.Count;

            var vertices = vs.ToArray();
            
            // var indices = new short[]
            // {
            //     0, 1, 2, 
            //     3, 4, 5
            // };

            var vbos = new uint[2];

            // for (int i = 0; i < size; i++)
            // {
            //     vertices[i].x = (float)Math.Cos(-i * (2.0f * Math.PI / (size))) * TRIANGLE_SIZE;
            //     vertices[i].y = (float)Math.Sin(-i * (2.0f * Math.PI / (size))) * TRIANGLE_SIZE;
            // }

            OpenGLES.GL.GenBuffers((uint)vbos.Length, out vbos);
            OpenGLES.GL.glBindBuffer(OpenGLES.Def.BufferTarget.ArrayBuffer, vbos[0]);
            OpenGLES.GL.glBindBuffer(OpenGLES.Def.BufferTarget.ElementArrayBuffer, vbos[1]);
           
            
            var hsl = new HSLA(0, 1, 0.5f);
            var angle = 0.0f;

            var random = new Random();
            pixel.OnDraw += () => {
                // pixel.ClearColor(hsl);
                // hsl.H = angle += 0.05f;
                // hsl.H = angle %= 360;

                // vertices[0].x = -(float)random.NextDouble();
                // vertices[0].y = (float)random.NextDouble();
                // vertices[0].r = (float)random.NextDouble();
                // vertices[0].g = (float)random.NextDouble();
                // vertices[0].b = (float)random.NextDouble();
                // vertices[0].a = 1f;

                for (int i = 0; i < vertices.Length; i++)
                {
                    vertices[i].a = angle;
                }
                
                angle+= 0.05f;
                angle %= 360f;

                unsafe
                {
                    fixed (Vertex* ptrVertex = vertices)
                    // fixed (short* ptrIndices = indices)
                    {
                        
                        OpenGLES.GL.glBufferData(OpenGLES.Def.BufferTarget.ArrayBuffer, (int)(Marshal.SizeOf(typeof(Vertex)) * size), (nint)ptrVertex, OpenGLES.Def.BufferUsageHint.StreamDraw);

                        // OpenGLES.GL.glBufferData(OpenGLES.Def.BufferTarget.ElementArrayBuffer, (int)(Marshal.SizeOf(typeof(short)) * size), (nint)ptrIndices, OpenGLES.Def.BufferUsageHint.StreamDraw);

                        shader.Use();




                        OpenGLES.GL.glEnable(OpenGLES.Def.EnableCap.StencilTest);
                        OpenGLES.GL.glStencilMask(0xff);

                        OpenGLES.GL.glStencilFunc(OpenGLES.Def.StencilFunction.Equal, 0x00, 0xff);
                        OpenGLES.GL.glStencilOp(OpenGLES.Def.StencilOp.Keep, OpenGLES.Def.StencilOp.Keep, OpenGLES.Def.StencilOp.Incr);

                        OpenGLES.GL.GetError("Stroke fill 0");

                        // SetRotationMatrix(angle / 360d * Math.PI * 2, model_mat_location);
                        // OpenGLES.GL.glDrawElements(OpenGLES.Def.BeginMode.Triangles, 3, OpenGLES.Def.DrawElementsType.UnsignedShort, 0);
                        OpenGLES.GL.glDrawArrays(OpenGLES.Def.BeginMode.TriangleStrip, 0, 3);
                        OpenGLES.GL.glDrawArrays(OpenGLES.Def.BeginMode.TriangleStrip, 3, 3);

                        

                        OpenGLES.GL.glStencilFunc(OpenGLES.Def.StencilFunction.Equal, 0x00, 0xff);
                        OpenGLES.GL.glStencilOp(OpenGLES.Def.StencilOp.Keep, OpenGLES.Def.StencilOp.Keep, OpenGLES.Def.StencilOp.Keep);

                        OpenGLES.GL.GetError("Stroke fill 0");

                        // SetRotationMatrix(angle / 360d * Math.PI * 2, model_mat_location);
                        // OpenGLES.GL.glDrawElements(OpenGLES.Def.BeginMode.Triangles, 3, OpenGLES.Def.DrawElementsType.UnsignedShort, 0);
                        OpenGLES.GL.glDrawArrays(OpenGLES.Def.BeginMode.TriangleStrip, 0, 3);
                        OpenGLES.GL.glDrawArrays(OpenGLES.Def.BeginMode.TriangleStrip, 3, 3);

                        

                        OpenGLES.GL.glStencilFunc(OpenGLES.Def.StencilFunction.Equal, 0x00, 0xff);
                        OpenGLES.GL.glStencilOp(OpenGLES.Def.StencilOp.Zero, OpenGLES.Def.StencilOp.Zero, OpenGLES.Def.StencilOp.Zero);

                        OpenGLES.GL.GetError("Stroke fill 0");

                        // SetRotationMatrix(angle / 360d * Math.PI * 2, model_mat_location);
                        // OpenGLES.GL.glDrawElements(OpenGLES.Def.BeginMode.Triangles, 3, OpenGLES.Def.DrawElementsType.UnsignedShort, 0);
                        OpenGLES.GL.glDrawArrays(OpenGLES.Def.BeginMode.TriangleStrip, 0, 3);
                        OpenGLES.GL.glDrawArrays(OpenGLES.Def.BeginMode.TriangleStrip, 3, 3);

                        
                        OpenGLES.GL.glColorMask(true, true, true, true);
                        OpenGLES.GL.glDisable(OpenGLES.Def.EnableCap.StencilTest);
                    }
                }


                
      
                
            };

            

            

            pixel.Show();
        }
    }
}
