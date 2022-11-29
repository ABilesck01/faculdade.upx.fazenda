using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SearchItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtName;
    [SerializeField] private TextMeshProUGUI txtDescription;
    [SerializeField] private TextMeshProUGUI txtPrice;
    [SerializeField] private Button btnBuy;
    
    public void Fill(UnityAction action, SearchSO item)
    {
        txtName.text = item.Name;
        txtDescription.text = item.Description;
        txtPrice.text = item.Price.ToString();
        btnBuy.onClick.AddListener(action);
    }
}
