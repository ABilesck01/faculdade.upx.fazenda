using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBarn : MonoBehaviour
{
    private ModalController inventoryModal;

    private void Start()
    {
        inventoryModal = GameObject.Find("InventoryUI").GetComponent<ModalController>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inventoryModal.OpenModal();
        }
    }
}
