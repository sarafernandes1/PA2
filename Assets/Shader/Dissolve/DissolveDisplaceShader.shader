Shader "Custom/DissolveDisplaceShader"
{
    Properties
    {
        [Header(Main Properties)]
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _BumpMap("Normal", 2D) = "bump" {}
        _NormalStrength("Normal Strength", Float) = 1

        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

        //Dissolve properties
         [Header(Dissolve)]
        _DissolveTex("Dissolve Texture", 2D) = "white" {}
        _DissolveAmount("Dissolve Amount", Range(0,1)) = 0.5
        _DissolveScale("Dissolve Scale", Float) = 1
        _DissolveLine("Dissolve Line", Range(0,0.2)) = 0.1
        _DissolveLineColor("Dissolve Line Color", Color) = (1,1,1,1)

        [Header(Vertex)]
        _VertexAmount("Extrusion Amount", Range(-1,1)) = 0
        _VertexTexture("Distortion Texture", 2D) = "white" {}

        [Toggle]
        _Ativo("Ativar/Desativar", float) = 0

        _Tempo("Tempo", float) = 0


    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        sampler2D _DissolveTex;
        sampler2D _BumpMap;
        sampler2D _VertexTexture;


        struct Input
        {
            float2 uv_MainTex;
            
        };


        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        float _NormalStrength;

        half _DissolveAmount;
        half _DissolveScale;
        half _DissolveLine;
        half3 _DissolveLineColor;

        float _VertexAmount;
        float _Ativo;
        float _Tempo;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        //struct appdata 
        //{
        //    float4 vertex : POSITION;
        //    float2 uv : TEXCOORD0;
        //};
        //
        //struct Vertices 
        //{
        //    float2 uv : TEXCOORD0;
        //    float4 vertex : SV_POSITION;
        //};
        //
        //Vertices vert(inout appdata_full v)
        //{
        //    Vertices o;
        //    o.uv = v.uv;
        //    float xMod = tex2D(_VertexTexture, float4(o.uv.xy, 0, 1));
        //    xMod = xMod * 2 - 1;
        //  
        //    o.uv.x = sin(xMod * 10 - _Time.y);
        //    float3 vert = v.vertex;
        //    vert.z = o.uv.x * 10;
        //    o.uv.x = o.uv.x * 0.5 + 0.5;
        //    o.vertex = UnityObjectToClipPos(vert);
        //    return o;
        //}

        void vert(inout appdata_full v)
        {
           // v.vertex.xyz += v.normal.y * sin(_Time.y) * _VertexAmount;
           
           v.vertex.x = sin(length(v.vertex.xy) * 10 + _Time.y);
        }

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            half4 noise = tex2D(_DissolveTex, IN.uv_MainTex * _DissolveScale);
           if(_Tempo>=0) clip( _Tempo - IN.uv_MainTex );

            //Normal map output
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.uv_MainTex));
            o.Normal.z *= _NormalStrength;

            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
           o.Albedo =  c;
        

            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;

            o.Emission += step(noise.r, _DissolveAmount + _DissolveLine) * _DissolveLineColor;
            
        }
        ENDCG
    }
    FallBack "Diffuse"
}
