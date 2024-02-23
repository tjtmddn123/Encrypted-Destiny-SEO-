using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footstep : MonoBehaviour
{
    public AudioSource footstep;
    public float normalSpeed = 1.0f;
    public float fastSpeed = 1.5f;

    private bool isRunning = false;
    private bool wasRunning = false;

    void Update()
    {
        bool isMoving = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");

        wasRunning = isRunning;

        isRunning = Input.GetKey(KeyCode.LeftShift);

        if (isRunning && !wasRunning)
        {
            PlayFootsteps(fastSpeed);
        }
        else if (!isRunning && wasRunning)
        {
            PlayFootsteps(normalSpeed);
        }
        else if (isMoving)
        {
            PlayFootsteps(isRunning ? fastSpeed : normalSpeed);
        }
        else
        {
            StopFootsteps();
        }
    }

    void PlayFootsteps(float speedMultiplier)
    {
        if (!footstep.isPlaying)
        {
            footstep.pitch = speedMultiplier;
            footstep.Play();
        }
    }

    void StopFootsteps()
    {
        if (footstep.isPlaying)
        {
            footstep.Stop();
        }
    }
}