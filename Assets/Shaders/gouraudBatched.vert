attribute highp vec3 			position;
attribute highp vec2 			texcoord;
attribute highp float   		a_boneIndex;
attribute highp vec3            normal;

#define INSTANCE_COUNT 20

uniform highp mat4              u_mvpTransforms[INSTANCE_COUNT];
uniform highp vec4              u_texScaleOffsets[INSTANCE_COUNT];
uniform highp vec4          	u_colors[INSTANCE_COUNT];
uniform highp mat4				u_proj;
uniform highp vec3				u_groundColor;

varying highp vec2 				v_texcoord;
varying highp vec4				v_color;

const highp  vec3 lightDir      = normalize(vec3(0.5, 0.9, 0.9));
const highp vec3 lightColor     = vec3(1.0, 1.0, 1.0);

highp vec2 transformTexCoord (const highp vec2 texCoord, const highp vec4 scaleOffset)
{
	return vec2(texCoord.st * scaleOffset.xy + scaleOffset.zw);
}

void main()
{
	highp int instanceIndex = int(a_boneIndex);
		
	highp vec4 pos = u_mvpTransforms[instanceIndex] * vec4(position, 1.0);
    gl_Position    = u_proj * pos;

	highp vec3 N    = (u_mvpTransforms[instanceIndex] * vec4(normal, 0.0)).xyz;
	highp float att = (dot(N, lightDir) * 0.5 + 0.5);
	
	v_color           = vec4(mix(u_groundColor, lightColor, att) * u_colors[instanceIndex].rgb, 1.0);
    v_texcoord        = transformTexCoord(texcoord, u_texScaleOffsets[instanceIndex]);
}
