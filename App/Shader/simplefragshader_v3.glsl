#version 300 es
#define UNIFORMARRAY_SIZE 11
#define EDGE_AA 1
precision mediump float;
in vec4 v_color;
out vec4 o_fragColor; 
void main()
{
   o_fragColor = v_color;
}