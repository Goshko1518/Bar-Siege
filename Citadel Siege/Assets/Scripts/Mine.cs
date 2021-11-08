using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour, IClickable
{
    public void OnClick()
    {
        Response();
    }

    void Response(){
        Debug.Log("You clicked on the mine");
    }
}
