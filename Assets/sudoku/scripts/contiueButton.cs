using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class contiueButton : MonoBehaviour
{

    public Text TimeText;
    public Text LevelText;

    public Text Error;

    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }
    // Start is called before the first frame update
    void Start()
    {
        
        if(Configuration.GameFileExist() == false)
        {
            gameObject.GetComponent<Button>().interactable = false;
            TimeText.text = "  ";
            LevelText.text = "  ";
            Error.text = "  ";
        } else
        {
            float delta_Time = Configuration.ReadGameTime();
            
            delta_Time += Time.deltaTime;
          
            TimeSpan span = TimeSpan.FromSeconds(delta_Time);
            

            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);


            TimeText.text = hour + ":" + minute + ":" + seconds;

            LevelText.text = Configuration.ReadBoardLevel() + " ";
            Error.text = "Errors " + Configuration.ErrorNumber();

            //
        }


    }

    public void SetGameData()
    {
        Game_settings.Instance.SetGameMode(Configuration.ReadBoardLevel());
    }
}
