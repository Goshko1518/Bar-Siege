using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DropdownManager : MonoBehaviour
{
    public Dropdown dropDown;
    // WarriorSlot warriorSlot = null;
    private GameManager gameManager;
    public int replacementsCounter = 0;
    public int targetCounter = 0;
    public int replacementCounterWhite = 0;
    public int replacementCounterBlack = 0;
    public UnityEvent OnWarriorSpawned;
    private Clicker clicker;
    private UIManager uIManager;
    GenericFight selectedWarrior;

    public DiceCheckZoneScript numberOfRollsPl1;
    public DiceCheckerPl2 numberOfRollsPl2;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        //warriorPick = GetComponent<Dropdown>();
        AddBasicDropDownOptions();
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
        uIManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        numberOfRollsPl1 = GameObject.FindGameObjectWithTag("BarracksWhite").GetComponent<DiceCheckZoneScript>();
        numberOfRollsPl2 = GameObject.FindGameObjectWithTag("BarracksBlack").GetComponent<DiceCheckerPl2>();
        gameObject.GetComponent<Dropdown>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void AddBasicDropDownOptions(){
        List<string> startOptions = new List<string>(){"none", "Peasant", "Pikeman", "Shieldman", "Archer", "Knight", "Ballista"};
        dropDown.AddOptions(startOptions);
    }
    private void Spawn(GenericFight selectedWarrior, float xRot, float yRot){
        //The spawn position is a bit shifted upwards so pawns do not get stuck in the ground
            clicker.warriorSlot.warriorToSpawn = Instantiate(selectedWarrior.gameObject, clicker.warriorSlot.gameObject.transform.position
            + new Vector3(0, 0.5f, 0), Quaternion.Euler(xRot, yRot, 0)) as GameObject;
    }
    public void SpawnWarrior()
    {
        if(clicker.warriorSlot != null){
            targetCounter = clicker.warriorSlot.owner == 1 ? replacementCounterWhite : replacementCounterBlack;
        
        if(dropDown.value != 0){
            //Replace unit with a new one
        string primeWarriorName = "";
        if (clicker.warriorSlot.warriorToSpawn != null){
            primeWarriorName =  clicker.warriorSlot.warriorToSpawn.GetComponent<GenericFight>().name;
            Destroy(clicker.warriorSlot.warriorToSpawn);
        }
        string selectedWarriorName = dropDown.options[dropDown.value].text;
        selectedWarrior = clicker.warriorSlot.warriorCollection.ToList().
        Find(warrior => dropDown.value != 0 && warrior.GetComponent<GenericFight>().name == selectedWarriorName).GetComponent<GenericFight>();
        float xRot = selectedWarriorName == "Ballista" ? 0 : -90;
        float yRot = selectedWarrior.GetComponent<GenericFight>().owner == 1 ? 0 : 180;


        
        if(gameManager.gamePhase != GamePhase.PREPARATION){
            Spawn(selectedWarrior, xRot, yRot);
        }
        
        if(gameManager.gamePhase == GamePhase.PREPARATION && targetCounter < 2){
            if(primeWarriorName != ""){
                AddDropdownOption(primeWarriorName);
            }
            RemoveDropdownOption();
            Spawn(selectedWarrior, xRot, yRot);
            
            Debug.Log("repl. counter: " +  targetCounter);
        }       
        dropDown.value = 0;
        }
        }        
        
        clicker.selectedUnitScript = selectedWarrior;
        //clicker.warriorSlot = null;
        OnWarriorSpawned?.Invoke();
    }
    public void Spawn(string warriorName)
    {
        if (clicker.warriorSlot.warriorToSpawn != null)
        {
            Debug.Log("Why are u not entering on staying");
            if (gameManager.turnOwner == 1)
            {
                numberOfRollsPl1.counter--;
            }
            else if (gameManager.turnOwner == 2)
            {
                numberOfRollsPl2.counter--;
            }

            
            return;
            
        }
        GenericFight selectedWarrior = clicker.warriorSlot.warriorCollection.ToList().
       Find(warrior => warrior.GetComponent<GenericFight>().name == warriorName).GetComponent<GenericFight>();
        float xRot = warriorName == "Ballista" ? 0 : -90;
        float yRot = selectedWarrior.GetComponent<GenericFight>().owner == 1 ? 0 : 180;

        //The spawn position is a bit shifted upwards so pawns do not get stuck in the ground
        Spawn(selectedWarrior, xRot, yRot);
        clicker.selectedUnitScript = selectedWarrior;
        //clicker.warriorSlot = null;
        OnWarriorSpawned?.Invoke();
        uIManager.RollButtonPl1.interactable = false;
        uIManager.RollButtonPl2.interactable = false;

    }
    public void UpgradeUnit(string upgradedWarriorName){
        Destroy(clicker.warriorSlot.warriorToSpawn);
        GenericFight selectedWarrior = clicker.warriorSlot.warriorCollection.ToList().
       Find(warrior => warrior.GetComponent<GenericFight>().name == upgradedWarriorName).GetComponent<GenericFight>();
        float xRot = upgradedWarriorName == "Ballista" ? 0 : -90;
        float yRot = selectedWarrior.GetComponent<GenericFight>().owner == 1 ? 0 : 180;

        //The spawn position is a bit shifted upwards so pawns do not get stuck in the ground
        Spawn(selectedWarrior, xRot, yRot);
        clicker.selectedUnitScript = selectedWarrior;
        //clicker.warriorSlot = null;
        OnWarriorSpawned?.Invoke();
        uIManager.RollButtonPl1.interactable = false;
        uIManager.RollButtonPl2.interactable = false;

    }
    public void AddDropdownOption(string optionName){
        dropDown.AddOptions(new List<string>(){optionName});
    }
    public void RemoveDropdownOption(){
        dropDown.options.RemoveAt(dropDown.value);
    }
    public void ClearAllDropdownOptions(){
        dropDown.options.Clear();
        dropDown.AddOptions(new List<string>(){"none"});
    }

    
}