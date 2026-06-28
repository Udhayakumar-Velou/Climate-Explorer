using System.Collections.Generic;
using UnityEngine;

public class GeoJsonManager : MonoBehaviour
{
    public static GeoJsonManager Instance;

    [Header("GeoJSON File")]
    public TextAsset geoJsonFile;

    [Header("Mesh Generator")]
    
    public CountryMeshGenerator meshGenerator;



    public List<CountryData> Countries = new List<CountryData>();

    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        LoadCountries();
    }

    public void LoadCountries()
    {
        if (geoJsonFile == null)
        {
            Debug.LogError("GeoJSON file not assigned.");
            return;
        }

        Countries = GeoJsonParser.Parse(geoJsonFile);

    

        for (int i = 0; i < Mathf.Min(10, Countries.Count); i++)
        {
            Debug.Log(
                Countries[i].IsoCode +
                " - " +
                Countries[i].Name +
                " | Borders : " +
                Countries[i].Borders.Count
            );
        }

        if (meshGenerator != null)
        {
            foreach (CountryData country in Countries)
            {
                meshGenerator.GenerateCountry(country);
            }

            if (CountryColorManager.Instance != null)
            {
                CountryColorManager.Instance.RefreshColors();
                CountrySelector selector = FindFirstObjectByType<CountrySelector>();

                foreach (CountryData country in Countries)
                {
                    if (country.IsoCode == "FRA")
                    {
                        CountryBehaviour behaviour =
                            country.GameObject.GetComponent<CountryBehaviour>();

                        selector.SelectCountry(behaviour);

                        break;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Mesh Generator is not assigned.");
        }
    }
}