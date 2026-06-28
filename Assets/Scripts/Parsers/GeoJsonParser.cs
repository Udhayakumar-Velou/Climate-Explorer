using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public static class GeoJsonParser
{
    public static List<CountryData> Parse(TextAsset geoJsonFile)
    {
        List<CountryData> countries = new List<CountryData>();

        JObject root = JObject.Parse(geoJsonFile.text);

        JArray features = (JArray)root["features"];

        foreach (JObject feature in features)
        {
            CountryData country = new CountryData();

            country.Name =
                feature["properties"]["name"].ToString();

            country.IsoCode =
                feature["id"].ToString();

            string geometryType =
                feature["geometry"]["type"].ToString();

            JToken coordinates =
                feature["geometry"]["coordinates"];

            if (geometryType == "Polygon")
            {
                ReadPolygon(country, coordinates);
            }
            else if (geometryType == "MultiPolygon")
            {
                ReadMultiPolygon(country, coordinates);
            }

            countries.Add(country);
        }

        Debug.Log("Countries Loaded : " + countries.Count);

        return countries;
    }

    static void ReadPolygon(
        CountryData country,
        JToken coordinates)
    {
        foreach (JToken ring in coordinates)
        {
            List<Vector2> border = new List<Vector2>();

            foreach (JToken point in ring)
            {
                float lon = point[0].Value<float>();
                float lat = point[1].Value<float>();

                border.Add(new Vector2(lon, lat));
            }

            country.Borders.Add(border);
        }
    }

    static void ReadMultiPolygon(
        CountryData country,
        JToken coordinates)
    {
        foreach (JToken polygon in coordinates)
        {
            foreach (JToken ring in polygon)
            {
                List<Vector2> border = new List<Vector2>();

                foreach (JToken point in ring)
                {
                    float lon = point[0].Value<float>();
                    float lat = point[1].Value<float>();

                    border.Add(new Vector2(lon, lat));
                }

                country.Borders.Add(border);
            }
        }
    }
}