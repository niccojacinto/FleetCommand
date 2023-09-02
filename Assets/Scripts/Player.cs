using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

   public static Player Instance;

    int resources = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddResources(int amount)
    {
        resources += amount;
        HUD.Instance.SetResources(resources);
    }

    public void DeductResources(int amount)
    {
        resources -= amount;
        HUD.Instance.SetResources(resources);
    }


}
