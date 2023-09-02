using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{

    static Dialogues instance;
    public static Dialogues Instance { get { return instance;} }
    [SerializeField]
    AudioClip[] dialogueClips;
    Dictionary<string, AudioClip> dialogueDict;
    public Dictionary<string, AudioClip> DialogueDict { get { return dialogueDict;} }

    private void Awake()
    {
        if (instance == null || instance != this)
        {
            if (instance) Destroy(instance.gameObject);
            instance = this;
        }
    }
    private void Start()
    {
        dialogueDict = new Dictionary<string, AudioClip>();
        foreach(AudioClip clip in dialogueClips)
        {
            // Debug.Log(string.Format("[ {0} ] : {1}", clip.name, clip));
            dialogueDict.Add(clip.name, clip);
        }
    }
}
