Shader "FullScreen/FS_ChromaticAbberation"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_EffectT ("EffectA", Float) = 0
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
			float _EffectA;

			fixed4 frag (v2f i) : SV_Target
			{
				fixed a = abs(cos(_Time.w));
				fixed2 offset = fixed2(a * 0.01 * cos(_EffectA), a * 0.01 * sin(_EffectA));

				fixed4 col_c = tex2D(_MainTex, i.uv);
				fixed4 col_r = tex2D(_MainTex, i.uv + offset);
				fixed4 col_l = tex2D(_MainTex, i.uv - offset);
				fixed4 col = fixed4(col_c.r, col_r.g, col_l.b, col_c.a);
				return col;
			}
			ENDCG
		}
	}
}
