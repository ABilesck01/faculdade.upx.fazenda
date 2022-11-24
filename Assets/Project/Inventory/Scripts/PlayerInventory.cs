using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private UI_inventory ui_Inventory;
    [SerializeField] private Inventory inventory;
    [SerializeField] private int maxAmount;

    private void Awake()
    {
        ui_Inventory = FindObjectOfType<UI_inventory>();
        inventory = new Inventory()
        {
            MaxAmount = this.maxAmount
        };
        
    }

    private void Start()
    {
        ui_Inventory.SetInventory(inventory);
    }

    public bool AddItem(ItemSO item)
    {
        Debug.Log(inventory.InventoryIsFull());

        if (inventory.InventoryIsFull())
        {
            MessageBox.Instance.ShowMessage("Inventário está cheio! Construa mais cilos");

            return false;
        }
        
        inventory.AddItem(item);
        return true;
    }

    public void RemoveItem(ItemSO item)
    {
        inventory.RemoveItem(item);
    }
}
