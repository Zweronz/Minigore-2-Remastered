attribute highp vec3 			position;
attribute highp vec2 			texcoord;
attribute highp float   		a_boneIndex;
attribute highp vec3            normal;

#define INSTANCE_COUNT 20

uniform highp mat4              u_mvpTransforms[INSTANCE_COUNT];
uniform highp vec4              u_texScaleOffsets[INSTANCE_COUNT];
uniform lowp  vec4         	    u_colors[INSTANCE_COUNT];
uniform highp mat4				u_proj;
uniform highp mat4				u_lightCookieMatrix;
uniform highp vec3              u_lightPos;
uniform highp vec3              u_muzzleDir;
uniform highp float             u_lightIntensity;

varying lowp vec4				v_color;
varying highp vec2				v_lightCookieUV;
varying lowp float              v_lightIntensity;
varying lowp vec3				v_normal;
varying highp vec2 				v_texcoord;

highp vec2 transformTexCoord (const highp vec2 texCoord, const highp vec4 scaleOffset)
{
	return vec2(texCoord.st * scaleOffset.xy + scaleOffset.zw);
}

void main()
{
	mediump int instanceIndex = int(a_boneIndex);

	vec4 pos = (u_mvpTransforms[instanceIndex] *  vec4(position,1.0));
	v_normal =  normalize((u_mvpTransforms[instanceIndex] * vec4(normal,0.0)).xyz);

	highp vec3 lightDiff    = (u_lightPos - pos.xyz);
	highp float invDist     = inversesqrt(dot(lightDiff, lightDiff));
	highp vec3 lightDir     = invDist * lightDiff;
	v_lightIntensity        =  max(dot(lightDir, v_normal), 0.0) * (dot(lightDir, -u_muzzleDir) * 0.4 + 0.6) * u_lightIntensity * invDist;

    v_lightCookieUV         = (u_lightCookieMatrix * pos).xy;
	gl_Position             = u_proj * pos;
	v_normal                = (u_mvpTransforms[instanceIndex] * vec4(normal,0.0)).xyz;
	v_color                 = u_colors[instanceIndex].rgba;
    v_texcoord              = transformTexCoord(texcoord, u_texScaleOffsets[instanceIndex]);
}