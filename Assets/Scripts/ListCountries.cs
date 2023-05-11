using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ListCountries")]
public class ListCountries : ScriptableObject
{
    [SerializeField]
    public List<CountrySO> countries;
}
