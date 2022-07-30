using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Clock_Sudoku : MonoBehaviour
{
   private int _hour = 0;
   private int _minut = 0;
   private int _seconds = 0;

    private Text textClockTime;
    private float delta_time;
    private bool stop_clock_sudoku = false;

    public static Clock_Sudoku _instance;


    private void Awake()
    {
        if (_instance) Destroy(_instance);

        _instance = this;
        textClockTime = GetComponent<Text>();

        if (Game_settings.Instance.GetContinuePreviousGame())
        {
            delta_time = Configuration.ReadGameTime();
        }
        else
        {
            delta_time = 0;
        }
    }

    private void Start()
    {
        stop_clock_sudoku = false;
    }

    private void Update()
    {
        if( Game_settings.Instance.GetPaused() ==false  && stop_clock_sudoku == false )
        {
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);

            string Hours = LeadingZero(span.Hours);
            string Minutes = LeadingZero(span.Minutes);
            string Seconds = LeadingZero(span.Seconds);

            textClockTime.text = Hours + ":" + Minutes + ":" + Seconds;
        }
    }

    public static string GetCurrentTime()
    {
        return _instance.delta_time.ToString();
    }

   public string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    public void OnGameOverPlayer()
    {
        stop_clock_sudoku = true;
    }

    private void OnEnable()
    {
        GameEvents._OnGameOver += OnGameOverPlayer;
    }


    private void OnDisable()
    {
        GameEvents._OnGameOver -= OnGameOverPlayer;
    }
        

    public Text GetCurrentTimerText()
    {
        return textClockTime;
    }

    public void StartClock()
    {
        stop_clock_sudoku = false;
    }
}
