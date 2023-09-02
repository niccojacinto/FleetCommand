using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    AudioSource source;
    [SerializeField]
    AudioClip bounceSFX;
    [SerializeField]
    AudioClip huhuSFX;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayBounce()
    {
        source.PlayOneShot(bounceSFX);
    }

    public void PlayHuhu()
    {
        source.volume = 0.5f;
        source.PlayOneShot(huhuSFX);
    }

    public void LoadMain()
    {
        SceneManager.LoadScene(1);
    }


}
