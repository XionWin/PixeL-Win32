namespace OpenGLES.GFX
{
    public class GfxProgram: GfxObject
    {
        public GfxProgram(string vertexShaderPath, string fragmentShaderPath): base()
        {
            this.Id = GL.glCreateProgram();

            using(var vertexShader = new GfxShader(Def.ShaderType.VertexShader, vertexShaderPath).Load().Check())
            using(var fragmentShader = new GfxShader(Def.ShaderType.FragmentShader, fragmentShaderPath).Load().Check())
            {
                this.AttachShader(vertexShader)
                .AttachShader(fragmentShader)
                .Link()
                .Check();
            }
        }

        protected override void Release() => GL.glDeleteProgram(this.Id);
    }

    static class GfxProgramExtension {
        public static GfxProgram AttachShader(this GfxProgram program, GfxShader shader) {
            GL.glAttachShader(program.Id, shader.Id);
            return program;
        }
        public static GfxProgram Link(this GfxProgram program) {
            GL.glLinkProgram(program.Id);
            program.CheckLink();
            return program;
        }

        public static GfxProgram CheckLink(this GfxProgram program) {
            if(!GL.glGetProgramLinkedStatus(program))
            {
                throw new OpenGLESException(GL.glGetProgramLinkedInformation(program));
            }
            return program;
        }
    }
}
