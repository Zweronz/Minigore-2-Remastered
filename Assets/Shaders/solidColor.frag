uniform lowp vec3	u_fogColor;
uniform lowp vec4	u_color;

void main()
{
	highp float fogFactor   = 0.4;
	highp float dist        = 0.0;
	highp float fogDensity  = 0.0025;

	dist            = gl_FragCoord.z / gl_FragCoord.w;
	
	// FOG EXP2
//	fogFactor = 1.0 /exp( (dist * fogDensity)* (dist * fogDensity));
	// FOG LINEAR
	fogFactor = (700.0 - dist) / (700.0 - 1.0);
	fogFactor = clamp(fogFactor, 0.0, 1.0);	
	
    gl_FragColor		= u_color;
	gl_FragColor.rgb	= mix(u_fogColor, gl_FragColor.rgb, fogFactor);	
	gl_FragColor		= vec4(gl_FragColor.rgb, 1.0);
}