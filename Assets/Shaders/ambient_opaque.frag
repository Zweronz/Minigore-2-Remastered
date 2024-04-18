uniform sampler2D u_texture;

uniform lowp vec4 u_color;

varying highp vec2 v_texcoord;

void main()
{
    gl_FragColor = vec4(u_color.rgb * texture2D( u_texture, v_texcoord ).rgb, 1.0);
}