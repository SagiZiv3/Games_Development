using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private AudioClip openSfx, closeSfx;
    [SerializeField] private AudioSource audioSource;
    private int counter = 0;
    private static readonly int IsOpenAnimatorVariable = Animator.StringToHash("is_open");


    public void TransformEntered(Transform _)
    {
        counter++;
        animator.SetBool(IsOpenAnimatorVariable, true);
        audioSource.Stop();
        audioSource.PlayOneShot(openSfx);
    }

    public void TransformExited(Transform _)
    {
        counter -= 1;
        if (counter != 0) return;
        animator.SetBool(IsOpenAnimatorVariable, false);
        audioSource.Stop();
        audioSource.PlayOneShot(closeSfx);
    }
}