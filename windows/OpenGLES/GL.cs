﻿using System;
using System.Runtime.InteropServices;

namespace OpenGLES
{
    public class GL
    {
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern nint glGetString(Def.StringName stringName);
        public static string GetString(Def.StringName stringName) => Marshal.PtrToStringAnsi(glGetString(stringName));

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glClearColor(float red, float green, float blue, float alpha);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glViewport(int x, int y, int width, int height);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glClear(Def.ClearBufferMask mask);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern uint glGetError();

        public static void GetError(string extraMessage = null)
        {
            if(GL.glGetError() is var errorCode && errorCode != 0) 
                throw new OpenGLESException($"ErrorCode: {errorCode}"
                + (string.IsNullOrEmpty(extraMessage) ? string.Empty : $" ExtralMessage: {extraMessage}"));
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint glCreateProgram ();

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern uint glCreateShader(Def.ShaderType shaderType);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glShaderSource (uint shader, int count, string[] source, int len);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glCompileShader (uint shader);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glAttachShader (uint programId, uint shaderId);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glLinkProgram (uint programId);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glDeleteShader (uint shaderId);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGetShaderiv (uint shaderId, Def.ShaderParameter shaderParameter, ref int value);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGetShaderInfoLog (uint shaderId, int bufSize, int[] length, byte[] infoLog);
        public static bool glGetShaderCompiledStatus(GFX.GfxShader shader)
        {
            var isCompiled = 0;
            glGetShaderiv(shader.Id, Def.ShaderParameter.CompileStatus, ref isCompiled);
            return isCompiled == 1;
        }

        public static string glGetShaderCompiledInformation(GFX.GfxShader shader)
        {
            var len = 0;
            glGetShaderiv(shader.Id, Def.ShaderParameter.InfoLogLength, ref len);
            if(len > 1)
            {
                var bs = new byte[len];
                glGetShaderInfoLog(shader.Id, len, null, bs);
                return System.Text.Encoding.ASCII.GetString(bs);
            }
            return string.Empty;
        }
        
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glDeleteProgram(uint programId);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGetProgramiv (uint programId, Def.ProgramParameter programParameter, ref int value);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGetProgramInfoLog (uint programId, int bufSize, int[] length, byte[] infoLog);
        public static bool glGetProgramLinkedStatus(GFX.GfxProgram program)
        {
            var isLinked = 0;
            glGetProgramiv(program.Id, Def.ProgramParameter.LinkStatus, ref isLinked);
            return isLinked == 1;
        }

        public static string glGetProgramLinkedInformation(GFX.GfxProgram program)
        {
            var len = 0;
            glGetProgramiv(program.Id, Def.ProgramParameter.InfoLogLength, ref len);
            if(len > 1)
            {
                var bs = new byte[len];
                glGetProgramInfoLog(program.Id, len, null, bs);
                return System.Text.Encoding.ASCII.GetString(bs);
            }
            return string.Empty;
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glUseProgram(uint programId);

        public static void glUseProgram(GFX.GfxProgram program) => glUseProgram(program.Id);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glVertexAttribPointer (uint index, int size, Def.VertexAttribPointerType type, bool normalized, uint stride, nint pointer);

        public unsafe static void glVertexAttribPointerF(uint index, int size, bool normalized, uint stride, float[] data)
        {
            fixed(float *ptr = data)
            {
                glVertexAttribPointer(index, size, Def.VertexAttribPointerType.Float, normalized, stride, (nint)ptr);
            }
        }
        public unsafe static void glVertexAttribPointerN(uint index, int size, bool normalized, uint stride, nint offset)
        {
            glVertexAttribPointer(index, size, Def.VertexAttribPointerType.Float, normalized, stride, (nint)offset);
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern int glGetAttribLocation (uint programId, [MarshalAs(UnmanagedType.LPStr)]string name);
        public static uint glGetAttribLocation (GFX.GfxProgram program, string name)
        {
            return glGetAttribLocation(program.Id, name) is var r && r >= 0 ? (uint)r : throw new OpenGLESException();
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern int glGetUniformLocation (uint programId, [MarshalAs(UnmanagedType.LPStr)]string name);
        public static uint glGetUniformLocation (GFX.GfxProgram program, string name)
        {
            return glGetUniformLocation(program.Id, name) is var r && r >= 0 ? (uint)r : throw new OpenGLESException();
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glEnableVertexAttribArray (uint index);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glDrawArrays(Def.BeginMode beginMode, int first, uint count);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glDrawElements (Def.BeginMode beginMode, uint count, Def.DrawElementsType type, nint indices);



        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glEnable (Def.EnableCap cap);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glStencilMask (uint mask);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glStencilFunc (Def.StencilFunction func, int ref_, uint mask);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glStencilOp (Def.StencilOp fail, Def.StencilOp zFail, Def.StencilOp zPass);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glColorMask (bool red, bool green, bool blue, bool alpha);
        

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glDisable (Def.EnableCap cap);


        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private unsafe static extern void glUniform4fv (int location, int count, float *value);

        
        public static void Uniform4fv (int location, float[] value)
        {
            unsafe
            {
                 fixed (float *ptr = value)
                 {
                    glUniform4fv (location, value.Length, ptr);
                 }
            }
        }
        



        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glGenVertexArrays (uint n, out uint arrayId);
        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glBindVertexArray (uint array);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glGenBuffers (uint n, [In, Out] uint[] buffer);
        public static void GenBuffers (uint n, out uint[] buffer)
        {
            buffer = new uint[n];
            glGenBuffers(n, buffer);
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        private static extern void glDeleteBuffers (uint n, [In] uint[] buffer);
        public static void DeleteBuffers (uint n, uint[] buffer)
        {
            glDeleteBuffers(n, buffer);
        }

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glBindBuffer (Def.BufferTarget target, uint bufferId);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glBufferData (Def.BufferTarget target, int size, nint data, Def.BufferUsageHint bufferUsageHint);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        public static extern void glUniformMatrix4fv (uint location, uint count, [MarshalAs(UnmanagedType.Bool)]bool transpose, float[] value);

        [DllImport(Lib.Name, CallingConvention = CallingConvention.Cdecl)]
        unsafe public static extern void glUniformMatrix4fv (uint location, uint count, [MarshalAs(UnmanagedType.Bool)]bool transpose, float *value);
    }
}
