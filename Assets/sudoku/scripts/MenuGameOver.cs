using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameOver : MonoBehaviour
{

    public Text textClockTime;
    void Start()
    {
        textClockTime.text = Clock_Sudoku._instance.GetCurrentTimerText().text;
    }

}
