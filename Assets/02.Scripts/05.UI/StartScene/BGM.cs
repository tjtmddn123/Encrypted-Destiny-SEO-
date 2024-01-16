using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGM : MonoBehaviour
{
    public AudioSource audioSource;
    public GameObject[] musics;

    public void Awake()
    {
        musics = GameObject.FindGameObjectsWithTag("BGM");

        if(musics.Length >= 2 )
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(transform.gameObject);
        audioSource = GetComponent<AudioSource>();
    }
    public void PlayBGM()
    {
        if (audioSource.isPlaying) return;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
}
