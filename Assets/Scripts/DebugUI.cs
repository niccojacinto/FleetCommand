using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class DebugUI : MonoBehaviour, ILogger
{
    public static DebugUI Instance { get; private set; }
    public ILogHandler logHandler { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public bool logEnabled { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    public LogType filterLogType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    [SerializeField]
    Reticle reticle;

    bool isActive = true;
    [Header("Panels")]
    [SerializeField]
    GameObject[] panels;

    [Header("Mouse Pointer Debug")]
    [SerializeField]
    Transform mouseDebugPanel;
    [SerializeField]
    TMP_Text mouseScreenPos;
    [SerializeField]
    TMP_Text mouseWorldPos;
    [SerializeField]
    TMP_Text mouseViewportPos;


    [Header("Log Update")]
    [SerializeField]
    TMP_Text debugUpdate1;
    [SerializeField]
    TMP_Text debugUpdate2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Start()
    {
        isActive = false;
        foreach (GameObject panel in panels)
        {
            panel.SetActive(isActive);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isActive = !isActive;
            Debug.Log("DebugUI Active: " + isActive);
            foreach (GameObject panel in panels)
            {
                panel.SetActive(isActive);
            }
        }

        if (!isActive) return;
        // Mouse Coordinates
        mouseDebugPanel.position = Input.mousePosition + (Vector3.right * 100f);
        //mouseScreenPos.text = string.Format("Screen: {0}", reticle.ScreenPos);
        //mouseWorldPos.text = string.Format("World: {0}", reticle.WorldPos);
        //mouseViewportPos.text = string.Format("View: {0}", reticle.ViewportPos);
    }

    public bool IsLogTypeAllowed(LogType logType)
    {
        throw new NotImplementedException();
    }

    public void Log(LogType logType, object message)
    {
        throw new NotImplementedException();
    }

    public void Log(LogType logType, object message, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }

    public void Log(LogType logType, string tag, object message)
    {
        throw new NotImplementedException();
    }

    public void Log(LogType logType, string tag, object message, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }

    public void Log(object message)
    {

    }

    public void LogUpdate1(object message)
    {
        debugUpdate1.text = message.ToString();
    }

    public void LogUpdate2(object message)
    {
        debugUpdate2.text = message.ToString();
    }

    public void Log(string tag, object message)
    {
        throw new NotImplementedException();
    }

    public void Log(string tag, object message, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }

    public void LogWarning(string tag, object message)
    {
        throw new NotImplementedException();
    }

    public void LogWarning(string tag, object message, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }

    public void LogError(string tag, object message)
    {
        throw new NotImplementedException();
    }

    public void LogError(string tag, object message, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }

    public void LogFormat(LogType logType, string format, params object[] args)
    {
        throw new NotImplementedException();
    }

    public void LogException(Exception exception)
    {
        throw new NotImplementedException();
    }

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
        throw new NotImplementedException();
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        throw new NotImplementedException();
    }
}
