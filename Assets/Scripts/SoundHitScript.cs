using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHitScript : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip defaultSound;
    [SerializeField] private AudioClip hardSound;
    private AudioClip currentAudioClip;
    private Rigidbody rigidBody;
    private float speed;


    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rigidBody != null)
        {
            speed = rigidBody.velocity.magnitude;
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
