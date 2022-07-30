using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_settings : MonoBehaviour
{
    public enum EGameMode
    {
        NOT_SET,
        Easy,
        Medium,
        Hard,
        VeryHard
    }



    public static Game_settings Instance;

    private void Awake()
    {
        _paused = false;
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else Destroy(this);
    }

    private EGameMode _GameMode;
    private bool _countinuePreviouseGame = false;
    private bool _exitAfterWon = false;


    public void setExitAfterWon(bool set)
    {
        _exitAfterWon = set;
        _countinuePreviouseGame = false;
    }

    public bool GetExtiAfterWon()
    {
        return _exitAfterWon;
    }

    public void SetContinuePrevisiousGame(bool continue_game)
    {

        _countinuePreviouseGame = continue_game;

    }

    public bool GetContinuePreviousGame()
    {
        return _countinuePreviouseGame;
    }



    private bool _paused = false;

    public void SetPaused(bool paused)
    {
        _paused = paused;
    }
    public bool GetPaused()
    {
        return _paused;
    }

     void Start()
    {
        _GameMode = EGameMode.NOT_SET;
        _countinuePreviouseGame = false;
    }

    public void SetGameModeD(EGameMode mode)
    {
        _GameMode = mode;
    }


    public void SetGameMode(string mode)
    {

        if (mode == "Easy")
        {
            SetGameModeD(EGameMode.Easy);
        }
        else if (mode == "Medium")
        {
            SetGameModeD(EGameMode.Medium);
        }
        else if (mode == "Hard")
        {
            SetGameModeD(EGameMode.Hard);
        }
        else if (mode == "Very Hard")
        {
            SetGameModeD(EGameMode.VeryHard);
        }
        else
        {
            SetGameModeD(EGameMode.NOT_SET);
        }
    }

    public string GetGameMode()
    {

        switch (_GameMode)
        {
            case (EGameMode.Easy): return "Easy";
            case (EGameMode.Medium): return "Medium";
            case (EGameMode.Hard): return "Hard";
            case (EGameMode.VeryHard): return "Very Hard";
        }

        
       Debug.LogError("Error: Game Lvl is not set");
        return " ";
    }

}
