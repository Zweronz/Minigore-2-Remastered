uniform sampler2D u_texture;
uniform lowp float u_alphaRef;

varying lowp vec4 v_color;
varying highp vec2 v_texcoord;

void main()
{
	lowp vec4 tex   = texture2D(u_texture, v_texcoord);
    gl_FragColor    = vec4(0.0, 0.0, 0.0, 0.0);

	if (tex.a < u_alphaRef)
	{
		discard;
	}

    gl_FragColor = clamp(v_color * tex, vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0));
}
