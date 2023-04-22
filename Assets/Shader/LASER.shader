Shader "Unlit/LASER"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            #define mod(x,y) (x-y*floor(x/y))
			
            fixed4 frag (v2f i) : SV_Target
            {
            
            float2 uv = float2(i.uv.x , i.uv.y) ;

	            //get the colour
	            float xCol = (uv.x - (100.0*_Time / 4.0)) * 3.0;
	            xCol = mod(xCol, 3.0);
	            float3 horColour = float3(0.25, 0.25, 0.25);
	
	            if (xCol < 1.0) {
		
		            horColour.r += 1.0 - xCol;
		            horColour.g += xCol;
	            }
	            else if (xCol < 2.0) {
		
		            xCol -= 1.0;
		            horColour.g += 1.0 - xCol;
		            horColour.b += xCol;
	            }
	            else {
		
		            xCol -= 2.0;
		            horColour.b += 1.0 - xCol;
		            horColour.r += xCol;
	            }
	
	            ////background lines
	            float backValue = 1.0;
	            float aspect = i.uv.x / i.uv.y;
	            if (mod(uv.y * 100.0, 1.0) > 0.75 || mod(uv.x * 100.0 * aspect, 1.0) > 0.75) {
		
		            backValue = 1.0;	
	            }
	
	            float3 backLines  = float3(backValue, backValue, backValue) ;
	
	            //main beam
	            uv = (1.0 * uv) - 1.0;
	            float beamWidth = abs(1.0 / (30.0 * uv.y));
	            float3 horBeam = float3(beamWidth,beamWidth,beamWidth);
	
	            return float4(((backLines * horBeam) * horColour), 1.0)*20.0f;
            }
            ENDCG
        }
    }
}

//float2 uv = float2(i.uv.x , i.uv.y) ;

	            ////get the colour
	            //float xCol = (uv.x - (20.0*_Time / 8.0)) * 3.0;
	            //xCol = mod(xCol, 3.0);
	            //float3 horColour = float3(0.25, 0.25, 0.25);
	
	            //if (xCol < 1.0) {
		
		           // horColour.r += 1.0 - xCol;
		           // horColour.g += xCol;
	            //}
	            //else if (xCol < 2.0) {
		
		           // xCol -= 1.0;
		           // horColour.g += 1.0 - xCol;
		           // horColour.b += xCol;
	            //}
	            //else {
		
		           // xCol -= 2.0;
		           // horColour.b += 1.0 - xCol;
		           // horColour.r += xCol;
	            //}
	
	            //////background lines
	            //float backValue = 1.0;
	            //float aspect = i.uv.x / i.uv.y;
	            //if (mod(uv.y * 100.0, 1.0) > 0.75 || mod(uv.x * 100.0 * aspect, 1.0) > 0.75) {
		
		           // backValue = 1.15;	
	            //}
	
	            //float3 backLines  = float3(backValue, backValue, backValue);
	
	            ////main beam
	            //uv = (2.0 * uv) - 1.0;
	            //float beamWidth = abs(1.0 / (30.0 * uv.y));
	            //float3 horBeam = float3(beamWidth,beamWidth,beamWidth);
	
	            //return float4(((backLines * horBeam) * horColour), 1.0);