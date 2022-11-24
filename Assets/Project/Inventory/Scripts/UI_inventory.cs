using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_inventory : MonoBehaviour
{
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemTemplate;

    private Inventory inventory;

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;

        this.inventory.OnListUpdate += Inventory_OnListUpdate;

        RefreshItems();
    }

    private void Inventory_OnListUpdate(object sender, System.EventArgs e)
    {
        RefreshItems();
    }

    private void RefreshItems()
    {
        foreach (Transform item in itemSlotContainer)
        {
            if (item == itemTemplate) continue;
            Destroy(item.gameObject);
        }
        foreach (ItemInInventory item in inventory.GetItemList())
        {
            RectTransform itemFrame = Instantiate(itemTemplate,
                itemSlotContainer).GetComponent<RectTransform>();

            itemFrame.GetComponent<UI_inventoryItem>().
                Setup(item.item.Icon, item.item.Name,
                item.item.price.ToString(), item.Amount.ToString());

            itemFrame.gameObject.SetActive(true);
        }

    }
}
