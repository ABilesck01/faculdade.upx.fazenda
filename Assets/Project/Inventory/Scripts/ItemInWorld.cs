using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemInWorld : MonoBehaviour
{
    [SerializeField] private ItemSO item;
    [SerializeField] private Transform rotationTranform;
    [SerializeField] private float rotationSpeed;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerInventory>().GetItem(item);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(rotationTranform != null)
        {
            rotationTranform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
        }
    }
}
