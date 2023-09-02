using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public static class Helpers
{
    public static void Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n+1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    public static bool Chance(float probabilityOfTriggering, UnityAction action )
    {
        if (Random.Range(0f, 100f) < probabilityOfTriggering)
        {
            action.Invoke();
            return true;
        }
        return false;
    }

    public static Vector3 GetRandomVector()
    {
        Quaternion rot = Random.rotation;

        // For my old wave design where the cruiser goes in a straight line
        // return new Vector3(rot.x, rot.y, 0).normalized;


        int negX = Random.Range(0, 2) == 0 ? -1 : 1;
        int negY = Random.Range(0, 2) == 0 ? -1 : 1;
        int negZ = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector3 direction = new Vector3(rot.eulerAngles.x * negX, rot.eulerAngles.y * negY, rot.eulerAngles.z * negZ).normalized;
        return direction;

    }

    public static float ToSeconds(int minutes, int seconds, int milliseconds)
    {
        return (minutes * 60f) + seconds + (milliseconds / 100f);
    }

    public static string[] Opposing(string tag) 
    {
        switch (tag)
        {
            case "UNSA":
                return new string[]{"Umbra", "Debris" };
            case "Umbra":
                return new string[] {"UNSA"};
            default:
                return new string[] { };
        }
    }

}
