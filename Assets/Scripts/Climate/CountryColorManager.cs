using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountryColorManager : MonoBehaviour
{
    public static CountryColorManager Instance;

    public DatasetType CurrentDataset = DatasetType.Population;

    private void Awake()
    {
        Instance = this;
    }

    public void RefreshColors()
    {
        ClimateDataset dataset = GetCurrentDataset();

        if (dataset == null)
            return;

        int year = YearManager.Instance.CurrentYear;
        float minValue = float.MaxValue;
        float maxValue = float.MinValue;

        foreach (CountryData country in GeoJsonManager.Instance.Countries)
        {
            if (!dataset.Data.ContainsKey(country.IsoCode))
                continue;
            Dictionary<int, float> years = dataset.Data[country.IsoCode];

            if (years.Count == 0)

                continue;

            int nearestYear = FindNearestYear(years, year);

            float value = years[nearestYear];

            if (value < minValue)

                minValue = value;

            if (value > maxValue)

                maxValue = value;
        }

        foreach (CountryData country in GeoJsonManager.Instance.Countries)
        {
            if (country.GameObject == null)
                continue;

            Renderer renderer = country.GameObject.GetComponent<Renderer>();

            if (renderer == null)
                continue;

            if (!dataset.Data.ContainsKey(country.IsoCode))
            {
                renderer.material.color = Color.gray;
                continue;
            }

            Dictionary<int, float> years = dataset.Data[country.IsoCode];

            if (years.Count == 0)
            {
                renderer.material.color = Color.gray;
                continue;
            }

            int nearestYear = FindNearestYear(years, year);

            float value = years[nearestYear];

           Color heatColor = GetColor(value, minValue, maxValue);

            country.CurrentColor = heatColor;

            StartCoroutine(
                AnimateColor(
                    renderer,
                    heatColor,
                    0.3f
                )
            );
        }
    }

    private int FindNearestYear(Dictionary<int, float> years, int targetYear)
    {
        int nearestYear = -1;
        int smallestDifference = int.MaxValue;

        foreach (int year in years.Keys)
        {
            int difference = Mathf.Abs(year - targetYear);

            if (difference < smallestDifference)
            {
                smallestDifference = difference;
                nearestYear = year;
            }
        }

        return nearestYear;
    }

    private Color GetColor(float value, float minValue, float maxValue)
    {
        float normalized = Mathf.InverseLerp(minValue, maxValue, value);

        // Green → Yellow
        if (normalized < 0.5f)
        {
            return Color.Lerp(
                Color.green,
                Color.yellow,
                normalized * 2f
            );
        }

        // Yellow → Red
        return Color.Lerp(
            Color.yellow,
            Color.red,
            (normalized - 0.5f) * 2f
        );
    }
    private ClimateDataset GetCurrentDataset()
    {
        switch (CurrentDataset)
        {
            case DatasetType.Population:
                return ClimateDataManager.Instance.Population;

            case DatasetType.CO2:
                return ClimateDataManager.Instance.CO2;

            case DatasetType.Temperature:
                return ClimateDataManager.Instance.Temperature;

            case DatasetType.Renewable:
                return ClimateDataManager.Instance.Renewable;

            default:
                return ClimateDataManager.Instance.Population;
        }
    }

    private IEnumerator AnimateColor(
    Renderer renderer,
    Color targetColor,
    float duration)
    {
        Color startColor = renderer.material.color;

        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;

            renderer.material.color =
                Color.Lerp(
                    startColor,
                    targetColor,
                    time / duration);

            yield return null;
        }

        renderer.material.color = targetColor;
    }


}