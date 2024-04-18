uniform sampler2D u_texture;

varying lowp vec4   v_color;
varying highp vec2  v_texcoord;

void main()
{
    gl_FragColor = v_color * texture2D(u_texture, v_texcoord);

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
	gl_FragColor        = clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0));
	//gl_FragColor.rgb = u_fogColor.rgb; // DEBUG SWITCH
}