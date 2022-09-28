using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingTerrainController : MonoBehaviour
{
    public Mesh rawMesh;
    public Mesh readyMesh;
    public int maxUses = 2;

    private MeshFilter meshFilter;

    public bool CanPlant = false;
    private int currentUses = 0;

    private void Start()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
    }

    [ContextMenu("Prepare Terrain")]
    public void PrepareTerrain()
    {
        if (CanPlant) return;

        CanPlant = true;
        meshFilter.mesh = readyMesh;
    }

    [ContextMenu("Get Plant")]
    public void GetPlant()
    {
        Debug.Log("get plant");

        CanPlant = false;
        meshFilter.mesh = rawMesh;
        currentUses++;
        if (currentUses >= maxUses)
        {
            Destroy(gameObject);
            //remove from grid
        }
    }
}
