using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/Seeds")]
public class SeedSO : ScriptableObject
{
    public string SeedName;
    public Sprite Icon;
    public int BuyCost;
    public int SellCost;
    public float TimeToFullGrow;
    public GameObject stepOne;
    public GameObject stepTwo;
    public GameObject stepTree;
}
