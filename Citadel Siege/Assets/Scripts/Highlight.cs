using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highlight : MonoBehaviour, IClickable
{
    // Start is called before the first frame update

    public GameObject SelectedSlot;
    public GameObject HighlightOutline1;
    public Outline HighlightOutline2;
        void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }
    
    public void OnClick()
    {
        Interact();
        
    }
    public void Interact(){
         Debug.Log("I Was Clicked");
        if(HighlightOutline2.GetComponent<Outline>().enabled == true)
        {
        HighlightOutline2.GetComponent<Outline>().enabled = false;
        }
            //HighlightOutline1.GetComponent<Outline>().enabled = false;
            
            //HighlightOutline2.enabled = false;
    }

}
