using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SevenSound : MonoBehaviour
{
    public AudioSource audioSource;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlaySound", 300f); 
    }

    private void PlaySound()
    {
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        
        audioSource.Play();

        InvokeRepeating("PlaySound", 420f, 420f); 
    }
}