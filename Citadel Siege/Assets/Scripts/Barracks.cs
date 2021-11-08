using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Barracks : MonoBehaviour, IClickable
{
    public int owner;
    public WarriorSlot[] slots;
    public Ray raycast;
    private Vector3 mousePos;
    private WarriorSlot selectedSlot = null;
    private RaycastHit hit;
    private Clicker clicker;

    


    private void Start()
    {
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
        //ShowWarriorSlots(false);
    }

    private void Update()
    {
        if (
            //Input.GetMouseButtonUp(0)
            Physics.Raycast(raycast, out hit) &&
        clicker.touchCount > 0 && clicker.touch.phase == TouchPhase.Ended)
        {
            Debug.Log("barrack shit");
            mousePos = clicker.touch.position;
            raycast = Camera.main.ScreenPointToRay(mousePos);
            if (Physics.Raycast(raycast, out hit))
            {
                selectedSlot = hit.collider.gameObject.GetComponent<WarriorSlot>();
                if(selectedSlot != null){
                selectedSlot.IsInteracted = true;
                selectedSlot.IsHighlighted = true;
                //selectedSlot.gameObject.GetComponent<CustomHighlightScript>().NotClickedOn();
                
                int changedValueIndex = slots.ToList().FindIndex(a => a == selectedSlot);
                
                for (int i = 0; i < slots.Length; i++)
                {
                    if (i != changedValueIndex)
                    {
                        slots[i].IsInteracted = false;
                        slots[i].IsHighlighted = false;
                        //slots[i].gameObject.GetComponent<CustomHighlightScript>().ClickedOn();

                        
                    }
                    
                }   
                Debug.Log("selected slot: " + changedValueIndex);
                }
            }
        }

    }
    public void OnClick()
    {
        Response();
    }

    void Response()
    {
        ShowWarriorSlots(true);
    }
    void ShowWarriorSlots(bool argument)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].gameObject.SetActive(argument);
        }
    }
}
