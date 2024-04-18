attribute highp vec3 			position;
attribute highp vec2 			texcoord;
attribute highp float   		a_boneIndex;

#define INSTANCE_COUNT 20

uniform highp mat4              u_mvpTransforms[INSTANCE_COUNT];
uniform highp vec4              u_texScaleOffsets[INSTANCE_COUNT];
uniform lowp vec4          		u_colors[INSTANCE_COUNT];

varying lowp vec4				v_color;
varying highp vec2 				v_texcoord;

highp vec2 transformTexCoord (const highp vec2 texCoord, const highp vec4 scaleOffset)
{
	return vec2(texCoord.st * scaleOffset.xy + scaleOffset.zw);
}

void main()
{
	mediump int instanceIndex = int(a_boneIndex);
		
	gl_Position = u_mvpTransforms[instanceIndex] * vec4(position, 1.0);
    
    v_color = u_colors[instanceIndex];
    v_texcoord = transformTexCoord(texcoord, u_texScaleOffsets[instanceIndex]);
}
