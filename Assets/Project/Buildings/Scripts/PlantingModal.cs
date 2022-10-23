using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingModal : MonoBehaviour
{
    public PlantingGrow plantingGrow;
    public List<SeedSO> Seeds;
    [SerializeField] private Transform container;
    [SerializeField] private PlantingItem plantingItemTemplate;

    public void SetPlant(SeedSO seed)
    {
        plantingGrow.PlantSeed(seed);
    }

    private void Start()
    {
        FillPlants();
    }

    private void FillPlants()
    {
        plantingItemTemplate.gameObject.SetActive(false);

        foreach (Transform item in container)
        {
            if (!item.name.Equals(plantingItemTemplate.gameObject.name))
            {
                Destroy(item.gameObject);
            }
        }

        foreach (SeedSO item in Seeds)
        {
            PlantingItem it = Instantiate(plantingItemTemplate, container);
            it.name = "btn_" + item.name;
            it.SetItem(item, () => SetPlant(item));
            it.gameObject.SetActive(true);
        }
    }
}
