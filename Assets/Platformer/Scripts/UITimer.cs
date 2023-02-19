using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    
    private const int StartTime = 400;
    
    private int _timerValue;
    private float _timeOfRestart;
    // Start is called before the first frame update
    void Start()
    {
        RestartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        var timePassed = Time.realtimeSinceStartup - _timeOfRestart;
        _timerValue = (int)(StartTime - timePassed);
        timerText.text = $"TIME\n{_timerValue}";
    }

    private void RestartTimer()
    {
        _timerValue = StartTime;
        _timeOfRestart = Time.realtimeSinceStartup;
    }
}
