using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModalController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
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
        animator.SetBool("open", true);
    }

    private void CloseModalWithAnimation()
    {
        animator.SetBool("open", false);
    }
}
