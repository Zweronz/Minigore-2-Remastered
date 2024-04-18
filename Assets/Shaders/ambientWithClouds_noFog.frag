uniform sampler2D	u_texture;
uniform sampler2D	u_texture1;

varying highp vec2	v_texcoord;
varying highp vec2	v_lightCookieUV;

void main()
{
    gl_FragColor        = vec4(1.9 * texture2D(u_texture, v_texcoord).rgb * texture2D(u_texture1, v_lightCookieUV).rgb, 1.0);
    gl_FragColor        = clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0));
}