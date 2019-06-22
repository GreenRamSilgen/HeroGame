// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "ProtagShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_DashColor("Dash Color", Color) = (0.5, 0.8, 0.2, 1)
		_ColorStrength1("Color Strength 1", float) = 0.3
		_ColorStrength2("Color Strength 2", float) = 0.4
		_ColorStrength3("Color Strength 3", float) = 0.9
	}

	SubShader
	{
		ZTest Always
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha 

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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float _DashDiffX[3];
			float _DashDiffY[3];

			v2f vert(appdata v)
			{
				v2f o;
				
				v.vertex.x -= _DashDiffX[2];
				v.vertex.y -= _DashDiffY[2];
				
				o.vertex = UnityObjectToClipPos(v.vertex);

				o.uv = v.uv;

				return o;
			}

			sampler2D _MainTex;
			float4 _DashColor;
			float _ColorStrength3;

			float4 frag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);
				float4 green = float4 (color.x + _ColorStrength3 * (_DashColor.x - color.x), color.y + _ColorStrength3 * (_DashColor.y - color.y), color.z + _ColorStrength3 * (_DashColor.z - color.z), color.a * (1 - _ColorStrength3));

				return green;
			}
			ENDCG
		}
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha

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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float _DashDiffX[3];
			float _DashDiffY[3];

			v2f vert(appdata v)
			{
				v2f o;

				v.vertex.x -= _DashDiffX[1];
				v.vertex.y -= _DashDiffY[1];

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _DashColor;
			float _ColorStrength2;

			float4 frag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);
				float4 green = float4 (color.x + _ColorStrength2 * (_DashColor.x - color.x), color.y + _ColorStrength2 * (_DashColor.y - color.y), color.z + _ColorStrength2 * (_DashColor.z - color.z), color.a * (1 - _ColorStrength2));

				return green;
			}
			ENDCG
		}
		Pass{
			Blend SrcAlpha OneMinusSrcAlpha

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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			float _DashDiffX[3];
			float _DashDiffY[3];

			v2f vert(appdata v)
			{
				v2f o;

				v.vertex.x -= _DashDiffX[0];
				v.vertex.y -= _DashDiffY[0];

				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			float4 _DashColor;
			float _ColorStrength1;

			float4 frag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);
				float4 green = float4 (color.x + _ColorStrength1 * (_DashColor.x - color.x), color.y + _ColorStrength1 * (_DashColor.y - color.y), color.z + _ColorStrength1 * (_DashColor.z - color.z), color.a * (1 - _ColorStrength1));

				return green;
			}
			ENDCG
		}
		Pass{
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;

			float4 frag(v2f i) : SV_Target
			{
				float4 color = tex2D(_MainTex, i.uv);

				return color;
			}
			ENDCG
		}
		
	}
	Fallback "Diffuse"
}
