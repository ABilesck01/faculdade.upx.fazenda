using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private UI_inventory ui_Inventory;
    [SerializeField] private Inventory inventory;

    private void Awake()
    {
        ui_Inventory = FindObjectOfType<UI_inventory>();
        inventory = new Inventory();
        
    }

    private void Start()
    {
        ui_Inventory.SetInventory(inventory);
    }

    public void GetItem(ItemSO item)
    {
        inventory.AddItem(item);
    }

    public void RemoveItem(ItemSO item)
    {
        inventory.RemoveItem(item);
    }
}
