using UnityEngine;

public class Light : MonoBehaviour
{
    public Material GreenMaterial;
    public Material RedMaterial;
    public Material DarkMaterial;
    private Renderer Render;
    void Start()
    {
        Render = GetComponent<Renderer>();
    }

    public void LightOn(bool rg)
    {
        if (rg)
        {
            Render.material = RedMaterial;
        }
        else
        {
            Render.material = GreenMaterial;
        }
        
    }
    public void LightOff()
    {
        Render.material = DarkMaterial;
    }
}
