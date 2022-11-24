using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalController : MonoBehaviour
{
    private Animator animator;

    [Header("Background - Optional")]
    [SerializeField] private GameObject background;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if(background != null)
            background.SetActive(false);
    }

    [ContextMenu("Open")]
    public void OpenModal()
    {
        if(animator != null)
        {
            OpenModalWithAnimation();
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    [ContextMenu("Close")]
    public void CloseModal()
    {
        if(animator != null)
        {
            CloseModalWithAnimation();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OpenModalWithAnimation()
    {
        if (background != null)
            background.SetActive(true);
        animator.SetBool("open", true);
    }

    private void CloseModalWithAnimation()
    {
        if (background != null)
            background.SetActive(false);
        animator.SetBool("open", false);
    }
}
