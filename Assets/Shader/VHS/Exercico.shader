Shader "Hidden/Exercico"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Slider1("Slider1",Range(-1,1)) = 0
        _Slider2("Slider2",Range(-1,1)) = 0
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
            float _Slider1;
            float _Slider2;
            sampler2D _GradientMap;

            fixed4 frag(v2f i) : SV_Target
            {
                
                float2 novoUV = i.uv;

                  if(i.uv.y>0.5f) novoUV.y -= 0.5f*(cos(_Time*2.0f + 2.0f).y*(-0.08f)) * (novoUV.x - 0.5f);
               else novoUV.y -= 0.5*(cos(_Time*2.0f + 2.0f).y*(-0.1f)) * (novoUV.x - 0.5f);

               if(i.uv.y>0.5f) novoUV.x += -0.5f*((sin(_Time*20.0f +2.0f).x) * 0.2f)* (novoUV.y-0.5f) ;
              else novoUV.x -= -0.5f*((cos(_Time*20.0f + 2.0f)) * 0.2f)* (novoUV.y-0.5f)  ;
               
              
              // if(i.uv.y>0.5f) novoUV.y -= (cos(_Time*2.0f).y*(-0.08f)) * (novoUV.x - 0.5f);
              // else novoUV.y -= (cos(_Time*2.0f).y*(-0.1f)) * (novoUV.x - 0.5f);

              // if(i.uv.y>0.5f) novoUV.x += -((sin(_Time*20.0f).x) * 0.2f)* (novoUV.y-0.5f) ;
              //else novoUV.x += -((cos(_Time*20.0f)) * 0.2f)* (novoUV.y-0.5f)  ;
               
              //novoUV.x += ((sin(_Time*20.0f).x) * (-0.1f))* (novoUV.y-0.5f);
               
               

               // novoUV.y += (cos(_Time).y*(0.05f)) * (novoUV.x - 0.5f) ;
               //novoUV.x -= abs(tan(sin(_Time*20.0f)).x*0.05f)* (novoUV.x-0.5f) ;

               // novoUV.y += (cos(_Time).y*(0.05f)) * (novoUV.x - 0.5f) ;
               //novoUV.x -= (sin(_Time).x*(10.0f)) * (novoUV.y - 0.5f);

                // novoUV.y *= cos(_Time*10.0f).y;
                //novoUV.x = sin(_Time*10.0f).y ;

                //  novoUV.y /= cos(_Time*10.0f).x;
                //novoUV.x = sin(_Time).y;

                //novoUV.y += cos(_Time).y * (novoUV.y - 0.5f) ;
                //novoUV.x += sin(_Time).y;
                // just invert the colors
                fixed4 col = tex2D(_MainTex, novoUV);
                
                
                
                   fixed4 disTex = tex2D(_DistorTexture,novoUV);

                   float grayScale=(col.r+col.g+col.b)/3;
                   fixed4 tex= tex2D(_GradientMap, grayScale);
                
                return tex + disTex;
            }
            ENDCG
        }
    }
}
