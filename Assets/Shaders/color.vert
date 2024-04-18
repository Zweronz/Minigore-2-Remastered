attribute highp vec3    position;
attribute lowp vec4     color;

uniform highp mat4      u_MVPMatrix;
uniform lowp vec4       u_color;

varying lowp vec4       v_color;

void main()
{
	v_color = color * u_color;
	gl_Position = u_MVPMatrix * vec4(position, 1.0);
}