using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public float speed;
    public bool lookAt = true;
    private void Start()
    {
        StartCoroutine(ChangeCamPosition());
    }
    void Update()
    {
        Camera.main.transform.position += transform.forward * speed * Time.deltaTime;
        if (lookAt == true) Camera.main.transform.LookAt(transform);
    }

    public IEnumerator ChangeCamPosition()
    {
        float maxDist = 10f;
        Vector3 targetPosition = Vector3.zero;
        while (true)
        {
            Vector3 dirVector = Helpers.GetRandomVector();
            Camera.main.transform.position = transform.position + dirVector * Random.Range(maxDist-5f, maxDist);
            maxDist += 10f;
            if (maxDist > 100) maxDist = 100f;
            yield return new WaitForSeconds(10f);
        }
    }
}
