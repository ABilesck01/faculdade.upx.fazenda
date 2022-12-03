using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class UI_inventoryItem : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private TextMeshProUGUI txtAmount;
    [SerializeField] private Button btnSell;

    public void Setup(Sprite sprite, string name, string price, string amount,
        UnityAction sellAction)
    {
        icon.sprite = sprite;
        txtName.text = name;
        txtPrice.text = price;
        txtAmount.text = amount;
        btnSell.onClick.AddListener(sellAction);
    }

}
