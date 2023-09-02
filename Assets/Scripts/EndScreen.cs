using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScreen : MonoBehaviour
{

    public static EndScreen Instance;

    [SerializeField]
    GameObject defeatPanel;
    [SerializeField]
    GameObject restartBtn;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void ShowDefeat()
    {
        Time.timeScale = 0f;
        Cursor.visible = true;
        MusicManager.Instance.StopMusic();
        defeatPanel.SetActive(true);
        restartBtn.SetActive(true);
    }

}
