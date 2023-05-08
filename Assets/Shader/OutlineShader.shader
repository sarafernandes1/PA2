Shader "Custom/OutlineSurfaceShader"
{
    Properties
    {
        _Color ("Cor", Color) = (0,0,0,1)
        _MainTex ("Textura", 2D) = "white" {}
        _Smoothness ("Suavidade", Range(0,1)) = 0.0
        _Metallic ("Metalicidade", Range(0,1)) = 0.0

        _OutlineColor("Cor do Outline", Color) = (1,1,1,1)
        _OutlineThickness("Espessura do Outline", Range(0,1)) = 0.1
        _Expessura("Espessura", Range(0.1,2)) = 0.1
        _Frequencia("Frequencia", Range(20.0,1000.0)) = 50.0


    }
    SubShader
    {
        Tags { "RenderType"="Opaque"  "Queue" = "Geometry"}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;

        half _Smoothness;
        half _Metallic;
        

        struct Input
        {
            float2 uv_MainTex;
        };


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        // UNITY_INSTANCING_BUFFER_START(Props)**
            // put more per-instance properties here
        //UNITY_INSTANCING_BUFFER_END(Props)**

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Read Albedo color from texture and apply tint
            fixed4 colour = tex2D (_MainTex, IN.uv_MainTex);
            colour *= _Color;
            o.Albedo = colour.rgb;
            //just apply the values for metalness, smoothness and emission
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            //o.Alpha = c.a;
        }
        ENDCG

        //The second pass where we render the outlines
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
            float _Expessura;
            float _Frequencia;

            Varyings vert(Attributes i)
            {
                Varyings o = (Varyings)0;

                float3 smoothNormal = UnityObjectToWorldNormal(i.smoothNormalOS);

                //float3 OutlineVariable = abs(sin(_OutlineThickness + _Time.y / 5)* 0.15) ;
                float3 OutlineVariable = ((_OutlineThickness + _Time.y) / _Frequencia) -  _Expessura* floor(((_OutlineThickness + _Time.y) / _Frequencia) / _Expessura);

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
    FallBack "Standard"
}
