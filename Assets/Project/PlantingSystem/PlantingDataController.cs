using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingDataController : MonoBehaviour
{
    private PlacedObject placedObject;
    private PlantingTerrainController plantingTerrainController;
    private PlantingGrow plantingGrow;

    [SerializeField] private PlantingData data;

    private void Start()
    {
        placedObject = GetComponent<PlacedObject>();
        plantingTerrainController = GetComponent<PlantingTerrainController>();
        plantingGrow = GetComponent<PlantingGrow>();

        plantingTerrainController.OnPrepareTerrain += PlantingTerrainController_OnPrepareTerrain;
        plantingTerrainController.OnGetPlant += PlantingTerrainController_OnGetPlant;

        PlantingGrow.OnPlantSeed += PlantingGrow_OnPlantSeed;

        RetrieveData();
    }

    private void PlantingGrow_OnPlantSeed(object sender, PlantingGrow.OnPlantSeedEventArgs e)
    {
        data.SeedId = SeedsDatabase.instance.GetSeedIndex(e.seed);
        data.StartDate = e.startDate.ToString();
        UpdateData();
    }

    private void PlantingTerrainController_OnGetPlant(object sender, System.EventArgs e)
    {
        data.TerrainState = 0;
        UpdateData();
    }

    private void PlantingTerrainController_OnPrepareTerrain(object sender, System.EventArgs e)
    {
        data.TerrainState = 1;
        UpdateData();
    }

    private void RetrieveData()
    {
        string buildingJson = PlayerFarmDataController.instance.FindObjectDataByOrigin(placedObject.Origin).BuildingData;
        
        if(!string.IsNullOrEmpty(buildingJson))
        {
            data = JsonUtility.FromJson<PlantingData>(buildingJson);

            plantingTerrainController.UpdateMesh(data.TerrainState);

            if(data.TerrainState == 1)
            {
                if(!string.IsNullOrEmpty(data.StartDate))
                {
                    SeedSO seed = SeedsDatabase.instance.GetSeedByIndex(data.SeedId);
                    DateTime startDate = DateTime.Parse(data.StartDate);
                    plantingGrow.PlantSeed(seed, startDate);
                }
                //02/10/2022 15:26:46
            }

        }
    }

    private void UpdateData()
    {
        string buildingJson = JsonUtility.ToJson(data);
        PlayerFarmDataController.instance.FindObjectDataByOrigin(placedObject.Origin).BuildingData = buildingJson;
        PlayerFarmDataController.instance.UpdateJsonData();

    }
}

[System.Serializable]
public class PlantingData
{
    public int TerrainState;
    public int SeedId;
    public string StartDate;
}
