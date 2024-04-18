varying highp vec2 v_irisInnerAndOuter;
varying highp vec2 v_screenPos;

void main()
{
	highp float dst = length(v_screenPos);
	gl_FragColor    = vec4(0.0, 0.0, 0.0, smoothstep(v_irisInnerAndOuter.x, v_irisInnerAndOuter.y, dst));
	gl_FragColor    = clamp(gl_FragColor, vec4(0.0, 0.0, 0.0, 0.0), vec4(1.0, 1.0, 1.0, 1.0));
}