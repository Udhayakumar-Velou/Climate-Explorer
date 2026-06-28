using UnityEngine;

public class CountrySelector : MonoBehaviour
{
    private CountryBehaviour currentCountry;
    private CountryBehaviour hoveredCountry;

    void Update()
    {
        HandleHover();
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Debug.Log("Hit : " + hit.collider.gameObject.name);

            CountryBehaviour country =
                hit.collider.GetComponent<CountryBehaviour>();

            if (country == null)
                return;

            // Restore previous country's color
            if (currentCountry != null)
            {
                currentCountry.ResetColor();
            }

            // Highlight newly selected country
            currentCountry = country;
            hoveredCountry = null;
            currentCountry.Highlight();
            
            Debug.Log("Country Selected");
            Debug.Log("Name : " + country.Country.Name);
            Debug.Log("ISO : " + country.Country.IsoCode);

            CountryUIManager.Instance.ShowCountry(country.Country);
        }
        else
        {
            Debug.Log("Nothing Hit");
        }
    }

    private void HandleHover()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CountryBehaviour country =
                hit.collider.GetComponent<CountryBehaviour>();

            if (country == null)
                return;

            if (country == currentCountry)
                return;

            if (hoveredCountry != null &&
                hoveredCountry != country)
            {
                hoveredCountry.ExitHover();
            }

            hoveredCountry = country;
            hoveredCountry.Hover();
        }
        else
        {
            if (hoveredCountry != null &&
                hoveredCountry != currentCountry)
            {
                hoveredCountry.ExitHover();
                hoveredCountry = null;
            }
        }
    }
    public void SelectCountry(CountryBehaviour country)
    {
        if (country == null)
            return;

        if (currentCountry != null)
        {
            currentCountry.ResetColor();
        }

        currentCountry = country;
        hoveredCountry = null;
        currentCountry.Highlight();

        CountryUIManager.Instance.ShowCountry(country.Country);
    }

}