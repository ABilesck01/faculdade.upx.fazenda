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

    public void OpenModal()
    {
        if(animator != null)
        {
            OpenModalWithAnimation();
        }
    }

    public void CloseModal()
    {
        if(animator != null)
        {
            CloseModalWithAnimation();
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
