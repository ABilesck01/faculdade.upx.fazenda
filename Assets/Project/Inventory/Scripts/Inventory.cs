using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Inventory
{
    private List<ItemInInventory> itemList;

    public int MaxAmount = 14;

    public event EventHandler OnListUpdate;

    public Inventory()
    {
        itemList = new List<ItemInInventory>();
    }

    public void RemoveItem(ItemSO item)
    {
        if (item.IsStackable)
        {
            bool hasItem = false;
            foreach (ItemInInventory it in GetItemList())
            {
                if (it.item == item)
                {
                    it.Amount -= item.Amount;
                    hasItem = true;
                }
            }
            if (!hasItem)
            {
                ItemInInventory itemInInventory = itemList.Find(i => i.item.Equals(item));

                itemList.Remove(itemInInventory);
            }
        }
        else
        {
            ItemInInventory itemInInventory = itemList.Find(i => i.item.Equals(item));

            itemList.Remove(itemInInventory);
        }
        OnListUpdate?.Invoke(this, EventArgs.Empty);
    }

    public void AddItem(ItemSO item)
    {
        if(item.IsStackable)
        {
            bool hasItem = false;
            foreach (ItemInInventory it in GetItemList())
            {
                if(it.item == item)
                {
                    it.Amount += item.Amount;
                    hasItem = true;
                }
            }
            if(!hasItem)
            {
                itemList.Add(new ItemInInventory() 
                { 
                    item = item, 
                    Amount = item.Amount
                });
            }
        }
        else
        {
            itemList.Add(new ItemInInventory()
            {
                item = item,
                Amount = item.Amount
            });
        }
        OnListUpdate?.Invoke(this, EventArgs.Empty);
    }

    public bool InventoryIsFull()
    {
        return itemList.Count >= MaxAmount;
    }

    public List<ItemInInventory> GetItemList()
    {
        return itemList;
    }
}
