using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalList : MonoBehaviour
{
    [SerializeField] private Transform listContainer;
    [SerializeField] private AnimalListItem template;

    private BreedingPlaceController _currentPlaceController;
    
    public void SetController(BreedingPlaceController current)
    {
        _currentPlaceController = current;
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
                });
            
            item.gameObject.SetActive(true);
        }
    }
}
