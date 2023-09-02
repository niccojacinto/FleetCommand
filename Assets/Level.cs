using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ElapsedTime))]
public class Level : MonoBehaviour
{
    ElapsedTime timeHandler;
    private void Awake()
    {
        timeHandler = GetComponent<ElapsedTime>();
    }

    private void Start()
    {
        SetupLevel();
    }

    void SetupLevel()
    {
        timeHandler.QueueEvent(Helpers.ToSeconds(0, 3, 0), () =>
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                temp.Add("Drone");
                
                // temp.Add("Asteroid");
                // temp.Add("Big Asteroid");
                WaveSpawner.Instance.spawnRate = 0.0f;
            }
            temp.Add("Hippogriff");
            temp.Add("Hippogriff");
            temp.Add("Hippogriff");
            temp.Add("Hippogriff");
            temp.Add("Hippogriff");
            temp.Add("Hippogriff");
            temp.Add("Hippogriff");
            Helpers.Shuffle(temp);
            WaveSpawner.Instance.QueueList(temp);
            HUD.Instance.Say("AWACS Hopper", "Griffin-X1, Finish your patrol rounds and prepare for escort.", Dialogues.Instance.DialogueDict["AWACS-Griffin_x1"], true);
            temp.Clear();
        });

        timeHandler.QueueEvent(Helpers.ToSeconds(0, 10, 0), () =>
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                //temp.Add("Asteroid");
                //temp.Add("Big Asteroid");
                // temp.Add("Iris");
                temp.Add("Ocula");

            }
            WaveSpawner.Instance.spawnRate = 0.2f;
            Helpers.Shuffle(temp);
            WaveSpawner.Instance.QueueList(temp);
            MusicManager.Instance.StartMusic();
            // HUD.Instance.Say("Hippogriff Pilot", "Taking Fire", Dialogues.Instance.DialogueDict["cduckett-04-taking_fire"], true);
            temp.Clear();
        });

        timeHandler.QueueEvent(Helpers.ToSeconds(0, 20, 0), () =>
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                temp.Add("Asteroid");
                temp.Add("Big Asteroid");

            }
            WaveSpawner.Instance.spawnRate = 0.5f;
            Helpers.Shuffle(temp);
            WaveSpawner.Instance.QueueList(temp);
            // HUD.Instance.Say("Hippogriff Pilot", "Ahh!", Dialogues.Instance.DialogueDict["cduckett-05-scream"], true);
            temp.Clear();
        });

        timeHandler.QueueEvent(Helpers.ToSeconds(0, 20, 0), () =>
        {
            List<string> temp = new List<string>();
            for (int i = 0; i < 200; i++)
            {
                temp.Add("Drone");

            }
            WaveSpawner.Instance.spawnRate = 0.5f;
            Helpers.Shuffle(temp);
            WaveSpawner.Instance.QueueList(temp);
            temp.Clear();
        });
    }
}
