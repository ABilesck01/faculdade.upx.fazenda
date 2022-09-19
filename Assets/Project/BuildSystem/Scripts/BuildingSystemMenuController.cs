using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystemMenuController : MonoBehaviour
{
    [SerializeField] private List<BuildingTypeSO> Buildings;
    [SerializeField] private GameObject MenuItem;
    [SerializeField] private Transform MenuParent;
    [Space]
    [SerializeField] private BuildingSystem MyBuildingSystem;

    private void Start()
    {
        Buildings = BuildingsDatabase.instance.GetAllBuildings();

        foreach (BuildingTypeSO item in Buildings)
        {
            GameObject gameObject = Instantiate(MenuItem, MenuParent);
            BuilsingSystemMenuItem menuItem = gameObject.GetComponent<BuilsingSystemMenuItem>();
            menuItem.text.text = item.name;
            menuItem.button.onClick.AddListener( () => SetBuilding(item));
        }
    }

    public void SetBuilding(BuildingTypeSO building)
    {
        MyBuildingSystem.SetBuilding(building);
    }

    public void btnConfirm_Click()
    {
        BuildingSystem.instance.PlaceObject();
    }

    public void btnRotate_Click()
    {
        BuildingSystem.instance.ManageRotation();
    }

}
