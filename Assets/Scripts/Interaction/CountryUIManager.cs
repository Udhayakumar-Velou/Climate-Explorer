using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CountryUIManager : MonoBehaviour
{
    public static CountryUIManager Instance;
    private CountryData selectedCountry;

    public TMP_Text countryName;
    // public TMP_Text countryISO;
    public TMP_Text countryPopulation;
    public TMP_Text countryCO2;
    public TMP_Text countryTemperature;
    public TMP_Text countryRenewable;

    private void Awake()
    {
        Instance = this;
    }


    public void ShowCountry(CountryData country)
    {
       
        if (country == null)
            return;

        selectedCountry = country;

        countryName.text = country.Name;

        string iso = country.IsoCode;

        // countryISO.text = iso;

        // ================= Population =================

        if (ClimateDataManager.Instance.Population.Data.ContainsKey(iso))
        {
            var years = ClimateDataManager.Instance.Population.Data[iso];

            int selectedYear = YearManager.Instance.CurrentYear;

            int nearestYear = FindNearestYear(years, selectedYear);

            float population = years[nearestYear];

            countryPopulation.text =
            "Population      " +
            population.ToString(
                "N0",
                System.Globalization.CultureInfo.InvariantCulture
            );
        }
        else
        {
            countryPopulation.text = "Population      —";
        }

        // ================= CO2 =================

        if (ClimateDataManager.Instance.CO2.Data.ContainsKey(iso))
        {
            var years = ClimateDataManager.Instance.CO2.Data[iso];

            int nearestYear = FindNearestYear(years, YearManager.Instance.CurrentYear);

            float co2 = years[nearestYear];

            countryCO2.text =
            "CO2 Emissions   " +
            co2.ToString("0.00") +
            " t/person";
        }
        else
        {
            countryCO2.text = "CO2 Emissions   —";
        }

        // ================= Temperature =================

        if (ClimateDataManager.Instance.Temperature.Data.ContainsKey(iso))
        {
            var years = ClimateDataManager.Instance.Temperature.Data[iso];

            int nearestYear = FindNearestYear(years, YearManager.Instance.CurrentYear);

            float temperature = years[nearestYear];

            countryTemperature.text =
            "Avg Temperature     " +
            temperature.ToString("0.00") +
            " °C";
        }
        else
        {
            countryTemperature.text = "Avg Temperature     —";
        }

        // ================= Renewable =================

        if (ClimateDataManager.Instance.Renewable.Data.ContainsKey(iso))
        {
            var years = ClimateDataManager.Instance.Renewable.Data[iso];

            int nearestYear = FindNearestYear(years, YearManager.Instance.CurrentYear);

            float renewable = years[nearestYear];

            countryRenewable.text =
            "Renewables      " +
            renewable.ToString("0.00") +
            " %";
        }
        else
        {
            countryRenewable.text = "Renewables      —";
        }
    }
    public void RefreshCountry()
    {
        if (selectedCountry == null)
            return;

        ShowCountry(selectedCountry);
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


    
}