uniform sampler2D   u_texture;
uniform sampler2D   u_texture1;

uniform lowp float  u_alphaRef;
uniform lowp vec4   u_color;
uniform lowp vec4   u_fogColor;
uniform lowp float  u_fogAmount;

varying highp vec2  v_texcoord;
varying highp vec2  v_lightCookieUV;

void main()
{
	lowp vec4 color = texture2D(u_texture, v_texcoord);

    gl_FragColor = vec4(0.0, 0.0, 0.0, 0.0);

	if (color.a < u_alphaRef)
	{
		discard;
	}

    gl_FragColor = vec4(color.rgb * u_color.rgb * texture2D(u_texture1, v_lightCookieUV).rgb, 0.0);

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
	fogFactor *= u_fogAmount;
	
	gl_FragColor.rgb    = mix(gl_FragColor.rgb, u_fogColor.rgb, fogFactor);
    gl_FragColor        = clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0));
	//gl_FragColor.rgb  = u_fogColor.rgb; // DEBUG SWITCH
}
