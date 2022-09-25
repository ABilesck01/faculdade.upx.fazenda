using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreModalController : MonoBehaviour
{
    [Header("Items To Buy")]
    [SerializeField] private List<BuildingTypeSO> buildings = new List<BuildingTypeSO>();


    [Header("List")]
    [SerializeField] private GameObject template;
    [SerializeField] private Transform container;

    [Space]
    [SerializeField] private ModalController modalController;

    

    private void Start()
    {
        FillBuildings();
    }

    private void FillBuildings()
    {
        template.SetActive(false);

        foreach (Transform item in container)
        {
            if(!item.name.Equals(template.name))
            {
                Destroy(item.gameObject);
            }
        }

        foreach (BuildingTypeSO item in buildings)
        {
            GameObject it = Instantiate(template, container);
            it.name = "btn_" + item.name;
            it.GetComponent<StoreItem>().SetItem(item, () => CloseModal());
            it.SetActive(true);
        }
    }

    public void OpenModal()
    {
        modalController.OpenModal();
    }

    public void CloseModal()
    {
        modalController.CloseModal();
    }
}
