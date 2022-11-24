using System;
using System.Collections.Generic;

[Serializable]
public class Inventory
{
    private List<ItemInInventory> itemList;

    public int MaxAmount;

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
        return GetCurrentAmount() >= MaxAmount;
    }

    public int GetCurrentAmount()
    {
        int amount = 0;

        foreach (ItemInInventory item in itemList)
        {
            amount += item.Amount;
        }

        return amount;
    }

    public List<ItemInInventory> GetItemList()
    {
        return itemList;
    }
}
