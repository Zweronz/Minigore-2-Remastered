attribute highp vec3    position;

uniform lowp vec4       u_color;
uniform highp float     u_aspectRatio;
uniform highp mat4      u_MVPMatrix;

varying highp vec2      v_irisInnerAndOuter;
varying highp vec2      v_screenPos;

void main()
{
	vec4 pos = u_MVPMatrix * vec4(position, 1.0);
	gl_Position = pos;
	pos.y *= u_aspectRatio;
	pos.y -= 0.075; // Focus on player.
	v_screenPos = pos.xy * 0.70710678118; // invSqrt2 = 0.70710678118.
	v_irisInnerAndOuter = vec2(max(u_color.r - 0.1, 0.0), u_color.r);
}
