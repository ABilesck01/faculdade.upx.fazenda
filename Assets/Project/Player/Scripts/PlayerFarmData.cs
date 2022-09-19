using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFarmData : MonoBehaviour
{
    public PlacedObjectDataList objects;

    [TextArea(5, 10)]
    public string json;

    public static PlayerFarmData instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if(!json.Equals(""))
        {
            RetrieveData();
        }
    }

    public void AddData(PlacedObjectData data)
    {
        objects.List.Add(data);
        json = JsonUtility.ToJson(objects);
    }

    public void RetrieveData()
    {
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

            PlacedObject.Create(worldPos, origin, dir, building);
        }
    }
}
