using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BriefingCaptions : MonoBehaviour
{
    static BriefingCaptions instance;
    public static BriefingCaptions Instance { get { return instance; } }

    [SerializeField]
    Caption captionHolder;
    [SerializeField]
    AudioSource captionSource;

    Coroutine speakingCR;

    private void Awake()
    {
        if (instance != null || instance != this)
        {
            instance = this;
        }
    }

    public void Say(string speaker, string message, AudioClip clip, bool isPriority)
    {
        if (isPriority && speakingCR != null)
        {
            StopCoroutine(speakingCR);
        }

        speakingCR = StartCoroutine(SayCR(speaker, message, clip, isPriority));
    }
    // If priority is true.. stops all playing dialogue and plays clip
    public IEnumerator SayCR(string speaker, string message, AudioClip clip, bool isPriority)
    {
        if (isPriority)
        {
            captionSource.Stop();
        }
        else
        {
            if (captionSource.isPlaying) yield break;
        }
        captionHolder.gameObject.SetActive(true);
        captionHolder.textSpeaker.text = speaker;
        captionHolder.textDialogue.text = string.Format("<< {0} >>", message); ;
        captionSource.clip = clip;
        captionSource.Play();
        if (clip == null)
        {
            yield return new WaitForSeconds(5f);
        }
        else
        {
            yield return new WaitForSeconds(clip.length);
        }
        captionHolder.gameObject.SetActive(false);
    }
}
