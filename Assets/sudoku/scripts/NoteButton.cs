using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NoteButton : Selectable, IPointerClickHandler
{

    public Sprite On_image;
    public Sprite Off_image;
    private bool active;

    void Start()
    {
        active = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        active = !active;

        if (active)
        {
            GetComponent<Image>().sprite = On_image;
        }
        else
        {
            GetComponent<Image>().sprite = Off_image;
        }

        GameEvents.OnNotesActiveMethod(active);

    }

}
