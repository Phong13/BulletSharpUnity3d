Shader "LineRenderFix" {
	Subshader{
		BindChannels{
		Bind "vertex", vertex
		Bind "color", color
	}
		Pass{}
	}
}