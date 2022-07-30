using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lives : MonoBehaviour
{
    public List<GameObject> Error_images;
    //изменение
    public static Lives Instance;
    public GameObject Game_over_popup;
    int Live = 0;
    int error_number = 0;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    void Start()
    {
        Live = Error_images.Count;
        error_number = 0;

        if (Game_settings.Instance.GetContinuePreviousGame())
        {
            error_number = Configuration.ErrorNumber();
            Live = Error_images.Count - error_number;
            for(int error = 0; error < error_number; error++)
            {
                Error_images[error].SetActive(true);
            }
        }
    }

    public int getErrorNumber()
    {
        return error_number;
    }


  private void WrongNumber()
    {
        if(error_number < Error_images.Count)
        {
            Error_images[error_number].SetActive(true);
            error_number++;
            Live--;
           
        }
        ChekForGameOver();
    }
    //изменение
    private void ChekForGameOver()
    {
        if (Live <= 0)
        {
            
            GameEvents._OnGameOverMethod();
            Game_over_popup.SetActive(true);
        }
    }

    private void OnEnable()
    {
        GameEvents.OnWrongNumber += WrongNumber;
    }

    private void OnDisable()
    {
        GameEvents.OnWrongNumber -= WrongNumber;
    }




    public void ResetLives()
    {
        foreach(var error in Error_images)
        {
            error.SetActive(false);
        }

        error_number = 0;
        Live = Error_images.Count;
    }

}
