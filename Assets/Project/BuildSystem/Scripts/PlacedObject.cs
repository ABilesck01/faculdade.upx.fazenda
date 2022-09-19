using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacedObject : MonoBehaviour
{
    public static PlacedObject Create(Vector3 worldPosition, Vector2Int origin, 
        BuildingTypeSO.Dir dir, BuildingTypeSO building)
    {
        Transform placedObjectTransform = Instantiate(
            building.prefab,
            worldPosition,
            Quaternion.Euler(0, building.GetRotationAngle(dir), 0));

        PlacedObject placedObject = placedObjectTransform.GetComponent<PlacedObject>();

        placedObject.buildingTypeSO = building;
        placedObject.origin = origin;
        placedObject.dir = dir;

        //FindObjectOfType<TestScript>().placedObjects.Add(placedObject);
        PlacedObjectData placedObjectData = new PlacedObjectData();
        
        placedObjectData.BuildingId = BuildingsDatabase.instance.GetBuildingIndex(building);
        placedObjectData.origin_x = origin.x;
        placedObjectData.origin_y = origin.y;
        placedObjectData.direction = (int)dir;
        placedObjectData.worldPos_x = worldPosition.x;
        placedObjectData.worldPos_y = worldPosition.y;
        placedObjectData.worldPos_z = worldPosition.z;

        PlayerFarmData.instance.AddData(placedObjectData);

        return placedObject;
    }

    private BuildingTypeSO buildingTypeSO;
    private Vector2Int origin;
    private BuildingTypeSO.Dir dir;

    public List<Vector2Int> getGridPositionList()
    {
        return buildingTypeSO.GetGridPositionList(origin, dir);
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
