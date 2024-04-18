uniform sampler2D u_texture;

varying lowp vec4 v_color;
varying highp vec2 v_texcoord;

void main()
{
    gl_FragColor = v_color * texture2D( u_texture, v_texcoord );
}
