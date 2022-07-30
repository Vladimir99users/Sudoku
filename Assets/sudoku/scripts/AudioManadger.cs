using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;



public class AudioManadger : MonoBehaviour
{

    public AudioMixerGroup _mixer;
    public AudioClip button_click;
    public AudioClip music_background;

    public AudioSource _sourcebutton;
    public AudioSource _sourceBackground;

    public Toggle Music_toggle;
    public Toggle Sound_toggle;


    public int music_bool = 1;
    public int sound_bool = 1;

    public static AudioManadger _instance;

    void Start()
    {
        


        if (_instance) Destroy(_instance);
        _instance = this;


        StartingVolume();
        //  Debug.Log("Music_toogle" + Music_toggle.isOn);
        //  Debug.Log("sound_toogle" + Sound_toggle.isOn);
        SetVoluemMusic();
        SetVoluemSound();
    }

    public void StartingVolume()
    {

        _sourceBackground.PlayOneShot(music_background);
        if (PlayerPrefs.HasKey("Music") && PlayerPrefs.HasKey("Sound"))
        {
            music_bool = PlayerPrefs.GetInt("Music");
            sound_bool = PlayerPrefs.GetInt("Sound");

            if (music_bool == 0)
            {
                Music_toggle.isOn = true;
            }
            else
            {
                Music_toggle.isOn = false;
            }

            if (sound_bool == 0)
            {
                Sound_toggle.isOn = true;
            }
            else
            {
                Sound_toggle.isOn = false;
            }

        }
        else
        {
            PlayerPrefs.SetInt("Music", 0);
            PlayerPrefs.SetInt("Sound", 0);
        }
    }

    public void SetVoluemMusic()
    {
        
        _mixer.audioMixer.SetFloat("Music", music_bool);
    }

    public void SetVoluemSound()
    {

        _mixer.audioMixer.SetFloat("Sound", sound_bool);
    }


    public void SetMusicToggle()
    {
        if (Music_toggle.isOn)
        {
            PlayerPrefs.SetInt("Music", 0);
            music_bool = PlayerPrefs.GetInt("Music");
        }
        else
        {
            PlayerPrefs.SetInt("Music", -80);
            music_bool = PlayerPrefs.GetInt("Music");
        }
        SetVoluemMusic();
       // Debug.Log("Music_toogle" + Music_toggle.isOn + "Method");
    }


    public void SetSoundToggle()
    {
        if (Sound_toggle.isOn)
        {
            PlayerPrefs.SetInt("Sound", 0);
            sound_bool = PlayerPrefs.GetInt("Sound");
        }
        else
        {
            PlayerPrefs.SetInt("Sound", -80);
            sound_bool = PlayerPrefs.GetInt("Sound");
        }
        SetVoluemSound();
        //Debug.Log("sound_toogle" + Sound_toggle.isOn + "Method");
    }


    public void PlayAudioforButton()
    {
        _sourcebutton.PlayOneShot(button_click);
    }

}
