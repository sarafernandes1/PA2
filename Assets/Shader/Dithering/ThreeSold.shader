Shader "Hidden/ThreeSold"
{
   Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BG("Background", Color) = (0.14509805,0.15294118,0.0627451,0)
        _FG("Foreground", Color) = (0.913725551,1,1,1)
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

            float4 _BG;
            float4 _FG;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                col.rgb = lerp(_BG, _FG, round(col.r)).rgb;
                return col;
            }
            ENDCG
        }
    }
}
