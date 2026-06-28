using System.Collections.Generic;
using UnityEngine;

public static class CsvLoader
{
    public static ClimateDataset Load(TextAsset csvFile)
    {
        ClimateDataset dataset = new ClimateDataset();

        if (csvFile == null)
        {
            Debug.LogError("CSV file is missing.");
            return dataset;
        }

        string[] lines = csvFile.text.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(lines[i]))
                continue;

            string[] columns = lines[i].Split(',');

            if (columns.Length < 4)
                continue;

            string iso = columns[1].Trim();

            if (string.IsNullOrEmpty(iso))
                continue;

            if (!int.TryParse(columns[2], out int year))
                continue;

            if (!float.TryParse(columns[3], out float value))
                continue;

            if (!dataset.Data.ContainsKey(iso))
            {
                dataset.Data.Add(
                    iso,
                    new Dictionary<int, float>()
                );
            }

            dataset.Data[iso][year] = value;
        }

        Debug.Log(
            "Loaded Dataset : " +
            dataset.Data.Count +
            " countries."
        );

        return dataset;
    }
}