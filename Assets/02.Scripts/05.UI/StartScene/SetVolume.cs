using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat("BGM", 0.75f);
    }

    public void SetLevel(float sliderValue)
    {
        mixer.SetFloat("BGM", Mathf.Log10(sliderValue)*20);
        PlayerPrefs.SetFloat("BGM", sliderValue);
    }
}
