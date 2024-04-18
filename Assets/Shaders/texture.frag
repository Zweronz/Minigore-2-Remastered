// Used in vignette.

uniform sampler2D u_texture;

uniform lowp vec4 u_color;

varying highp vec2 v_texcoord;

void main()
{
    gl_FragColor		= texture2D(u_texture, v_texcoord);
	gl_FragColor        = clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0));
}