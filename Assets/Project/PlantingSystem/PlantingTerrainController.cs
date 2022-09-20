using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingTerrainController : MonoBehaviour
{
    public Mesh rawMesh;
    public Mesh readyMesh;
    public int maxUses = 2;

    private MeshFilter meshFilter;

    private bool canPlant = false;
    private int currentUses = 0;

    private void Start()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
    }

    [ContextMenu("Prepare Terrain")]
    private void PrepareTerrain()
    {
        if (canPlant) return;

        canPlant = true;
        meshFilter.mesh = readyMesh;
    }

    [ContextMenu("Get Plant")]
    private void GetPlant()
    {
        canPlant = false;
        meshFilter.mesh = rawMesh;
        currentUses++;
        if (currentUses >= maxUses)
        {
            Destroy(gameObject);
            //remove from grid
        }
    }
}
