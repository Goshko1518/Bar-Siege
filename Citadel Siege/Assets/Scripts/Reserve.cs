using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reserve : MonoBehaviour
{
    private Line line;
    public DropdownManager dropdownManager;
    private Clicker clicker;
    private UIManager uIManager;
    // Start is called before the first frame update
    void Start()
    {
        uIManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        //dropdownManager = GameObject.FindGameObjectWithTag("DropDown Manager").GetComponent<DropdownManager>();
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
        line = GetComponent<Line>();
    }
    public void MoveUnitToReserve(){
        //if you don't use the reserved unit in the current wave, the unit will be lost
        if (dropdownManager.targetCounter >= 2)
        {
            uIManager.OnReplacementsLimitExceeded?.Invoke();
            return;
        }
        else if (dropdownManager.targetCounter < 2){
            dropdownManager.targetCounter++;
            string unitNameForReserve = clicker.selectedUnitScript.name;
        GameObject unitToMove = clicker.selectedUnitScript.gameObject;
        dropdownManager.AddDropdownOption(unitNameForReserve);
        Destroy(unitToMove);
        dropdownManager.dropDown.value = 0;
        }

        uIManager.EnableMoveUnitToReserve(false);
    }
}
