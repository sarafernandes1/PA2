Shader "Custom/NewSurfaceShader"
{
    Properties
    {
        _Color("Cor", Color) = (0,0,0,1)
        _MainTex("Textura", 2D) = "white" {}
        _Smoothness("Suavidade", Range(0,1)) = 0.0
        _Metallic("Metalicidade", Range(0,1)) = 0.0

        _DistortTex("Distort Texture", 2D) = "white" {}
        _DistortIntensity("Distort Intensity", Range(0,10)) = 0

        _FresnelColor("Fresnel Color", Color) = (1,1,1,1)
        _FresnelIntensity("Fresnel Intensity", Range(0,10)) = 0
        _FresnelRamp("Fresnel Ramp", Range(0,10)) = 0

        _InvFresnelColor("Inverted Fresnel Color", Color) = (1,1,1,1)
        _InvFresnelIntensity("Inverted Fresnel Intensity", Range(0,10)) = 0
        _InvFresnelRamp("Inverted Fresnel Ramp", Range(0,10)) = 0

    }

    SubShader
    {
        Tags { "RenderType" = "Opaque"  "Queue" = "Geometry"}

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
        // UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        //UNITY_INSTANCING_BUFFER_END(Props)

        void surf(Input IN, inout SurfaceOutputStandard o)
        {
            // Read Albedo color from texture and apply tint
            fixed4 colour = tex2D(_MainTex, IN.uv_MainTex);
            colour *= _Color;
            o.Albedo = colour.rgb;
            //just apply the values for metalness, smoothness and emission
            o.Metallic = _Metallic;
            o.Smoothness = _Smoothness;
            //o.Alpha = c.a;
        }
        ENDCG


        Blend SrcAlpha One
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float3 normal : TEXCOORD1;
                float3 viewDir : TEXCOORD2;
            };

            sampler2D _MainTex;
            sampler2D _DistortTex;

            float4 _MainTex_ST;
            float4 _FresnelColor;
            float4 _InvFresnelColor;


            float _FresnelIntensity;
            float _FresnelRamp;
            float _DistortIntensity;
            float _InvFresnelRamp;
            float _InvFresnelIntensity;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normal = UnityObjectToWorldNormal(v.normal);
                o.viewDir = normalize(WorldSpaceViewDir(v.vertex));
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

           

            fixed4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                uv.y += _Time.x;

                float distort = tex2D(_DistortTex, uv).r;
                float3 finalNormal = i.normal;


                float fresnelAmount = 1 - max(0, dot(finalNormal, i.viewDir));
                fresnelAmount *= distort * _DistortIntensity;
                fresnelAmount = pow(fresnelAmount, _FresnelRamp) * _FresnelIntensity;
                float3 fresnelColor = fresnelAmount * _FresnelColor;
                
                float invfresnelAmount = max(0, dot(finalNormal, i.viewDir));
                invfresnelAmount *= distort * _DistortIntensity;
                invfresnelAmount = pow(invfresnelAmount, _InvFresnelRamp) * _InvFresnelIntensity;
                float3 invfresnelColor = invfresnelAmount * _InvFresnelColor;
                float3 finalColor = fresnelColor + invfresnelColor;
                return fixed4(finalColor, 1);
          
               
            }

           ENDCG
        }
    }
    FallBack "Diffuse"
}
