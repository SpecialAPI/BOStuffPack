Shader "Unlit/Borchestra3DOutlineShader"
{
    Properties
    {
        _ArtificialDepth ("Depth", Float) = 0
        _ArtificialDepthScale ("Depth Scale", Float) = 0
        _OutlineColor ("Color", Color) = (1,1,1,1)
        _OutlineAlpha ("Alpha", Range(0,1)) = 1
        _Thickness ("Outline Thickness", Float) = 0
        _Scale ("Outline Scale", Vector) = (1,1,1,1)
        _ScaledOffset ("Scaled Offset", Vector) = (0,0,0,0)
        _NonScaledOffset ("Non-scaled Offset", Vector) = (0,0,0,0)
    }
    SubShader
    {
        Tags { "Queue" = "Transparent" "CanUseSpriteAtlas" = "True" "IgnoreProjector" = "True" "RenderType" = "Transparent" "PreviewType" = "Plane" }
		Blend SrcAlpha OneMinusSrcAlpha
		Cull Front
		//ZWrite Off
		ZTest LEqual

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _ArtificialDepth;
            float _ArtificialDepthScale;
            float4 _OutlineColor;
            float _OutlineAlpha;
            float _Thickness;
            float3 _Scale;
            float3 _ScaledOffset;
            float3 _NonScaledOffset;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos((v.vertex + _ScaledOffset) * _Scale + _NonScaledOffset + v.normals * _Thickness);
                o.vertex.z = (o.vertex.z * _ArtificialDepthScale) + _ArtificialDepth;
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                clip(_OutlineAlpha - 0.001);

                return fixed4(_OutlineColor.rgb, _OutlineAlpha);
            }
            ENDCG
        }
    }
}
