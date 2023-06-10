Shader "Custom/TVLobby"
{
    Properties
    {
        
        _mainTex1 ("Texture 1", 2D) = "white" {}
		_mainTex2 ("Texture 2", 2D) = "white" {}
        
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        
        #pragma surface surf Lambert 

        struct Input
        {
            float2 uv_mainTex1;
			float2 uv_mainTex2;
        };

        sampler2D _mainTex1;
		sampler2D _mainTex2;

       float2 Rotate(float2 ponto, float deg)
        {

        float2x2 matRotacao = {cos(deg), -sin(deg), sin(deg), cos(deg)};

        return mul(matRotacao, ponto);

        }

        void surf (Input IN, inout SurfaceOutput o)
        {
            float2 uv =	IN.uv_mainTex1+cos(_Time+100);	//tex2D(_mainTex1,IN.uv_mainTex1);
			float2 block = floor(uv.xy / float2(16,16));
			float2 uv_noise = block / float2(64,64);
			uv_noise += floor(float2(_Time.x,_Time.y)*float2(1234.0, 3543.0)) / float2(64,64);
	
			float block_thresh = pow(frac(_Time * 10.0), 2.0) * 0.2;
			float line_thresh = pow(frac(_Time * 20.0), 3.0) * 0.7;
	
			float2 uv_r = uv, uv_g = uv, uv_b = uv;

			// glitch some blocks and lines
			if (tex2D(_mainTex2, uv_noise).r < block_thresh ||
			tex2D(_mainTex2, float2(uv_noise.y, 0.0)).g < line_thresh) {
	
			float2 dist = (frac(uv_noise) - 0.5) * 0.3;
			uv_r += dist * 0.1;
			uv_g += dist * 0.2;
			uv_b += dist * 0.125;
            }

            fixed4 fragColor;

            fragColor.r = tex2D(_mainTex1, uv_r).r;
		    fragColor.g = tex2D(_mainTex1, uv_g).g;
		    fragColor.b = tex2D(_mainTex1, uv_b).b;


			//float3 Tex1 = tex2D(_mainTex1, IN.uv_mainTex1).rgb;
			float3 Tex1 = tex2D(_mainTex1, IN.uv_mainTex1+cos(_Time+100)).rgb;
           o.Albedo = Tex1 + fragColor.rgb; 
			
        }
        ENDCG
    }
    FallBack "Diffuse"
}
