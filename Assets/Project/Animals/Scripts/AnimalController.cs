using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [SerializeField] private AnimalSO asset;
    [SerializeField] private GameObject baloon;

    public double TimeFeeded = 0;
    
    private DateTime lastTimeFeeded;
    private bool hasFeed = false;
    private bool hasItem = false;

    public string Name => asset.name;
    
    public int PriceToFeed => asset.feedCost;
    
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
                //TODO give item
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
