Shader "Unlit/ScrollingStars_SpaceInvaders_URP"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        _ScrollSpeedY("Scroll Speed Y", Range(0, 10)) = 1.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Background" }
        LOD 100

        Pass
        {
            // Código específico para URP
            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS   : POSITION;
                float2 uv           : TEXCOORD0;
            };

            struct Varyings
            {
                float2 uv           : TEXCOORD0;
                float4 positionHCS  : SV_POSITION;
            };

            TEXTURE2D(_MainTex);
            SAMPLER(sampler_MainTex);
            CBUFFER_START(UnityPerMaterial)
                float4 _MainTex_ST;
                float _ScrollSpeedY;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = TRANSFORM_TEX(IN.uv, _MainTex);
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // La misma lógica de mover la textura
                float2 scrolledUV = IN.uv;
                scrolledUV.y += _Time.y * _ScrollSpeedY;
                
                half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, scrolledUV);
                return col;
            }
            ENDHLSL
        }
    }
}