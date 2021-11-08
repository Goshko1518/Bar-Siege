using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomHighlightScript : MonoBehaviour
{
    public GameObject selectedObject;
    public int redCol;
    public int greenCol;
    public int blueCol;
    public bool lookingAtObject = false;
    public bool flashingIn = true;
    public bool startedFlashing = false;
    private Clicker clicker;

    void Start()
    {
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (lookingAtObject == true)
        {
            selectedObject = clicker?.HighlightedObject?.gameObject;
            selectedObject.GetComponent<Renderer>().material.color = new Color32((byte)redCol, (byte)greenCol, (byte)blueCol, 255); //look into better solution
        }
        


    }
    public void ClickedOn()
    {
        //selectedObject = GameObject.Find(clicker.HighlightedObject.gameObject);
        lookingAtObject = true;
        if(startedFlashing == false)
        {
            startedFlashing = true;
            // StartCoroutine(FlashObject());
            ChangeColor();
        }
    }

    public void NotClickedOn()
    {
        //selectedObject = clicker.HighlightedObject.gameObject;
        startedFlashing = false;
        lookingAtObject = false;
        //StartCoroutine(FlashObject());
        ChangeColor();
        //selectedObject = clicker.HighlightedObject.gameObject;
        gameObject.GetComponent<Renderer>().material.color = new Color32(223, 223, 214, 255);
    }

    public void ChangeColor()
    {
        if (flashingIn == true)
        {
            if (redCol <= 30)
            {
                flashingIn = false;
            }
            else
            {
                redCol -= 25;
                greenCol -= 1;
            }


            if (flashingIn == false)
            {
                if (redCol >= 250)
                {
                    flashingIn = true;
                }
                else
                {
                    redCol += 25;
                    greenCol += 1;
                }
            }
        }
    }

    IEnumerator FlashObject()
    {
        while (lookingAtObject == true)
        {
            yield return new WaitForSeconds(0.05f);
            if (flashingIn == true)
            {
                if (redCol <= 30)
                {
                    flashingIn = false;
                }
                else
                {
                    redCol -= 25;
                    greenCol -= 1;
                }


                if (flashingIn == false)
                {
                    if (redCol >= 250)
                    {
                        flashingIn = true;
                    }
                    else
                    {
                        redCol += 25;
                        greenCol += 1;
                    }
                }
            }
        }
    }
}
