using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class StoreItem : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public Image icon;
    public TextMeshProUGUI txtPrice;
    public TextMeshProUGUI txtPaidPrice;
    public Button button;

    private BuildingSystem buildingSystem;

    private void Awake()
    {
        buildingSystem = FindObjectOfType<BuildingSystem>();
    }

    public void SetItem(BuildingTypeSO building, UnityAction callback)
    {
        txtName.text = building.nameString;
        icon.sprite = building.icon;
        txtPrice.text = building.price.ToString();
        txtPaidPrice.text = building.paidPrice.ToString();
        button.onClick.AddListener(
            () =>
            {
                buildingSystem.SetBuilding(building);
                callback();
            });
    }
}
