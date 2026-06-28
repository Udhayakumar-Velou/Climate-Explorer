    using UnityEngine;

    public class ClimateDataManager : MonoBehaviour
    {
        public static ClimateDataManager Instance;

        [Header("CSV Files")]
        public TextAsset populationCSV;
        public TextAsset co2CSV;
        public TextAsset renewableCSV;
        public TextAsset temperatureCSV;

        [Header("Loaded Datasets")]
        public ClimateDataset Population;
        public ClimateDataset CO2;
        public ClimateDataset Renewable;
        public ClimateDataset Temperature;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        private void Start()
        {
            Population = CsvLoader.Load(populationCSV);
            CO2 = CsvLoader.Load(co2CSV);
            Renewable = CsvLoader.Load(renewableCSV);
            Temperature = CsvLoader.Load(temperatureCSV);

            Debug.Log("========== Climate Data Loaded ==========");
            Debug.Log("Population Countries : " + Population.Data.Count);
            Debug.Log("CO2 Countries : " + CO2.Data.Count);
            Debug.Log("Renewable Countries : " + Renewable.Data.Count);
            Debug.Log("Temperature Countries : " + Temperature.Data.Count);
            Debug.Log("=========================================");
            
            if (CountryColorManager.Instance != null)
            {

                CountryColorManager.Instance.RefreshColors();

            }
        }
    }