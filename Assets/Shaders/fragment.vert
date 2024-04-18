attribute highp vec3 			position;
attribute highp vec2 			texcoord;
attribute highp float   		a_boneIndex;
attribute highp vec3            normal;

uniform highp mat4              u_MVPMatrix;
uniform highp mat4              u_MVMatrix;
uniform highp mat4              u_TEXMatrix;
uniform highp mat4				u_lightCookieMatrix;

uniform lowp vec4				u_color;

uniform highp vec3              u_lightPos;
uniform highp vec3              u_muzzleDir;
uniform highp float             u_lightIntensity;

varying highp vec2 				v_texcoord;
varying highp vec3				v_normal;
varying highp vec2				v_lightCookieUV;
varying lowp vec4				v_color;
varying lowp float              v_lightIntensity;

void main()
{
	v_normal		    = normalize((u_MVMatrix * vec4(normal, 0.0)).xyz);
	vec3 pos		    = (u_MVMatrix * vec4(position, 1.0)).xyz;
	vec3 lightDiff      = (u_lightPos - pos);

	float invDist	    = inversesqrt(dot(lightDiff, lightDiff));
	vec3 lightDir	    = invDist * lightDiff;
	v_lightIntensity    =  max(dot(lightDir, v_normal), 0.0) *(dot(lightDir, -u_muzzleDir) * 0.4 + 0.6) * u_lightIntensity * invDist;

	v_lightCookieUV     = (u_lightCookieMatrix * vec4(position, 1.0)).xy;
	gl_Position		    = u_MVPMatrix * vec4(position, 1.0);

    v_texcoord		    = (u_TEXMatrix * vec4(texcoord, 0.0, 1.0)).st;
	v_color			    = u_color.rgba;
}
