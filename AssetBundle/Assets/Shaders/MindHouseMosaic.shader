Shader "Unlit/MindHouseMosaic"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags
        {
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Blend One OneMinusSrcAlpha

        LOD 100

        Pass
        {
            //Tags { "CanUseSpriteAtlas" = "true" "IGNOREPROJECTOR" = "true" "PreviewType" = "Plane" "QUEUE" = "Transparent" "RenderType" = "Transparent" }
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
            float2 _MainTex_TexelSize;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float random(float2 Seed, float Min, float Max)
            {
                float randomno = frac(sin(dot(Seed, float2(12.9898, 78.233)))*43758.5453);
                return lerp(Min, Max, randomno);
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float t = ((sin(_Time.y * 5) / 2) + 1) * 5;
                float2 pixelUv = round(i.uv / _MainTex_TexelSize / t) * t;
                float2 uv = pixelUv * _MainTex_TexelSize;

                // sample the texture
                fixed4 col = tex2D(_MainTex, uv);
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                //col.rgb = 1;

                if(col.a == 0)
                {
                    discard;
                }

                return col;
            }
            ENDCG
        }
    }
}
