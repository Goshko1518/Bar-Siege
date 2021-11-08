using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citadel : MonoBehaviour,IClickable
{
    public int owner;
    public void OnClick()
    {
        Response();
    }

    void Response(){
        Debug.Log("You clicked on the citadel");
    }
}
