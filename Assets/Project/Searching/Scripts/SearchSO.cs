using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/New Search")]
public class SearchSO : ScriptableObject
{
    public enum SearchType
    {
        animal,
        seed
    }
    
    public string Name;
    [TextArea(3, 10)]
    public string Description;
    public SearchType Type;
    public int Price;
    public int CoCost;
    public int PriceIncrease;
}
