using System;
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

    public event EventHandler OnPrepareTerrain;
    public event EventHandler OnGetPlant;

    private void Start()
    {
        meshFilter = GetComponentInChildren<MeshFilter>();
    }

    [ContextMenu("Prepare Terrain")]
    public void PrepareTerrain()
    {
        if (CanPlant) return;

        OnPrepareTerrain?.Invoke(this, null);
        CanPlant = true;
        meshFilter.mesh = readyMesh;
    }

    public void UpdateMesh(int state)
    {
        if(state == 0)
        {
            CanPlant = false;
            meshFilter.mesh = rawMesh;
        }
        else if(state == 1)
        {
            CanPlant = true;
            meshFilter.mesh = readyMesh;
        }
    }

    [ContextMenu("Get Plant")]
    public void GetPlant()
    {
        OnGetPlant?.Invoke(this, null);
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
