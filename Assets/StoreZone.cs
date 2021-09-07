using UnityEngine;

public class StoreZone : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            audioSource.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            audioSource.Stop();
    }
}