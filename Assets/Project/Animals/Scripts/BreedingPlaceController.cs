using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BreedingPlaceController : MonoBehaviour
{
    [SerializeField] private int maxAnimalsCount = 3;
    [SerializeField] private List<AnimalSO> currentAnimals = new List<AnimalSO>();
    [SerializeField] private List<Transform> animalsPosition = new List<Transform>();
    [SerializeField] public List<AnimalController> animals = new List<AnimalController>();
    [Header("Modal")] 
    [SerializeField] private ModalController modal;
    [SerializeField] private BreedingModal breedingModal;
    [SerializeField] private AnimalList animalList;

    public static event EventHandler OnBuyAnimal;
    
    private void Start()
    {
        modal = GameObject.Find("BreedingModal").GetComponent<ModalController>();
        breedingModal = modal.GetComponent<BreedingModal>();
        animalList = modal.GetComponent<AnimalList>();
    }

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            breedingModal.SetController(this);
            animalList.SetController(this);
            
            modal.OpenModal();
        }
    }

    public void AddAnimal(AnimalSO animal)
    {
        if (currentAnimals.Count == maxAnimalsCount)
        {
            MessageBox.Instance.ShowMessage("Máximo de animais neste cercado!");
            return;
        }

        if (PlayerMoney.instance.SpendCoins(animal.Price))
        {
            currentAnimals.Add(animal);
            int count = currentAnimals.Count - 1;
            AnimalController newAnimal = Instantiate(animal.Visual, animalsPosition[count]);
            newAnimal.SetAsset(animal);
            animals.Add(newAnimal);
            
            OnBuyAnimal?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            MessageBox.Instance.ShowMessage("Dinheiro insuficiente!");
            return;
        }
    }
}
