using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InfoOnMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject infoWindow;
    public Text topTextBox,bottomTextBox;
    private string topText = "Every point spent adds";
    public string bottomText;
    private bool overThisButtom;
    private Vector3 mousePos;
    private void Start()
    {
        topTextBox.text = topText;
    }

    //Detect if the Cursor starts to pass over the GameObject
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(name.Equals(this.name))
        {
            bottomTextBox.text = bottomText;
            mousePos = Input.mousePosition;
            infoWindow.SetActive(true);
            infoWindow.transform.position = new Vector2(mousePos.x+300, mousePos.y);
        }
       
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if (name.Equals(this.name))
        {
            infoWindow.SetActive(false);
        }
    }
}


