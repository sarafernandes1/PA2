Shader "Custom/sky_shader"
{
    Properties
    {
        
        _MainTex ("Texture", 2D) = "white" {}
       
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

  
        CGPROGRAM
        
        #pragma surface surf Lambert 

        struct Input
        {
            float2 uv_MainTex;
        };


       sampler2D _MainTex;
       
       float2 Rotate(float2 ponto, float deg)
        {
        
        float2x2 matRotacao = {cos(deg), -sin(deg), sin(deg), cos(deg)};

        return mul(matRotacao, ponto);

        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            
            float2 rotations = Rotate(IN.uv_MainTex,(tan(_Time)+1000));
            float3 Tex = tex2D(_MainTex, rotations);
            o.Albedo = Tex;
            o.Alpha=1;

        }
        ENDCG
    }
    FallBack "Diffuse"
}
