Shader "Custom/ScanLineShader" {
	Properties {
		//调整背景
//		_BackgroundColor ("BackgroundColor", Color) = (0, 0, 0, 0)
//		_BackgroundColor2 ("BackgroundColor2", Color) = (0, 0, 0, 1)
//		_Space ("Space", Range(0, 1)) = 0.2
//		_XOffset ("XOffset", Range(-1, 1)) = 0.15
//		_YOffset ("YOffset", Range(-1, 1)) = 0.05

		//调整曲线的波动
		_Frequency ("Frequency", Range(0, 100)) = 10//频率
		_Amplitude ("Amplitude", Range(0, 1)) = 0.1//振幅
		_Speed ("Speed", Range(0, 100)) = 10//速度
	}
	SubShader {
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
			};

			//格子背景
			fixed4 _BackgroundColor;
//			fixed4 _BackgroundColor2;
//			fixed _Space;
//			fixed _XOffset;
//			fixed _YOffset;

			half _Frequency;
			half _Amplitude;
			half _Speed;

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.uv;

				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				//fmod(x, y)：x/y的余数，和x有同样的符号	
				//step(a, x)：如果x<a，返回0；如果x>=a，返回1

				//得到一个小于_Space的余数，即a的范围为[0, _Space)
//				fixed a = fmod(i.uv.x + _XOffset, _Space);
				//有1/2概率返回0，有1/2概率返回1，从而形成间隔效果
//				a = step(0.5 * _Space, a);

//				fixed b = fmod(i.uv.y + _YOffset, _Space);
//				b = step(0.5 * _Space, b);

//				fixed4 bgCol = _BackgroundColor * a * b + _BackgroundColor2 * (1 - a * b);
				

				//范围(1, 51)，乘上100是扩大差距(中间最亮其他两边基本不亮)，加上1是防止0作为除数，同时确保最中间最亮
				float y = i.uv.y + sin(_Time.y);// 扫描线效果
//				float y = i.uv.y + sin(i.uv.x * _Frequency + _Time.y * _Speed) * _Amplitude;//可以看成一条y的关于x的方程式
				float v = abs(y - 0.5) * 100 + 1;
				v = 1 / v;
				fixed4 lineCol = fixed4(v, v, v, 0);

				return lineCol;
//				return bgCol + lineCol;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}