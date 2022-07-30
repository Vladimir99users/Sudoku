using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Gridsquare : Selectable,IPointerClickHandler,ISubmitHandler,IPointerUpHandler,IPointerExitHandler
{
   public Color Cla_normal = Color.green;
    public List<GameObject> number_notes;
    private bool note_active;
    //изменение 
    public bool has_default_value = false;



    public GameObject number_text;
    private int number_ = 0;

    public int getSquareNumber() { return number_; }

    private int corect_number = 0;

    private bool has_wrong_value_ = false;
    private bool selected_ = false;
    private int square_index = -1;

    public bool hasWrongValue() { return has_wrong_value_; }
    public bool isSelected() { return selected_; }
    public void SetSquareIndex(int index)
    {
        square_index = index;
    }

    //функция для проверки правильности ввода, будет возвращать true or false;
    public bool iscorrectNemberset()
    {


        return number_ == corect_number;
    }


    //изменение
    public void SetHasDefaultValue(bool hasdefualt) { has_default_value = hasdefualt; }
    public bool GetHasDefaultValue()
    {
        return has_default_value;
    }

    public void SetCorrectNumber(int number)
    {
        corect_number = number;
        has_wrong_value_ = false;

        if (number_ !=0 &&  number_ != corect_number)
        {
            has_wrong_value_ = true;
            SetSqareColour(Color.red);
        }

    }

    //данная функци используется для победы игрока.
    public void SetCorrectNumber()
    {
        number_ = corect_number;
        SetNoteNumberValue(0);
        DisplayText();
    }

    void Start()
    {
        selected_ = false;
        note_active = false;

        if (Game_settings.Instance.GetContinuePreviousGame() == false)
            SetNoteNumberValue(0);
        else
            SetClearEmptyNotes();
    }

    public List<string> GetSquareNotes()
    {
        List<string> notes = new List<string>();
        foreach (var number in number_notes)
        {
            notes.Add(number.GetComponent<Text>().text);
        }
        return notes;

    }

    private void SetClearEmptyNotes()
    {
        foreach (var number in number_notes)
        {
            if(number.GetComponent<Text>().text == "0")
            {
                number.GetComponent<Text>().text = " ";
            }
        }
    }

    private void SetNoteNumberValue(int value)
    {
        foreach(var number in number_notes)
        {
            if(value <= 0)
            {
                number.GetComponent<Text>().text = " ";
            } else
            {
                number.GetComponent<Text>().text = value.ToString();
            }
        }
    }

    public void SetNoteSingleNumberValue(int value,bool force_update = false)
    {
        if(note_active == false && force_update == false)
        {
            return;
        }

        if(value <= 0)
        {
            number_notes[value - 1].GetComponent<Text>().text = " ";
        } else
        {
            if(number_notes[value-1].GetComponent<Text>().text == " " || force_update)
            {
                number_notes[value - 1].GetComponent<Text>().text = value.ToString();
            } else
            {
                number_notes[value - 1].GetComponent<Text>().text = " ";
            }
        }

    }


    public void SetGridNotes(List<int> _notes)
    {
        foreach(var note in _notes)
        {
            SetNoteSingleNumberValue(note, true);
        }
    }

    public void OnNotesActive(bool active)
    {
        note_active = active;
    }


    public void DisplayText()
    {
        if (number_ <= 0) number_text.GetComponent<Text>().text = " ";
        else number_text.GetComponent<Text>().text = number_.ToString();
    }

    public void SetNumber( int number)
    {
        number_ = number;
        DisplayText();
   
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected_ = true;
        GameEvents.SquareSelectedNethod(square_index);
    }

    public void OnSubmit(BaseEventData eventData)
    {
       
    }

    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnsquareSelected += OnsquareSelected;
        GameEvents._onNotesActive += OnNotesActive;
        GameEvents._OnClearumber += OnClearumber;

        GameEvents._OnGameOver += OnGameOver;

    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnsquareSelected -= OnsquareSelected;
        GameEvents._onNotesActive -= OnNotesActive;
        GameEvents._OnClearumber -= OnClearumber;

        GameEvents._OnGameOver -= OnGameOver;
    }

    public void OnClearumber()
    {
        if(selected_ && !has_default_value)
        {
            number_ = 0;
            has_wrong_value_ = false;
            SetSqareColour(Color.white);
            SetNoteNumberValue(0);
            DisplayText();
        }
    }

    private void OnGameOver()
    {
        if(number_ != 0 && number_ != corect_number)
        {
            has_default_value = false;
            SetSqareColour(Color.white);
            number_ = 0;
            DisplayText();
        }
    }

    public void OnSetNumber(int number)
    { 

        if(selected_ && has_default_value == false)
        {
            if (note_active == true && has_wrong_value_ == false)
            {
                SetNoteSingleNumberValue(number);   
            } else if(note_active ==false)
            {
                SetNoteNumberValue(0);
                SetNumber(number);

                if (number_ != corect_number)
                {
                    has_wrong_value_ = true;
                    var сolors = this.colors;
                    сolors.normalColor = Color.red;
                    this.colors = сolors;

                    GameEvents.OnWrongNumberMethod();
                }
                else
                {
                    has_wrong_value_ = false;
                    has_default_value = true;
                    var сolors = this.colors;
                    сolors.normalColor = Color.white;
                    this.colors = сolors;

                }
            }
           
       
        }
    }

    public void OnsquareSelected(int Square_index)
    {
        if(square_index != Square_index)
        {
            selected_ = false;
        }
    }

   public void SetSqareColour(Color color)
    {
        var colors = this.colors;
        colors.normalColor = color;
        this.colors = colors;
    }

}
