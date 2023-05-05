Shader "Unlit/OutlineShader"
{
    Properties
    {
        _OutlineColor("Outline Color", Color) = (0, 0, 0, 1)
        _OutlineThickness("Outline thickness", Float) = 0.1 
        _MainTex("Textura", 2D)="white"{}
    }

        SubShader
    {
        Tags
        {
            "RenderType" = "Opaque"
            "Queue" = "Geometry"
        }

        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {

                float4 positionCS : SV_POSITION;
                 float2 uv : TEXCOORD0;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            Varyings vert(Attributes i)
            {
                Varyings o = (Varyings)0;

                o.positionCS = UnityObjectToClipPos(i.positionOS);

                return o;
            }

            half4 frag(Varyings i) : SV_TARGET
            {
                float4 col=tex2D(_MainTex, i.uv);
                return col;
            }

            ENDCG
        }

        Pass
        {
            Cull Front

            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float3 smoothNormalOS : TEXCOORD1;
            };

            struct Varyings
            {
                float4 positionCS : SV_POSITION;
            };

            half4 _OutlineColor;
            float _OutlineThickness;

            Varyings vert(Attributes i)
            {
                Varyings o = (Varyings)0;

                float3 smoothNormal = UnityObjectToWorldNormal(i.smoothNormalOS);

                //float3 OutlineVariable = abs(sin(_OutlineThickness + _Time.y / 5)* 0.15) ;
                float3 OutlineVariable = ((_OutlineThickness + _Time.y)/50.0f) - 0.1f * floor(((_OutlineThickness + _Time.y)/50.0f) / 0.1f);

                float3 pos = mul(unity_ObjectToWorld, i.positionOS).xyz + OutlineVariable * smoothNormal;

                pos = mul(unity_WorldToObject, float4(pos, 1)).xyz;

                o.positionCS = UnityObjectToClipPos(pos);

                return o;
            }

            half4 frag(Varyings i) : SV_TARGET
            {
                return _OutlineColor;
            }

            ENDCG
        }
    }

}
