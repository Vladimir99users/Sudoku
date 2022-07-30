using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Number_button : Selectable,IPointerClickHandler,ISubmitHandler,IPointerUpHandler,IPointerExitHandler
{
    public int value = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameEvents.UpdateSquareNumberMethod(value);
    }

    public void OnSubmit(BaseEventData eventData)
    {
       
    }

    void Start()
    {
        
    }


    void Update()
    {
        
    }
}
