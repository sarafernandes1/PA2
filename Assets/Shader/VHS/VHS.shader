Shader "Hidden/VHS"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistorTexture("Texture", 2D) = "white" {}
         _DistorTexture1("Texture", 2D) = "white" {}
         _DistorTexture2("Texture", 2D) = "white" {}
         _Record("Record", 2D)="white"{}
           _Colunas("Colunas", Range(0,200))=1
        _Linhas("Linhas", Range(0,200))=1
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
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _DistorTexture;
            sampler2D _DistorTexture1;
            sampler2D _DistorTexture2;

            sampler2D _Record;
             float _Colunas;
            float _Linhas;

            static float range = 0.05;
            static float noiseQuality = 250.0;
            static float noiseIntensity = 0.0088;
            static float offsetIntensity = 0.02;
            static float colorOffsetIntensity = 1.3;

            #define mod(x,y) (x-y*floor(x/y))

            float rand(float2 co)
            {
                return frac(sin(dot(co.xy ,float2(12.9898,78.233))) * 43758.5453);
            }

            float verticalBar(float pos, float uvY, float offset)
            {
                float edge0 = (pos - range);
                float edge1 = (pos + range);

                float x = smoothstep(edge0, pos, uvY) * offset;
                x -= smoothstep(pos, edge1, uvY) * offset;
                return x;
            }


            fixed4 frag (v2f i) : SV_Target
            {

            // Pixel
               float2 uv = i.uv;
               float2 UVS = uv;
               uv.x*= _Colunas;
               uv.y*= _Linhas;
               uv.x=round(uv.x);
               uv.y=round(uv.y);
               uv.x/=_Colunas;
               uv.y/=_Linhas;

                float2 novoUV = uv;
    
                // VHS
                for (float i = 0.0; i < 0.71; i += 0.1313)
                {
                    float d = mod(_Time * i, 1.7);
                    float o = sin(1.0 - tan(_Time * 0.24 * i));
    	            o *= offsetIntensity * 0.5f;
                    uv.x += verticalBar(d, uv.y, o);
                }
    
                float uvY = uv.y;
                uvY *= noiseQuality;
                uvY = float(int(uvY)) * (1.0 / noiseQuality);
                float noise = (_Time * 0.00001, uvY);
                uv.x += noise * noiseIntensity;

                uv += + 0.006 * sin(_Time);
                float r = tex2D(_MainTex, uv ).r;
                uv+=0.0073 * (cos(_Time * 0.97));
                float g = tex2D(_MainTex, uv).g;
                float b = tex2D(_MainTex, uv).b;

                float4 tex = float4(r, g, b, 1.0);


                // Efeito TV antiga
                novoUV.y += cos(_Time*1.0f).y;
                novoUV.x = sin(_Time).y;

                fixed4 disTex = tex2D(_DistorTexture,novoUV)*0.3f;

                // Gradiente
                float grayScale=(tex.r+tex.g+tex.b)/3;
                fixed4 tex2= tex2D(_DistorTexture1, grayScale)*0.2f;

                fixed4 vhsCanvas = tex2D(_Record, uv);

                // Distorção
              UVS.y += sin(_Time*1.0f).y;
              
                fixed4 tex3= tex2D(_DistorTexture2, UVS/2 )*0.2f;

                return tex + disTex  + vhsCanvas + tex2 + tex3;
            }
            ENDCG
        }
    }
}
