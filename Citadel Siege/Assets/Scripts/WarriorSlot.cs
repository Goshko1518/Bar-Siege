using System.Security.Cryptography.X509Certificates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WarriorSlot : MonoBehaviour, IClickable
{
    public int owner;
    private Outline HighlightObject;
    private WarriorSlot[] SelectedSlots;
    public bool IsInteracted = false;
    public bool IsHighlighted = false;
    public GameObject [] whiteWarriors;
    public GameObject [] whiteSlots;
    public GameObject [] blackWarriors;
    [HideInInspector]
    public GameObject [] warriorCollection;
    private DropdownManager dropdownManager;
    public GameObject warriorToSpawn = null;
    private Clicker clicker;
    private GameManager gameManager;
    private UIManager uIManager;

    

    
    // Start is called before the first frame update
    void Start()
    {
        HighlightObject = GetComponent<Outline>();
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();

        if(owner == 1){
            warriorCollection = whiteWarriors;
            
        }
        
        else {
            warriorCollection = blackWarriors;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(IsInteracted == true){
            IsHighlighted = true;
        }
    }

    public void OnClick()
    {
        Interact();
    }
    
    private void Interact()
    {
        if (clicker.clickedObject.GetComponent<WarriorSlot>().owner == gameManager.turnOwner)
        {

            clicker.warriorSlot = clicker.clickedObject.GetComponent<WarriorSlot>();
            Select();
        }
        if (clicker.clickedObject.GetComponent<WarriorSlot>().owner != gameManager.turnOwner)
        {
            clicker.warriorSlot = null;
        }

        IsInteracted = true;
    }

    public void Select()
    {
        clicker.warriorSlot.GetComponent<CustomHighlightScript>().ClickedOn();
        if (warriorToSpawn != null)
        {
            clicker.selectedUnitScript = warriorToSpawn.GetComponent<GenericFight>();
            
        }
          

        else
        {
            uIManager.EnableUpgradeKnightButton(false);
            uIManager.EnableUpgradeBallistaButton(false);
            uIManager.EnableDeleteUnitButton(false);
            uIManager.RollButtonPl1.interactable = true;
            uIManager.RollButtonPl2.interactable = true;
        }
        
        if (warriorToSpawn != null && clicker.selectedUnitScript != null)
        {
            uIManager.RollButtonPl1.interactable = false;
            uIManager.RollButtonPl2.interactable = false;
            
            Debug.Log("no warrior u stupid");
            if (gameManager.gamePhase == GamePhase.PLACEMENT)
            {
                uIManager.upgradeUnitToKnightButton.GetComponent<Button>().enabled = true;
                uIManager.upgradeUnitToBallistaButton.GetComponent<Button>().enabled = true;
                uIManager.upgradeUnitToKnightButton.GetComponent<Button>().onClick.RemoveAllListeners();
                uIManager.deleteUnitButton.GetComponent<Button>().onClick.RemoveAllListeners();
                uIManager.upgradeUnitToKnightButton.GetComponent<Button>().onClick.AddListener(clicker.selectedUnitScript.UpgradeToKnight);
                uIManager.upgradeUnitToBallistaButton.GetComponent<Button>().onClick.AddListener(clicker.selectedUnitScript.UpgradeToBallista);
                uIManager.deleteUnitButton.GetComponent<Button>().onClick.AddListener(clicker.selectedUnitScript.DeleteUnit);
                uIManager.deleteUnitButton.GetComponent<Button>().onClick.AddListener(uIManager.DisablePlacementunitModificationsButtons);
                uIManager.EnableUpgradeKnightButton(true);
                if(clicker.selectedUnitScript.warriorType == WarriorType.KNIGHT)
                uIManager.upgradeUnitToKnightButton.GetComponent<Button>().enabled = false;
                uIManager.EnableUpgradeBallistaButton(true);
                if(clicker.selectedUnitScript.warriorType == WarriorType.SIEGE)
                uIManager.upgradeUnitToBallistaButton.GetComponent<Button>().enabled = false;
                uIManager.EnableDeleteUnitButton(true);
            }
            if (gameManager.gamePhase == GamePhase.PREPARATION)
            {
                uIManager.moveUnitToReserveButton.GetComponent<Button>().onClick.RemoveAllListeners();
                uIManager.moveUnitToReserveButton.GetComponent<Button>().onClick.AddListener(clicker.FindSelectedLineReserve().MoveUnitToReserve);
                uIManager.EnableMoveUnitToReserve(true);
            }
        }
        if (gameManager.gamePhase == GamePhase.PREPARATION && warriorToSpawn == null)
        {
            uIManager.moveUnitToReserveButton.GetComponent<Button>().onClick.RemoveAllListeners();
            uIManager.EnableMoveUnitToReserve(false);
        }
    }
}
