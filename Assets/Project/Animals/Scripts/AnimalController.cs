using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [SerializeField] private AnimalSO asset;
    [SerializeField] private GameObject baloon;
    
    private PlayerInventory inventory;
    public double TimeFeeded = 0;
    
    private DateTime lastTimeFeeded;
    private bool hasFeed = false;
    private bool hasItem = false;

    public string Name => asset.name;
    
    public int PriceToFeed => asset.feedCost;
    
    private void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
    }
    
    private void Update()
    {
        if (hasFeed)
        {
            TimeFeeded = (DateTime.Now - lastTimeFeeded).TotalSeconds;
            if (TimeFeeded >= asset.timeToFeed)
            {
                hasFeed = false;
                baloon.SetActive(true);
                hasItem = true;
            }
        }
    }

    public void SetAsset(AnimalSO toSet)
    {
        asset = toSet;
    }

    public void PlayAction()
    {
        if (!hasFeed)
        {
            if (hasItem)
            {
                if (inventory.AddItem(asset.item))
                {
                    hasItem = false;
                }
            }
            else
            {
                Feed();
            }
        }
    }

    public string ActionName()
    {
        if (!hasFeed)
        {
            if (hasItem)
            {
                return "Recolher item";
            }
            else
            {
                return $"Alimentar ({asset.feedCost})";
            }
        }
        
        return "Pastando";
    }
    
    private void Feed()
    {
        lastTimeFeeded = DateTime.Now;
        hasFeed = true;
    }
    
    
}
