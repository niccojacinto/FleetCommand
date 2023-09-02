using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reticle : MonoBehaviour
{

    Camera cam;
    public RectTransform reticle;

    public static Reticle Instance;
    [SerializeField]
    RectTransform targetingBounds;

    private void Awake()
    {
        reticle = GetComponent<RectTransform>();    
        if (!Instance)
        {
            Instance = this;
        }
    }

    void Start()
    {
        cam = Camera.main;
        Cursor.visible = false;
    }

    void Update()
    {
        float magLimit = 10f;
        Vector3 mouseVector = Input.mousePosition - targetingBounds.position;
        // DebugUI.Instance.LogUpdate1(Input.mousePosition);
        if (mouseVector.magnitude > magLimit)
        {
            reticle.position = targetingBounds.position + mouseVector.normalized * magLimit;
        } else
        {
            reticle.position = Input.mousePosition;
        }


    }

}
