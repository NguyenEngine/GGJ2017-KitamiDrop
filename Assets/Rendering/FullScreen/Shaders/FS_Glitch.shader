Shader "FullScreen/FS_Glitch"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			float rand(float3 co)
			{
			    return frac(sin( dot(co.xyz ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
			}

			fixed4 frag (v2f i) : SV_Target
			{
				float a = ceil(_Time.w * 4);
				fixed du = rand(fixed3(ceil(i.uv.x * 20), ceil(i.uv.y * 20), a));
				fixed dv = rand(fixed3(ceil(i.uv.y * 20), ceil(i.uv.x * 20), a));
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 col2 = tex2D(_MainTex, fixed2((i.uv.x + du + 1) % 1, (i.uv.y + dv + 1) % 1));
				return lerp(col, col2, clamp(ceil(du - 0.75), 0, 1));
			}
			ENDCG
		}
	}
}
