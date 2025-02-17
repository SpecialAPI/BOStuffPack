Shader "Unlit/MindHouseChromAberration"
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
                float2 offs = float2(-2, 1) * (sin(_Time.y * 2) / 2 + 0.5);

                float2 pixUv = i.uv / _MainTex_TexelSize;
                float2 uv2 = (pixUv + offs) * _MainTex_TexelSize;
                float2 uv3 = (pixUv + 2 * offs) * _MainTex_TexelSize;

                fixed4 col1 = tex2D(_MainTex, i.uv);
                fixed4 col2 = tex2D(_MainTex, uv2);
                fixed4 col3 = tex2D(_MainTex, uv3);

                fixed4 col = fixed4(col1.r, col2.g, col3.b, col1.a + col2.a + col3.a);

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
