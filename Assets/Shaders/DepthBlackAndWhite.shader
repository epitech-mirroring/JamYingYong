Shader "Hidden/Custom/DepthBlackAndWhite"
{
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        TEXTURE2D_SAMPLER2D(_CameraDepthTexture, sampler_CameraDepthTexture);

        // Black and white depth of field
        // With bloom from emmisive objects
        float4 Frag(VaryingsDefault i) : SV_Target
        {
            float4 color = float4(0, 0, 0, 1);
            float depth = Linear01Depth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord));
            
            // More depth = more blurriness
            // Average neighboring pixels
            float neighborsAvg = 0;
            neighborsAvg += Linear01Depth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord + float2(0.001, 0.001)));
            neighborsAvg += Linear01Depth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord + float2(0.001, -0.001)));
            neighborsAvg += Linear01Depth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord + float2(-0.001, 0.001)));
            neighborsAvg += Linear01Depth(SAMPLE_TEXTURE2D(_CameraDepthTexture, sampler_CameraDepthTexture, i.texcoord + float2(-0.001, -0.001)));
            neighborsAvg /= 5;

            float4 blackNWhite = lerp(float4(depth, depth, depth, 1), float4(neighborsAvg, neighborsAvg, neighborsAvg, 1), depth);

            return blackNWhite;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}