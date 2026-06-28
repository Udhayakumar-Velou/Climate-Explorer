using System.Collections.Generic;

[System.Serializable]
public class ClimateDataset
{
    public Dictionary<string, Dictionary<int, float>> Data
        = new Dictionary<string, Dictionary<int, float>>();
}