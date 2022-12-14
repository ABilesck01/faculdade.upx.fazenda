using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, 
        BuildingTypeSO.Dir dir, BuildingTypeSO building, string data = "")
    {
        Transform placedObjectTransform = Instantiate(
            building.prefab,
            worldPosition,
            Quaternion.Euler(0, building.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.buildingTypeSO = building;
        placedObject.origin = origin;
        placedObject.dir = dir;
        placedObject.data = data;

        PlacedObjectData placedObjectData = new PlacedObjectData();
        
        placedObjectData.BuildingId = BuildingsDatabase.instance.GetBuildingIndex(building);
        placedObjectData.origin_x = origin.x;
        placedObjectData.origin_y = origin.y;
        placedObjectData.direction = (int)dir;
        placedObjectData.worldPos_x = worldPosition.x;
        placedObjectData.worldPos_y = worldPosition.y;
        placedObjectData.worldPos_z = worldPosition.z;
        placedObjectData.BuildingData = data;

        PlayerFarmDataController.instance.AddData(placedObjectData);
        placedObject.placedObjectData = placedObjectData;

        return placedObject;
    }

    private PlacedObjectData placedObjectData;
    private BuildingTypeSO buildingTypeSO;
    private Vector2Int origin;
    private BuildingTypeSO.Dir dir;
    private string data;
    public Vector2Int Origin { get => origin; set => origin = value; }

    public List<Vector2Int> getGridPositionList()
    {
        return buildingTypeSO.GetGridPositionList(origin, dir);
    }

    public void DestroySelf()
    {
        PlayerFarmDataController.instance.RemoveData(placedObjectData);

        Destroy(gameObject);
    }
}
