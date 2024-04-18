uniform sampler2D   u_texture;

varying lowp vec4   v_color;
varying highp vec2  v_texcoord;

void main()
{
    gl_FragColor = vec4(clamp(v_color.rgb * texture2D(u_texture, v_texcoord).rgb, vec3(0.0, 0.0, 0.0), vec3(1.0, 1.0, 1.0)), 1.0);
}
