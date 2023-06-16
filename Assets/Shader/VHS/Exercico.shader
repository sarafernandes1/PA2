Shader "Hidden/Exercico"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DistorTexture("Texture", 2D) = "white" {}

        _GradientMap("Gradiante", 2D)="white"{}
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
            sampler2D _GradientMap;

            fixed4 frag(v2f i) : SV_Target
            {
                
                float2 novoUV = i.uv;

               if(i.uv.y>0.5f) novoUV.y -= 0.5f*(cos(_Time*2.0f + 2.0f).y*(-0.08f)) * (novoUV.x - 0.5f);
                else novoUV.y -= 0.5*(cos(_Time*2.0f + 2.0f).y*(-0.1f)) * (novoUV.x - 0.5f);

                if(i.uv.y>0.5f) novoUV.x += -0.5f*((sin(_Time*20.0f +2.0f).x) * 0.2f)* (novoUV.y-0.5f) ;
                else novoUV.x -= -0.5f*((cos(_Time*20.0f + 2.0f)) * 0.2f)* (novoUV.y-0.5f)  ;
               
                fixed4 col = tex2D(_MainTex, novoUV);
                
                fixed4 disTex = tex2D(_DistorTexture,novoUV);

                float grayScale=(col.r+col.g+col.b)/3;
                fixed4 tex= tex2D(_GradientMap, grayScale);
                
                return tex + disTex ;
            }
            ENDCG
        }
    }
}
