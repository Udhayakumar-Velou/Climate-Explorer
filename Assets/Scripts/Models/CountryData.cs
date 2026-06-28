using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CountryData
{
    // Country name
    public string Name;

    // ISO code (AFG, IND, FRA...)
    public string IsoCode;

    // Border coordinates
    public List<List<Vector2>> Borders = new();

    // Mesh generated later
    public Mesh Mesh;

    // Country GameObject
    public GameObject GameObject;

    // Current heatmap color
    public Color CurrentColor = Color.green;
}