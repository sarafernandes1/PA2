Shader "Unlit/NightVisionShader"
{
     Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100



        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            #include "UnityCG.cginc"

            fixed4 _ScreenTint;
            sampler2D _MainTex;
            float _LuminosityMP;
            float _LuminosityIntensity;
            float _NightVisionEntensity;

           

            fixed4 frag (v2f i) : SV_Target
            {
                
                fixed4 col = tex2D(_MainTex, i.uv);
                float luminosity = saturate(lerp(_LuminosityMP, Luminance(col.rgb), _LuminosityIntensity));
                col.rgb = _ScreenTint.rgb  * luminosity *  _NightVisionEntensity;
                return col;
            }
                ENDCG
        }

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

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

             sampler2D _MainTex;
             float4 _MainTex_TexelSize;

          
            fixed4 frag(v2f i) : SV_Target
            {
              
                fixed4 col = tex2D(_MainTex, i.uv);
                float scale = 1;
                float3 blurSample =
                    tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(-scale, -scale)) +  //Baixo, esquerda
                    tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(-scale, scale)) +   //Baixo, direita
                    tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(scale, -scale)) +   //Cima, esquerda
                    tex2D(_MainTex, i.uv + _MainTex_TexelSize.xy * float2(scale, scale));     //Cima, direita

                return fixed4(blurSample * 0.25, 1);
            }

            ENDCG

        }
    }
}
