using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class Clicker : MonoBehaviour
{
    private RaycastHit hit;
    private Vector3 mousePos;
    public Ray raycast;
    private IClickable iClickable = null;
    
    public GenericFight selectedUnitScript = null;
    public WarriorSlot warriorSlot;
   
    public DropdownManager dropdownManager;
    public Touch touch;
    public int touchCount = 0;
    private UIManager uIManager;
    public GameManager gameManager;
    [SerializeField]
    public GameObject clickedObject;
    public CustomHighlightScript HighlightedObject;
    public List<CustomHighlightScript> highlights;
    public GameObject[] slots;
    private void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        Debug.Log(GameObject.FindGameObjectsWithTag("Warrior Pick").Length + "da fucking length");
        slots = GameObject.FindGameObjectsWithTag("Warrior Pick");
        for (int i = 0; i < slots.Length; i++)
        {
            highlights.Add(slots[i].GetComponent<CustomHighlightScript>());
        }
        Debug.Log(highlights.Count);
    }

    private void Update()
    {
        touchCount = Input.touchCount;
        if (touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (Input.GetMouseButtonUp(0) || Input.touchCount > 0 && touch.tapCount == 1 && touch.phase == TouchPhase.Ended)
            {
                mousePos = touch.position;
                raycast = Camera.main.ScreenPointToRay(mousePos);
                
                if (Physics.Raycast(raycast, out hit) && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    clickedObject = hit.collider.gameObject;
                    iClickable = clickedObject.GetComponent<IClickable>();
                    if (iClickable != null)
                    {
                        iClickable.OnClick();
                    }

                    else
                    {
                        return;
                    }
                    HighlightedObject = clickedObject.GetComponent<CustomHighlightScript>();
                    highlights.Find(x => x = HighlightedObject);
                    int changedValueIndex = highlights.FindIndex(a => a == HighlightedObject);
                    Debug.Log(changedValueIndex);
                    highlights[changedValueIndex]?.ClickedOn();
                    for (int i = 0; i < highlights.Count; i++)
                    {
                        if (i != changedValueIndex)
                        {
                            highlights[i]?.NotClickedOn();
                        }

                    }
                  
                }
                else
                {
                    return;
                }
            // if(warriorSlot == null){
            //     //if(gameManager.turnOwner == 1)
            //     uIManager.RollButtonPl1.interactable = false;
            //     //else
            //     uIManager.RollButtonPl2.interactable = false;
            // }

            // if (selectedUnitScript == null)
            //     isUnitSelected = false;

            // if(gameManager.gamePhase != GamePhase.RESOLUTION){
            //     Debug.Log(warriorSlot == null ? "null" : "not null");
            //     if(warriorSlot == null){
            //         uIManager.EnableUnitsDropdown(false);
            //     }
            //     else{
            //         uIManager.EnableUnitsDropdown(true);
            //     }
            // }
            }
        }

    }
    public Reserve FindSelectedLineReserve()
    {
        List<Line> lines = new List<Line>();
        List<GameObject> lineGameObjects = GameObject.FindGameObjectsWithTag("Line").ToList();
        for (int i = 0; i < lineGameObjects.Count; i++)
        {
            Line line = lineGameObjects[i].GetComponent<Line>();
            lines.Add(line);
        }
        Line searchLine = lines.Find(x => x.GetCorrespondingSlots(selectedUnitScript).ToList().
        Find(y => y.warriorToSpawn == (selectedUnitScript.gameObject)));
        Reserve searchReserve = searchLine.gameObject.GetComponent<Reserve>();
        return searchReserve;
    }
}