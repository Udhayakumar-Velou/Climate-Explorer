using UnityEngine;

public class CountryBehaviour : MonoBehaviour
{
    public CountryData Country;
    private Renderer meshRenderer;

    void Awake()
    {
        meshRenderer = GetComponent<Renderer>();
    }

    public void Highlight()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.yellow;
        }
    }

    public void Hover()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color =
                Color.Lerp(Country.CurrentColor, Color.white, 0.35f);
        }
    }
    public void ExitHover()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Country.CurrentColor;
        }
    }

    public void ResetColor()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Country.CurrentColor;
        }
    }
}