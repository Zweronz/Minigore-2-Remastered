attribute highp vec3 			position;
attribute highp vec2 			texcoord;
attribute highp float   		a_boneIndex;
attribute highp vec3            normal;

uniform highp mat4              u_MVMatrix;
uniform highp mat4              u_MVPMatrix;
uniform highp mat4              u_TEXMatrix;
uniform highp vec4              u_color;
uniform highp vec3				u_groundColor;

varying highp vec2 				v_texcoord;
varying highp vec4				v_color;

const highp  vec3 lightDir      = normalize(vec3(0.5, 0.8, 0.9));
const highp vec3 lightColor     = vec3(1.0, 1.0, 1.0);

void main()
{
    gl_Position     = u_MVPMatrix * vec4(position, 1.0);
	highp vec3 N    = (u_MVMatrix * vec4(normal, 0.0)).xyz;
	highp float att = (dot(N, lightDir) * 0.5 + 0.5);

	v_color         = vec4(mix(u_groundColor, lightColor, att) * u_color.rgb, 1.0);
    v_texcoord      = (u_TEXMatrix * vec4(texcoord, 0.0, 1.0)).st;
}
