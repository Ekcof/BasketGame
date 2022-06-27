using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHitScript : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip defaultSound;
    [SerializeField] private AudioClip hardSound;
    private AudioClip currentAudioClip;
    private Rigidbody rigidbody;
    private float speed;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        // TODO: Avoid reverse counting if player throw ball from beneath
        if (rigidbody != null)
        {
            speed = rigidbody.velocity.magnitude;
            if (speed > 0.5f)
            {
                if (speed > 3f)
                {
                    currentAudioClip = hardSound;
                }
                else
                {
                    currentAudioClip = defaultSound;
                }
                audioSource.clip = currentAudioClip;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Play();
        }
    }
}
