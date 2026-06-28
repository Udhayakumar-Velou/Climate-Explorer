using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YearManager : MonoBehaviour
{
    public static YearManager Instance;

    public Slider yearSlider;
    public TMP_Text yearText;

    public int CurrentYear { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentYear = (int)yearSlider.value;
        yearText.text = "Year : " + CurrentYear;

        yearSlider.onValueChanged.AddListener(OnYearChanged);
    }

    private void OnYearChanged(float value)
    {
        CurrentYear = (int)value;

        yearText.text = "Year : " + CurrentYear;

        Debug.Log("Current Year : " + CurrentYear);

        CountryUIManager.Instance.RefreshCountry();
        CountryColorManager.Instance.RefreshColors();
    }
}