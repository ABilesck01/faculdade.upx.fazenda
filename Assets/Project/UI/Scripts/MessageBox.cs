using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MessageBox : MonoBehaviour
{
    public static MessageBox Instance;

    [SerializeField] private TextMeshProUGUI lblMessage;
    [SerializeField] private GameObject messageBox;
    [SerializeField] private ModalController modalController;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowMessage(string message)
    {
        StartCoroutine(c_ShowMessage(message));
    }

    private IEnumerator c_ShowMessage(string message)
    {

        //messageBox.SetActive(true);
        modalController.OpenModal();
        lblMessage.text = message;
        yield return new WaitForSeconds(1.75f);
        modalController.CloseModal();
    }
}
