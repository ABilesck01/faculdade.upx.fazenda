using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_inventoryItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private TextMeshProUGUI txtAmount;

    public void Setup(Sprite sprite, string name, string price, string amount)
    {
        icon.sprite = sprite;
        txtName.text = name;
        txtPrice.text = price;
        txtAmount.text = amount;
    }

}
