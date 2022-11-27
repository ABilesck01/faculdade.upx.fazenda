using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerFarmDataController : MonoBehaviour
{
    public PlayerFormData objects;

    [TextArea(5, 10)]
    public string json;

    public static PlayerFarmDataController instance;

    public static string path;

    private void Awake()
    {
        instance = this;
        path = Application.dataPath + "/playerData.json";
    }

    private void Start()
    {
        if (System.IO.File.Exists(path))
        {
            RetrieveData();
        }
    }

    public void AddData(PlacedObjectData data)
    {
        objects.PlacedObjects.Add(data);
    }

    public void RemoveData(PlacedObjectData data)
    {
        objects.PlacedObjects.Remove(data);
    }


    private void OnApplicationQuit()
    {
        SaveData();
    }

    [ContextMenu("UpdateJsonData")]
    public void UpdateJsonData()
    {
        json = JsonUtility.ToJson(objects);
    }


    [ContextMenu("Save data")]
    public void SaveData()
    {
        System.IO.File.WriteAllText(path, json);
    }

    [ContextMenu("RetrieveData")]
    public void RetrieveData()
    {
        Debug.Log("Retrieve data");
        
        if(!File.Exists(path)) return;
        try
        {
            json = System.IO.File.ReadAllText(path);

            PlayerFormData retrievedObjects = JsonUtility.FromJson<PlayerFormData>(json);

            Debug.Log("retrievedObjects " + retrievedObjects.PlacedObjects.Count);

            foreach (PlacedObjectData item in retrievedObjects.PlacedObjects)
            {
                Vector3 worldPos = new Vector3
                (
                    item.worldPos_x, item.worldPos_y, item.worldPos_z
                );
                BuildingTypeSO.Dir dir = (BuildingTypeSO.Dir)item.direction;
                BuildingTypeSO building = BuildingsDatabase.instance.GetBuildingByIndex(item.BuildingId);

                Debug.Log("Building " + building.name);

                BuildingSystem.instance.SetBuilding(building);
                BuildingSystem.instance.SetDir(dir);

                BuildingSystem.instance.PlaceObjectAtPosition
                    (worldPos, false);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public PlacedObjectData FindObjectDataByOrigin(Vector2Int origin)
    {
        return objects.PlacedObjects.Find(data => data.origin_x == origin.x && data.origin_y == origin.y);
    }
}
