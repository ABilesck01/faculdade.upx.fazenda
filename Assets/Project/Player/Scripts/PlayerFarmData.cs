using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarmData : MonoBehaviour
{
    public PlacedObjectDataList objects;

    [TextArea(5, 10)]
    public string json;

    public static PlayerFarmData instance;

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
        objects.List.Add(data);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }

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
        json = System.IO.File.ReadAllText(path);

        PlacedObjectDataList retrievedObjects = JsonUtility.FromJson<PlacedObjectDataList>(json);

        foreach (PlacedObjectData item in retrievedObjects.List)
        {
            Vector3 worldPos = new Vector3
            (
                item.worldPos_x, item.worldPos_y, item.worldPos_z
            );

            Vector2Int origin = new Vector2Int
            (
                item.origin_x, item.origin_y
            );
            BuildingTypeSO.Dir dir = (BuildingTypeSO.Dir)item.direction;
            BuildingTypeSO building = BuildingsDatabase.instance.GetBuildingByIndex(item.BuildingId);

            PlacedObject.Create(worldPos, origin, dir, building, item.BuildingData);
        }
    }

    public PlacedObjectData FindObjectDataByOrigin(Vector2Int origin)
    {
        return objects.List.Find(data => data.origin_x == origin.x && data.origin_y == origin.y);
    }
}
