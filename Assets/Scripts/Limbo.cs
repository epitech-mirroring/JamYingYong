using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

[Serializable]
[PostProcess(typeof(LimboRenderer), PostProcessEvent.AfterStack, "Custom/Limbo", allowInSceneView: false)]
public sealed class Limbo : PostProcessEffectSettings
{
}

public sealed class LimboRenderer : PostProcessEffectRenderer<Limbo>
{
    public override void Render(PostProcessRenderContext context)
    {
        var depthBlackAndWhite = context.propertySheets.Get(Shader.Find("Hidden/Custom/DepthBlackAndWhite"));
        context.command.BlitFullscreenTriangle(context.source, context.destination, depthBlackAndWhite, 0);
    }
}