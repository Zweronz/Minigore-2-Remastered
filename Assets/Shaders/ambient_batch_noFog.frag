// Low settings - No Fog.

uniform sampler2D       u_texture;

varying lowp vec4       v_color;
varying highp vec2      v_texcoord;

void main()
{
    gl_FragColor	= vec4(0.0, 0.0, 0.0, 0.0);
    gl_FragColor 	= v_color * texture2D(u_texture, v_texcoord) * 1.4;
	gl_FragColor	= clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0));
}
