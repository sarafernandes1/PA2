Shader "Custom/pitch_black"
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
       

        void surf (Input IN, inout SurfaceOutput o)
        {
            
            

           

        }
         ENDCG
    }
    FallBack "Diffuse"
}
