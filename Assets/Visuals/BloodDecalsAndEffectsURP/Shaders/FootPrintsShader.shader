// Made with Amplify Shader Editor v1.9.1.5
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Custom/Footprints"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin]_MainTexture("MainTexture", 2D) = "white" {}
		_TrasparencyPower("TrasparencyPower", Range( 0.1 , 1)) = 0.6
		_MainColorTintFadeA("MainColorTint-Fade(A)", Color) = (0.9558824,0.9277682,0.9277682,1)
		_EmissionStrength("Emission Strength", Range(0, 5)) = 1.0
		_MainColor("Main Color", Color) = (1,1,1,1)
	}

	SubShader
	{
		Tags { "RenderType"="Transparent" "Queue"="Overlay" }
		LOD 100
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert(appdata_t v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTexture;
			float4 _EmissionColor;
			float _EmissionStrength;
			float4 _MainColor;

			fixed4 frag(v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTexture, i.uv) * _MainColor;
				fixed3 emission = _EmissionColor.rgb * _EmissionStrength;
				col.rgb += emission;
				return col;
			}
			ENDCG
		}
	}
}
