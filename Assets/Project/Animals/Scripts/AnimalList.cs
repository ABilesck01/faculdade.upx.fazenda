using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalList : MonoBehaviour
{
    [SerializeField] private Transform listContainer;
    [SerializeField] private AnimalListItem template;

    private BreedingPlaceController _currentPlaceController;
    private ModalController _modalController;

    private void Awake()
    {
        _modalController = GetComponent<ModalController>();
    }

    public void SetController(BreedingPlaceController current)
    {
        _currentPlaceController = current;
        FillList(_currentPlaceController.animals);
    }
    
    private void FillList(List<AnimalController> animals)
    {
        foreach (Transform item in listContainer)
        {
            if(item.name.Equals("Template")) continue;
            
            Destroy(item.gameObject);
        }

        foreach (AnimalController animal in animals)
        {
            AnimalListItem item = Instantiate(template, listContainer);
            item.Fill(
                animal.Name,
                animal.ActionName(),
                () =>
                {
                    animal.PlayAction();
                    _modalController.CloseModal();
                });
            
            item.gameObject.SetActive(true);
        }
    }
}
