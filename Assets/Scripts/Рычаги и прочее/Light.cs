using UnityEngine;

public class Light : MonoBehaviour
{
    public Material LightMaterial;
    public Material DarkMaterial;
    private Renderer Render;
    void Start()
    {
        Render = GetComponent<Renderer>();
    }

    public void LightOn()
    {
        Render.material = LightMaterial;
    }
    public void LightOff()
    {
        Render.material = DarkMaterial;
    }
}
