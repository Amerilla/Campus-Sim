Shader "Custom/Blur" {
    Properties {
        _BlurSize ("Blur Size", Range(0, 10)) = 5
        _MainTex ("Texture", 2D) = "white" {}
    }

    SubShader {
        Tags { "RenderType"="Opaque" }

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
                        float2 offset = float2(x, y) * _PixelSize * _BlurSize;
                        color += tex2D(_MainTex, i.uv + offset);
                    }
                }

                // Average the sampled colors and output the final result
                color /= ((_BlurSize * 2) + 1) * ((_BlurSize * 2) + 1);
                return color;
            }
            ENDCG
        }
    }
}