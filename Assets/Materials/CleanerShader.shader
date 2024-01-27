Shader "Custom/CleanerShader"
{
    Properties
    {
        _MainTex("Main Texture", 2D) = "white" {}
        _DirtTex("Dirt Texture", 2D) = "white" {}
        _CleanTex("Clean Texture", 2D) = "white" {}
    }
    SubShader
    {
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

            sampler2D _MainTex;
            sampler2D _DirtTex;
            sampler2D _CleanTex;
            float4 _MainTex_ST;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 mainColor = tex2D(_MainTex, i.uv);
                fixed4 dirtColor = tex2D(_DirtTex, i.uv);
                fixed4 cleanColor = tex2D(_CleanTex, i.uv);
                fixed4 color;

                color.rgb = lerp(dirtColor.rgb, cleanColor.rgb, min(max(mainColor.r * 100, 0), 1));
                color.a = 1.0;

                return color;
            }
            ENDCG
        }
    }
}