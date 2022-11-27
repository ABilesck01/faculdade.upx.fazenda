using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AnimalListItem : MonoBehaviour
{
    public TextMeshProUGUI txtName;
    public TextMeshProUGUI txtFeed;
    public Button btnAction;

    public void Fill(string name, string feed, UnityAction action)
    {
        txtName.text = name;
        txtFeed.text = feed;
        if (feed.Equals("Pastando"))
            btnAction.interactable = false;
        else
            btnAction.onClick.AddListener(action);
    }
}
