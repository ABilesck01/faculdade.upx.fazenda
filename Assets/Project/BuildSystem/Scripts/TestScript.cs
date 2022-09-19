using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Grid<GridObject> grid;

    public List<PlacedObject> placedObjects = new List<PlacedObject>();
}
