using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class PlantingItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lblName;
    [SerializeField] private TextMeshProUGUI lblMoney;
    [SerializeField] private Image icon;
    [SerializeField] private Button btnBuy;

    public void SetItem(SeedSO seed, UnityAction buyAction)
    {
        lblName.text = seed.SeedName;
        lblMoney.text = seed.BuyCost.ToString();
        icon.sprite = seed.Icon;
        btnBuy.onClick.AddListener(buyAction);
    }
}
