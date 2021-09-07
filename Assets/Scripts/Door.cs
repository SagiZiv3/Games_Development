using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip openSfx, closeSfx;
    [SerializeField] private AudioSource audioSource;
    private int counter = 0;

    private void Start()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        counter++;
        animator.SetBool("is_open", true);
        audioSource.Stop();
        audioSource.PlayOneShot(openSfx);
    }

    private void OnTriggerExit(Collider other)
    {
        counter -= 1;
        if (counter == 0)
        {
            animator.SetBool("is_open", false);
            audioSource.Stop();
            audioSource.PlayOneShot(closeSfx);
        }
    }
}
