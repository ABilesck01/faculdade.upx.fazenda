using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Assets/item")]
public class ItemSO : ScriptableObject
{
    public int id;
    public string Name;
    public string Description;
    public Sprite Icon;
    public bool IsStackable;
    public ItemType Type;
    public GameObject WorldGfx;
    public BuildingTypeSO buildingType;
    public int Amount;
}

public enum ItemType
{
    misc,
    tool,
    food,
    building
}

