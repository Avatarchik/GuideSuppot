Shader "Hidden/TopdownShader" {
	Properties{
		_MainTex("Base (RGB)", 2D) = "" {}
	}

	CGINCLUDE

	#pragma target 3.0
	#include "UnityCG.cginc"

	struct v2f {
		float4 pos : POSITION;
		float2 uv : TEXCOORD0;
	};

	sampler2D _MainTex;

	float3 _forward;
	float3 _right;
	float3 _up;
	float _fov;

	half4 frag(v2f i) : COLOR
	{
		float2 huv = (i.uv - 0.5) * 2;
		if (sqrt(huv.x*huv.x + huv.y*huv.y) > 1.0) {
			discard;
		}
		float2 pl;
		pl.y = sqrt(huv.x*huv.x + huv.y*huv.y) * 2 + 1;
		pl.x = atan2(huv.y,huv.x) / 3.14159265;
		huv = pl;
		i.uv = (huv + 1)*0.5;
		i.uv.x = clamp(i.uv.x,0.0,1);
		i.uv.y = clamp(i.uv.y,0,1.99);

		// sample the texture
		fixed4 col = tex2D(_MainTex, i.uv);
		color.a = 1.0;

		return col;
	}

	ENDCG

	Subshader {
		Pass{
			Tags{ "LightMode" = "Always" "Queue" = "AlphaTest" "IgnoreProjector" = "True" "RenderType" = "TransparentCutout" }
			ZTest Always Cull Off ZWrite Off
			Fog{ Mode off }

			CGPROGRAM
			#pragma fragmentoption ARB_precision_hint_fastest 
			#pragma vertex vert_img
			#pragma fragment frag
			ENDCG
		}

	}

	Fallback off

} // shader
