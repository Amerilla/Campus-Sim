Shader "Custom/example" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		Pass {
			CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            // Texture sampler
            sampler2D _MainTex;

            // Blur size parameter
            float _BlurSize;

            // Vertex input struct
            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            // Vertex output struct
            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            // Vertex shader
            v2f vert (appdata v) {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            // Fragment shader
            float4 frag (v2f i) : SV_Target {
                float4 color = float4(0, 0, 0, 0);

                // Sample the texture multiple times to apply blur
                for (int x = -_BlurSize; x <= _BlurSize; x++) {
                    for (int y = -_BlurSize; y <= _BlurSize; y++) {
                        float2 offset = float2(x, y) * _BlurSize;
                        color += tex2D(_MainTex, i.uv + offset);
                    }
                }

                // Average the sampled colors and output the final result
                color /= ((_BlurSize * 2) + 1) * ((_BlurSize * 2) + 1);
                return color;
            }
		
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0
			
		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
			
		ENDCG
		}
	}
	FallBack "Diffuse"
}