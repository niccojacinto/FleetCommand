using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    AudioSource source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        source = GetComponent<AudioSource>();
    }

    public void StartMusic()
    {
        source.Play();
    }
    public void StopMusic()
    {
        source.Stop();
    }
}
