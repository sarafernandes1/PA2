Shader "Custom/LASER1CORCOPIA"
{
       Properties
    {
        _Textura("Textura", 2D)="defaulttexture"{}
        _Noise("Noise", 2D)="defaulttexture"{}

        _rimColor("Cor",Color)=(1,1,1,1)
        _ExpoenteControl ("Expoente", Range(-5,10))=1
        _color("Cor2", Color)=(1,1,1,1)
    }

    SubShader
    {
     //Cull off
        CGPROGRAM
            #pragma surface surf Lambert alpha


        struct Input {
		float2 viewDir;
        float2 uv_Textura;
        float2 uv_noise;
        };

        sampler2D _Textura;
        float3 _rimColor;
        float _ExpoenteControl;
        float3 _color;
        sampler2D _Noise;

		 #define mod(x,y) (x-y*floor(x/y))

        void surf(Input IN, inout SurfaceOutput o) {
		
       float2 uv = float2(IN.uv_Textura.y, IN.uv_Textura.x) ;
				float angle=3.14;
	            //get the colour
	            float xCol = (uv.x - (40.0*_Time / 2.0)) * 3.0;
	            xCol = mod(xCol, 4.0);
	             float3 horColour = float3(0.25, 0.25, 0.25);
	
	            if (xCol < 1.0) {
		            horColour.b += 1.0;
					horColour.r=0.1f;

	            }
	             if (xCol < 2.0) {
		            horColour.b += 1.0f-0.6f;
					horColour.r=0.4f - 0.2f;
					
	            }
	            else if(xCol<3.0){
		            horColour.b += 1.0f - 0.4f;
					horColour.r=0.4f-0.1f ;
					o.Emission= float3(0.2f,0.2f,0.2f);
	            }
				  else {
		            horColour.b += 0.5f;
					horColour.r=0.3f ;

	            }

	            ////background lines
	            float backValue = 1.0;
	            float aspect = uv.x /uv.y;
	            if (mod(uv.y * 100.0, 1.0) > 0.75 || mod(uv.x * 100.0 * aspect, 1.0) > 0.75) {
		
		            backValue = 1.0;	
	            }
	
	            float3 backLines  = float3(backValue, backValue, backValue) ;
	
	            //main beam
	            uv = (1.0 * uv) - 1.0;
	            float beamWidth = abs(1.0 / (20.0 * uv.y));
	            float3 horBeam = float3(beamWidth,beamWidth,beamWidth);

	            o.Albedo += float4(((backLines * horBeam) * horColour), 1.0)*20.0f ;

				 half dotp= dot(normalize(IN.viewDir), o.Normal);
				

			 o.Alpha = 1;
			if(dotp>0 && dotp<0.7f){
				o.Alpha = 0.5f;
				o.Albedo = horColour;
				}
        }
		
        

        ENDCG
    }
        FallBack "Diffuse"
}
  