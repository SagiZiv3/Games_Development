using NewCode.Characters.Health;
using UnityEngine;

// Simple class that handles the grenade effect
public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700;

    public AudioClip explosionAudio;
    public GameObject explosionEffect;

    float countDown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {
        // Reset the count down timer
        countDown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the count down timer
        countDown -= Time.deltaTime;

        // If the count down timer reached zero, and the grenade didn't explode yet
        if (countDown <= 0 && !hasExploded)
        {
            // Make the explosion, and update hasExploded
            Explode();
            hasExploded = true;
        }
    }

    private void Explode()
    {
        // Instantiate the particle effects
        GameObject fx = Instantiate(explosionEffect, transform.position, transform.rotation);
        // Search for colliders that should be affected from the explosion
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        // Add an audio source to the effects
        AudioSource fxAudio = fx.AddComponent<AudioSource>();
        // Set the clip
        fxAudio.clip = explosionAudio;
        // Make sure it is 3D sound, to make it more realistic
        fxAudio.spatialBlend = 1;
        fxAudio.Play();

        foreach (Collider nearbyObject in colliders)
        {
            // If the collider has a Rigidbody, apply explosion physics on it
            if (nearbyObject.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.AddExplosionForce(force, transform.position, radius);
            }

            // If the collider has an Health component, damage it
            if (nearbyObject.TryGetComponent(out Health characterHealth))
            {
                // The damage is linear to the distance of the collider from the explosion source.
                // If the collider is close to the source, the damage would be 70, and if far from
                // the source the damage would be 10.
                characterHealth.DecreaseHealth(Mathf.RoundToInt(Mathf.Lerp(70, 10,
                    Vector3.Distance(transform.position, nearbyObject.transform.position) / radius)));
            }
        }

        // And destroy the grenade, because we don't need it anymore
        Destroy(gameObject);
        Destroy(fx, 3f);
    }
}