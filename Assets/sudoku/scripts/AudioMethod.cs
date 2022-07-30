using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMethod : AudioManadger
{

    private void Awake()
    {
   
        StartingVolume();
    }

    new public void  PlayAudioforButton()
    {
        _sourcebutton.PlayOneShot(button_click);
    }
}
