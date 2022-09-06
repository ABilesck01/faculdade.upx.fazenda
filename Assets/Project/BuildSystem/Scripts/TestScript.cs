using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] private Grid<GridObject> grid;

    // Start is called before the first frame update
    void Awake()
    {
        grid = new Grid<GridObject>(10, 10, 10, Vector3.zero, 
            (Grid<GridObject> g, int x, int z) => new GridObject(g, x, z), true);
    }
}
