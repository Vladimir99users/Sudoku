using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_panel_menu : MonoBehaviour
{

    public Text _time;

    public void DisplayText_pause()
    {
        _time.text = Clock_Sudoku._instance.GetCurrentTimerText().text;
    }

}
