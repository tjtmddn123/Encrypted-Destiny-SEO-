using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip openClip;
    public AudioClip closeClip;
    public AudioClip openCase;
    public AudioClip closeCase;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SFXPlay(string sfxname,AudioClip clip, float volume = 0.8f)
    {
        GameObject go = new GameObject(sfxname+"Sound");
        AudioSource audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(go,clip.length);
    }
    public void PlayDoorSound(bool isOpen)
    {
        AudioClip soundClip = isOpen ? openClip : closeClip;
        SFXPlay(isOpen ? "Open" : "Close", soundClip);
    }
    public void PlayCaseSound(bool isOpen)
    {
        AudioClip soundClip = isOpen ? openCase : closeCase;
        SFXPlay(isOpen ? "Openn" : "Closee", soundClip);
    }
}
