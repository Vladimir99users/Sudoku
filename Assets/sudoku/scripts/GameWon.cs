using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameWon : MonoBehaviour
{
    public GameObject winPanel;
    public Text TimerWinText;

    private void Start()
    {
        winPanel.SetActive(false);
        TimerWinText.text = Clock_Sudoku._instance.GetCurrentTimerText().text;
    }

    private void OnBoardCompledet()
    {
        winPanel.SetActive(true);
        TimerWinText.text = Clock_Sudoku._instance.GetCurrentTimerText().text;
    }

    private void OnEnable()
    {
        GameEvents._OnBoardComplited += OnBoardCompledet;
    }

    private void OnDisable()
    {
        GameEvents._OnBoardComplited -= OnBoardCompledet;
    }

    
    
}   
    