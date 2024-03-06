using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance { get; set; }

    public UnityEvent OnDayPass = new UnityEvent();
    public Text DayUI;

    private void Update()
    {
        DayUI.text = $"{dayInGame}";
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public int dayInGame = 1;

    public void TriggerNextDay()
    {
        dayInGame += 1;

        OnDayPass.Invoke();
    }
}
