using System.Collections.Generic;

[System.Serializable]
public class PlacedObjectData
{
    public int BuildingId;
    public int origin_x;
    public int origin_y;
    public int direction;
    public float worldPos_x;
    public float worldPos_y;
    public float worldPos_z;

}

[System.Serializable]
public class PlacedObjectDataList
{
    public List<PlacedObjectData> List;
}

