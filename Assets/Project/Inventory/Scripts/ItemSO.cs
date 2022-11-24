using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/item")]
public class ItemSO : ScriptableObject
{
    public int id;
    public string Name;
    public Sprite Icon;
    public bool IsStackable;
    public float price;
    public int Amount;
}

