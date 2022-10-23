using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class UI_inventory : MonoBehaviour
{
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private Transform itemTemplate;
    [SerializeField] private Transform emptyTemplate;
    [Space]
    [SerializeField] private ModalController modal;

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
            if (item == itemTemplate || item == emptyTemplate) continue;
            Destroy(item.gameObject);
        }
        foreach (ItemInInventory item in inventory.GetItemList())
        {
            RectTransform rectTransform = Instantiate(itemTemplate,
                itemSlotContainer).GetComponent<RectTransform>();
            rectTransform.gameObject.SetActive(true);

            rectTransform.Find("icon").GetComponent<Image>().sprite = item.item.Icon;
            if (item.item.IsStackable)
            {
                rectTransform.Find("amount").GetComponent<TextMeshProUGUI>().text =
                    item.Amount.ToString();
            }
            else
            {
                rectTransform.Find("amount").GetComponent<TextMeshProUGUI>().text = "";

            }
    }

    int amount = inventory.GetItemList().Count;
        int max = inventory.MaxAmount;

        for (int i = 0; i < (max - amount); i++)
        {
            RectTransform rectTransform = Instantiate(emptyTemplate,
                itemSlotContainer).GetComponent<RectTransform>();
            rectTransform.gameObject.SetActive(true);
        }
    }
}
