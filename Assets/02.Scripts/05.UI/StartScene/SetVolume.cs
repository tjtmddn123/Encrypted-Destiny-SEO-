using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public Slider slider;

    public void AudioControl()
    {
        float volume = slider.value;

        if (volume == -40f)
        {
            mixer.SetFloat("BGM", -80);
            mixer.SetFloat("Effect", -80);
        }
        else
        {
            mixer.SetFloat("BGM", volume);
            mixer.SetFloat("Effect", volume);
        }
    }

    public void ToggleAudioVolume()
    {
        float currentVolume = AudioListener.volume;
        AudioListener.volume = AudioListener.volume == 0 ? 1 : 0;
    }
}
