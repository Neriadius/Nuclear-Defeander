using UnityEngine;


public class GunSocketInteractor : UnityEngine.XR.Interaction.Toolkit.Interactors.XRSocketInteractor
{
    protected override bool ShouldDrawHoverMesh(
        MeshFilter meshFilter, 
        Renderer meshRenderer, 
        Camera mainCamera)
    {
        // Skip rendering any LineRenderer in the hover preview
        if (meshRenderer is LineRenderer)
            return false;

        return base.ShouldDrawHoverMesh(meshFilter, meshRenderer, mainCamera);
    }
}