using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        //  So that its not abrupt
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }
}
