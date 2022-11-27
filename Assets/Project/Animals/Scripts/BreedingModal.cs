using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreedingModal : MonoBehaviour
{
    [SerializeField] private List<AnimalSO> animalsToBuy = new List<AnimalSO>();
    [SerializeField] private Transform listContainer;
    [SerializeField] private AnimalUIItem template;

    private ModalController _modalController;
    private BreedingPlaceController _currentPlaceController;

    private void Awake()
    {
        _modalController = GetComponent<ModalController>();
    }

    public void SetController(BreedingPlaceController current)
    {
        _currentPlaceController = current;
        FillList();
    }

    private void FillList()
    {
        foreach (Transform item in listContainer)
        {
            if(item.name.Equals("CardTemplate")) continue;
            
            Destroy(item.gameObject);
        }

        foreach (AnimalSO animal in animalsToBuy)
        {
            AnimalUIItem item = Instantiate(template, listContainer);
            item.FillItem(animal, () =>
            {
                _currentPlaceController.AddAnimal(animal);
                _modalController.CloseModal();
            });
            
            item.gameObject.SetActive(true);
        }
    }
}
