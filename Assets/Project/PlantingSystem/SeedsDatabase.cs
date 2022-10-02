using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedsDatabase : MonoBehaviour
{
    [SerializeField] private List<SeedSO> seeds = new List<SeedSO>();

    public static SeedsDatabase instance;

    private void Awake()
    {
        instance = this;
    }

    public SeedSO GetSeedByIndex(int index)
    {
        return seeds[index];
    }

    public int GetSeedIndex(SeedSO seed)
    {
        return seeds.IndexOf(seed);
    }
}
