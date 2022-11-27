using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnimalUIItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private Button btnBuy;
    
    public void FillItem(AnimalSO animal, UnityAction btnBuyCallback)
    {
        txtName.text = animal.name;
        txtPrice.text = animal.Price.ToString();
        btnBuy.onClick.AddListener(btnBuyCallback);
    }
}
