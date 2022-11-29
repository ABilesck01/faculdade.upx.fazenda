using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSearch : MonoBehaviour
{
    public int CoValue;
    public int CurrentPriceRaiseOnSeeds;
    public int CurrentPriceRaiseOnAnimals;
    
    [Header("Costs")]
    public int CurrentCoCostOnSeeds;
    public int CurrentCoCostOnAnimals;
    private void OnEnable()
    {
        PlantingGrow.OnPlantSeed += PlantingGrowOnOnPlantSeed;
        BreedingPlaceController.OnBuyAnimal += BreedingPlaceControllerOnOnBuyAnimal;
        SearchController.OnSearchCompleted += SearchControllerOnOnSearchCompleted;
    }

    private void OnDisable()
    {
        PlantingGrow.OnPlantSeed -= PlantingGrowOnOnPlantSeed;
        BreedingPlaceController.OnBuyAnimal -= BreedingPlaceControllerOnOnBuyAnimal;
    }

    private void PlantingGrowOnOnPlantSeed(object sender, PlantingGrow.OnPlantSeedEventArgs e)
    {
        CoValue += CurrentCoCostOnSeeds;
        if (CoValue < 0) CoValue = 0;
    }
    
    private void BreedingPlaceControllerOnOnBuyAnimal(object sender, EventArgs e)
    {
        CoValue += CurrentCoCostOnAnimals;
        if (CoValue < 0) CoValue = 0;
    }
    
    private void SearchControllerOnOnSearchCompleted(object sender, SearchController.OnSearchCompletedEventArgs e)
    {
        if (e.completedSearch.Type == SearchSO.SearchType.seed)
        {
            CurrentPriceRaiseOnSeeds += e.completedSearch.PriceIncrease;
            CurrentCoCostOnSeeds += e.completedSearch.CoCost;
        }
        else
        {
            CurrentPriceRaiseOnAnimals += e.completedSearch.PriceIncrease;
            CurrentCoCostOnAnimals += e.completedSearch.CoCost;
        }
    }
}
