﻿using System;
using System.Runtime.InteropServices;
using Pixel.Drawing;
using Pixel.Color;
using Pixel.Windows;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");



            var window = new PixelWindow(1024, 640, "OpenGL ES 3.0");
            var context = new PixelParam(window);
            var param = new PixelMethod();

            var pixel = new PixelGraphic(window, context, param);

            Console.WriteLine($"GL Extensions: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Extensions)}");
            Console.WriteLine($"GL Version: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Version)}");
            Console.WriteLine($"GL Sharding Language Version: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.ShadingLanguageVersion)}");
            Console.WriteLine($"GL Vendor: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Vendor)}");
            Console.WriteLine($"GL Renderer: {OpenGLES.GL.GetString(OpenGLES.Def.StringName.Renderer)}");


            var shader = new PixelGLShader();

            
            uint size = 6;
            const float TRIANGLE_SIZE = 0.8f;

            var vertices = new Vertex[size];
            vertices[0].x = 0f;
            vertices[0].y = 0f;
            vertices[0].r = 1f;
            vertices[0].g = .0f;
            vertices[0].b = .0f;
            vertices[0].a = 1f;

            vertices[1].x = 0f;
            vertices[1].y = -.25f;
            vertices[1].r = .0f;
            vertices[1].g = 1f;
            vertices[1].b = .0f;
            vertices[1].a = 1f;

            vertices[2].x = -.25f;
            vertices[2].y = 0f;
            vertices[2].r = .0f;
            vertices[2].g = .0f;
            vertices[2].b = 1f;
            vertices[2].a = 1f;

            
            vertices[3].x = 0f;
            vertices[3].y = 0f;
            vertices[3].r = 1f;
            vertices[3].g = .0f;
            vertices[3].b = .0f;
            vertices[3].a = 1f;

            vertices[4].x = 0f;
            vertices[4].y = .25f;
            vertices[4].r = .0f;
            vertices[4].g = 1f;
            vertices[4].b = .0f;
            vertices[4].a = 1f;

            vertices[5].x = .25f;
            vertices[5].y = 0f;
            vertices[5].r = .0f;
            vertices[5].g = .0f;
            vertices[5].b = 1f;
            vertices[5].a = 1f;
            
            var indices = new short[]
            {
                0, 1, 2, 
                3, 4, 5
            };

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

                vertices[0].x = -(float)random.NextDouble();
                vertices[0].y = (float)random.NextDouble();
                vertices[0].r = (float)random.NextDouble();
                vertices[0].g = (float)random.NextDouble();
                vertices[0].b = (float)random.NextDouble();
                vertices[0].a = 1f;

                unsafe
                {
                    fixed (Vertex* ptrVertex = vertices)
                    fixed (short* ptrIndices = indices)
                    {
                        
                        OpenGLES.GL.glBufferData(OpenGLES.Def.BufferTarget.ArrayBuffer, (int)(Marshal.SizeOf(typeof(Vertex)) * size), (nint)ptrVertex, OpenGLES.Def.BufferUsageHint.StreamDraw);

                        OpenGLES.GL.glBufferData(OpenGLES.Def.BufferTarget.ElementArrayBuffer, (int)(Marshal.SizeOf(typeof(short)) * size), (nint)ptrIndices, OpenGLES.Def.BufferUsageHint.StreamDraw);

                        shader.Use();

                        // SetRotationMatrix(angle / 360d * Math.PI * 2, model_mat_location);
                        // OpenGLES.GL.glDrawElements(OpenGLES.Def.BeginMode.Triangles, 3, OpenGLES.Def.DrawElementsType.UnsignedShort, 0);
                        OpenGLES.GL.glDrawArrays(OpenGLES.Def.BeginMode.TriangleFan, 0, 3);
                        OpenGLES.GL.glDrawArrays(OpenGLES.Def.BeginMode.TriangleFan, 3, 3);
                    }
                }


                
      
                
            };

            

            

            pixel.Show();
        }
    }
}
