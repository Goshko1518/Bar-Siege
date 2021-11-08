using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableObjects : MonoBehaviour, IClickable
{
    // Start is called before the first frame update
 //public int owner;
    
    //public GameObject selectedSlot;
    

    private void Start() {
        
    }
    public void OnClick()
    {
        Response();
    }

    void Response(){
        Debug.Log("You clicked on slotNumber1");
        
    }
    
}
