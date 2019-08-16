Shader "FullScreen/FS_Ripple"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_EffectX ("EffectX", Float) = 0
		_EffectY ("EffectY", Float) = 0
		_EffectT ("EffectT", Float) = 0
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			float4 _MainTex_TexelSize;
			float _EffectX;
			float _EffectY;
			float _EffectT;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed2 uva = i.uv;
#if UNITY_UV_STARTS_AT_TOP
				if (_MainTex_TexelSize.y < 0)
					uva.y = 1 - uva.y;
#endif

				float a = 5.0f * (_Time.y - _EffectT);

				// Compute screen coordinates
				fixed2 screenPos = uva * _ScreenParams.xy;
				fixed2 centerPos = fixed2(_EffectX, _EffectY);
				fixed2 dist = centerPos - screenPos;

				// Compute band based on screen size
				fixed width = 0.5 * min(_ScreenParams.x, _ScreenParams.y);
				fixed innerBand = width * a * 0.7;
				fixed outerBand = innerBand + 10;

				// Compute whether inside
				fixed radiusSq = dist.x * dist.x + dist.y * dist.y;
				fixed isInside = clamp(ceil(outerBand * outerBand - radiusSq), 0, 1);
				fixed isOutside = clamp(ceil(radiusSq - innerBand * innerBand), 0, 1);

				fixed angle = atan2(dist.y, dist.x);
				fixed2 uv = lerp(i.uv, i.uv + 0.005 * fixed2(cos(angle), sin(angle)), isInside * isOutside);
				fixed4 col = tex2D(_MainTex, uv);
				col = lerp(col, 1.1 * col, isInside * isOutside);
				return col;
			}
			ENDCG
		}
	}
}
