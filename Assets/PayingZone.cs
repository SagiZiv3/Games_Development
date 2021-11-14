using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayingZone : MonoBehaviour
{
    [SerializeField] private Animator sellerAnimator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            sellerAnimator.ResetTrigger("Stop_Talking");
            sellerAnimator.SetTrigger("Talking");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            sellerAnimator.ResetTrigger("Talking");
            sellerAnimator.SetTrigger("Stop_Talking");
        }
    }
}
