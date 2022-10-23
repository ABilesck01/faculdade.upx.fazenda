using System.Collections.Generic;
using UnityEngine;

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
    public string BuildingData;

}

[System.Serializable]
public class PlayerFormData
{
    public string FarmName = "Fazenda Feliz =D";
    public int Currency = 1000;
    public int CurrencyPaid = 0;
    public List<PlacedObjectData> PlacedObjects;
}

