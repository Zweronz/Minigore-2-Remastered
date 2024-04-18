uniform sampler2D	u_texture;
uniform sampler2D	u_texture1;

uniform lowp float	u_fogAmount;
uniform lowp vec4	u_fogColor;
uniform lowp vec3	u_groundColor;
uniform lowp vec3	u_halfAngle;
uniform lowp vec3	u_lightDir;

varying lowp vec4	v_color;
varying highp vec2	v_lightCookieUV;
varying lowp float	v_lightIntensity;
varying lowp vec3	v_normal;
varying highp vec2	v_texcoord;

const lowp vec3     rimDir			= normalize(vec3(-0.3, 0.15, 0.0));
const lowp vec4     grayScale		= vec4(0.13, 0.15, 0.12, 0.08);
const lowp vec3     rimMaskColor	= vec3(0.5, 0.5, 0.5);

void main()
{
	lowp vec4 tex       = texture2D(u_texture, v_texcoord);
	lowp vec3 normal    = normalize(v_normal);
	lowp float att      = dot(normal, u_lightDir) * 0.5 + 0.5; // Wrap light.
	
	lowp float rimAtt   = clamp(dot(normal, rimDir), 0.0, 1.0);

	rimAtt *= rimAtt;
	rimAtt *= dot(tex.rgb, rimMaskColor);

	lowp float specMask 	= dot(tex, grayScale);
	lowp float spec     	= pow(clamp(dot(u_halfAngle, normal), 0.0, 1.0), 16.0) * specMask;
	lowp vec3 color     	= mix(u_groundColor, texture2D(u_texture1, v_lightCookieUV).rgb * 1.5, att);

	color += vec3(0.8, 0.64, 0.24) * v_lightIntensity;
	
	lowp float fresnel		= clamp(1.0 - normal.z, 0.0, 1.0);
	lowp float specialGlow	= fresnel;
	fresnel *= fresnel;
	fresnel *= fresnel;
	color += fresnel * 0.5;

	gl_FragColor = vec4((color * (spec + tex.rgb * v_color.rgb) + vec3(rimAtt)), 1.0);	

// FOG
	lowp float fogFactor	= 0.0;	
	highp float dist		= 0.0;
	dist					= gl_FragCoord.z / gl_FragCoord.w;

// FOG EXP2
	//medp float fogDensity	= 0.0015;	
	//fogFactor = 1.0 / exp((dist * fogDensity) * (dist * fogDensity));
// FOG LINEAR	
	fogFactor = (700.0 - dist) / (700.0 - 1.0);
	fogFactor = clamp(fogFactor, 0.0, 1.0);	
	fogFactor = 1.0 - fogFactor;
	fogFactor *= u_fogColor.a;	
	fogFactor *= u_fogAmount * 0.5;
	
	gl_FragColor.rgb    = mix(gl_FragColor.rgb, u_fogColor.rgb, fogFactor);
	gl_FragColor.rgb   += mix(vec3(0.0, 0.0, 0.0), v_color.rgb, (1.0 - v_color.a) * specialGlow) * ((tex.r + tex.b) * 2.0 + 0.35 + fresnel);
	gl_FragColor        = clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0));
	
	//gl_FragColor.rgb	= u_fogColor.rgb; // DEBUG SWITCH
}