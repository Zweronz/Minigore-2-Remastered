uniform sampler2D u_texture;

varying highp vec2 v_texcoord;

varying lowp vec4 v_color;

void main()
{
	lowp float ra = 1.0;
	if (texture2D( u_texture, v_texcoord ).a <= v_color.a)
	{
		ra = 0.0;
	}
	gl_FragColor    = ra * vec4(v_color.rgb, 1.0);
	gl_FragColor    = clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0));
}

