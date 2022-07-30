using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManuButtens : MonoBehaviour
{ 

    public void Load_scene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void Load_Easy_game(string name)
    {
        Game_settings.Instance.SetGameModeD(Game_settings.EGameMode.Easy);
        /////////
       // Configuration.DeleteDataFile();
        ////////
        SceneManager.LoadScene(name);
    }

    public void Load_Medium_game(string name)
    {
        Game_settings.Instance.SetGameModeD(Game_settings.EGameMode.Medium);
        /////////
       // Configuration.DeleteDataFile();
        ////////
        SceneManager.LoadScene(name);
    }

    public void Load_Hard_game(string name)
    {
        Game_settings.Instance.SetGameModeD(Game_settings.EGameMode.Hard);
        /////////
        //Configuration.DeleteDataFile();
        ////////
        SceneManager.LoadScene(name);
    }

    public void Load_VeryHard_game(string name)
    {
        Game_settings.Instance.SetGameModeD(Game_settings.EGameMode.VeryHard);
        /////////
     //   Configuration.DeleteDataFile();
        ////////
        SceneManager.LoadScene(name);
    }

    public void activete_Object(GameObject obj)
    {
        obj.SetActive(true);

    }

    public void Deactivete_Object(GameObject obj)
    {
        obj.SetActive(false);

    }

    public void SetPause(bool paused)
    {
        Game_settings.Instance.SetPaused(paused);
    }

    public void ContinuePreviosGame(bool continue_game)
    {
        Game_settings.Instance.SetContinuePrevisiousGame(continue_game);
    }

    public void ExitAfterWon()
    {
        Game_settings.Instance.setExitAfterWon(true);
    }

    public void ContinueAfterGameOver()
    {
       ADC_SUdoku._instence.ShowInterstitialAD();
        Lives.Instance.ResetLives();
    }
}
