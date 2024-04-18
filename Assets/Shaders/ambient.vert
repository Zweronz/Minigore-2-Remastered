attribute highp vec3 position;
attribute highp vec2 texcoord;

uniform highp mat4 u_MVPMatrix;
uniform highp mat4 u_TEXMatrix;

varying highp vec2 v_texcoord;

void main()
{
	gl_Position = u_MVPMatrix * vec4(position, 1.0);
	v_texcoord = (u_TEXMatrix * vec4(texcoord, 0.0, 1.0)).st;
}
