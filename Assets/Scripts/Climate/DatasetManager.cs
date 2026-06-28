using TMPro;
using UnityEngine;

public class DatasetManager : MonoBehaviour
{
    public static DatasetManager Instance;

    public TMP_Dropdown datasetDropdown;

    public string CurrentDataset { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        datasetDropdown.onValueChanged.AddListener(OnDatasetChanged);

        OnDatasetChanged(datasetDropdown.value);
    }


    private void OnDatasetChanged(int index)
    {
        CurrentDataset = datasetDropdown.options[index].text;
        DatasetType selected =

        (DatasetType)index;

        Debug.Log("Dataset : " + selected);

        CountryColorManager.Instance.CurrentDataset = selected;

        CountryColorManager.Instance.RefreshColors();
    }
}