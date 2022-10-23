using System.Collections.Generic;
using UnityEngine;

public class BuildingsDatabase : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSO> buildings = new List<BuildingTypeSO>();

    public static BuildingsDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    public void AddBuilding(BuildingTypeSO building)
    {
        buildings.Add(building);
    }

    public BuildingTypeSO GetBuildingByIndex(int index)
    { 
        return buildings[index]; 
    }

    public int GetBuildingIndex(BuildingTypeSO building)
    {
        return buildings.IndexOf(building);
    }

    public List<BuildingTypeSO> GetAllBuildings()
    {
        return buildings;
    }
}
