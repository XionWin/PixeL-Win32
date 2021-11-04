#version 300 es
#define UNIFORMARRAY_SIZE 11
#define EDGE_AA 1
layout(location = 0) in vec2 a_position;
layout(location = 1) in vec4 a_color;
out vec4 v_color;
uniform mat4 proj_mat;
uniform mat4 model_mat;
void main()
{
        v_color = a_color;
        gl_Position = vec4(a_position, 0, 1);
}