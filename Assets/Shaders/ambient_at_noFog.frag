uniform sampler2D   u_texture;
uniform sampler2D   u_texture1;

uniform lowp float  u_alphaRef;
uniform lowp vec4   u_color;

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

    gl_FragColor	= vec4(color.rgb * u_color.rgb * texture2D(u_texture1, v_lightCookieUV).rgb, 0.0);
	
	// If alpha channel isn't set to 1.0 the object will show as black on Samsung Galaxy S3 Mini.
    gl_FragColor	= clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 1.0), vec4(1.0, 1.0, 1.0, 1.0));
}
