#version 300 es
layout(location = 0) in vec2 vertex;
layout(location = 1) in vec2 tcoord;
layout(location = 2) in float angle;
out vec4 v_color;

void main(void) {

float x = 0.0;
float y = 0.0;
float z = 1.0;

float alpha = radians(angle);
float s = sin( alpha );
float c = cos( alpha );

float t = 1.0 - c;

mat4 myx = mat4(
1, 0, 0, 0,
0, c, -s, 0,
0, s, c, 0,
0, 0, 0, 1
);

mat4 myy = mat4(
c, 0, s, 0,
0, 1, 0, 0,
-s, 0, c, 0,
0, 0, 0, 1
);

mat4 myz = mat4(
c, s, 0, 0,
-s, c, 0, 0,
0, 0, 1, 0,
0, 0, 0, 1
);

        v_color = vec4(tcoord, 1.0, 1.0);
        //gl_Position = vec4((2.0 * vertex.x / 1024.0), (2.0 * vertex.y / 640.0), 0, 1) * myz;
        vec4 v = vec4(vertex, 0, 1) * myz;
        gl_Position = vec4((2.0 * v.x / 1024.0), (2.0 * v.y / 640.0), 0, 1);
}