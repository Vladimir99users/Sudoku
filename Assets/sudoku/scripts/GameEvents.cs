using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void UpdateSquareNumber(int number);
    public static event UpdateSquareNumber OnUpdateSquareNumber;

    public static void UpdateSquareNumberMethod(int number)
    {
        if (OnUpdateSquareNumber != null)
        {
            OnUpdateSquareNumber(number);
        }
    }

    public delegate void SquareSelected(int square_index);
    public static event SquareSelected OnsquareSelected;

    public static void SquareSelectedNethod(int square_index)
    {
        if (OnsquareSelected != null)
        {
            OnsquareSelected(square_index);
        }
    }

    public delegate void WrongNumber();
    public static event WrongNumber OnWrongNumber;

    public static void OnWrongNumberMethod()
    {
        if (OnWrongNumber != null)
        {
            OnWrongNumber();
        }
    }

    public delegate void GameOver();
    public static event GameOver _OnGameOver;

    public static void _OnGameOverMethod()
    {
        if (_OnGameOver != null)
        {
            _OnGameOver();
        }
    }


    public delegate void NotesActive(bool ative);
    public static event NotesActive _onNotesActive;

    public static void OnNotesActiveMethod(bool active)
    {
        if(_onNotesActive != null)
        {
            _onNotesActive(active);
        }
    }

    public delegate void ClearNumber();
    public static event ClearNumber _OnClearumber;

    public static void OnClearNumberMethod()
    {
        if(_OnClearumber != null)
        {
            _OnClearumber();
        }
    }


    public delegate void BoardComplited();
    public static event BoardComplited _OnBoardComplited;

    public static void OnBoardComlitedMethod() {
        if(_OnBoardComplited != null)
        {
            _OnBoardComplited();
        }
    }

}


