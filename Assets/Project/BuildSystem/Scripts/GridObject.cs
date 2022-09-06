using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GridObject
{
    [SerializeField] private Grid<GridObject> grid;
    [SerializeField] private int x;
    [SerializeField] private int z;
    [SerializeField] private PlacedObject placedObject;

    public GridObject(Grid<GridObject> grid, int x, int z)
    {
        this.grid = grid;
        this.x = x;
        this.z = z;
        this.placedObject = null;
    }

    public void SetPlacedObject(PlacedObject t)
    {
        placedObject = t;
    }

    public void ClearPlacedObject()
    {
        placedObject = null;
    }

    public PlacedObject GetPlacedObject()
    {
        return placedObject;
    }

    public bool CanBuild()
    {
        return placedObject == null;
    }
}
