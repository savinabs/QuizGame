using UnityEngine;

[CreateAssetMenu(menuName = "Country")]
public class CountrySO : ScriptableObject
{
    [SerializeField]
    string countryName;

    [SerializeField]
    string countryCapital;

    [SerializeField]
    int countryDifficulty;

    [SerializeField]
    string countryCurrency;

    [SerializeField]
    string countryContinent;

    [SerializeField]
    Sprite countryFlag;

    public string GetCountryName() { return countryName; }
    public string GetCountryCapital() { return countryCapital; }
    public int GetCountryDifficulty() { return countryDifficulty; }
    public string GetCountryCurrency() { return countryCurrency; }
    public string GetCountryContinent() { return countryContinent; }
    public Sprite GetCountryFlag() { return countryFlag; }
}
