using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceCheckerPl2 : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 diceVelocity;

    private DiceCheckZoneScript Spawner;
    public int warriorSelect { get; set; }

    public DropdownManager dropdownManager;

    

    public Rigidbody DiceBody;
    public GameObject fallPlatform;
    public Text HowMuchIRolled;
    int diceNumberLocal;
    public int counter = 0;
    public int limit = 4;
    public Button DiceButton;
    public Wall blackBarrack;


    void Update()
    {
        HowMuchIRolled.text = diceNumberLocal.ToString();
        if(blackBarrack.health <= 0)
		limit = 2;
        if (counter >= limit)
        {
            DiceButton.interactable = false;
        }
    }

    void OnTriggerStay(Collider col)
    {
        if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
        {
            DiceBody.transform.position = new Vector3(599, 8, 595);
            DiceBody.transform.rotation = Quaternion.identity;
            fallPlatform.SetActive(true);
            DiceButton.interactable = true;
            switch (col.gameObject.name)
            {
                case "Side1":
                    DiceNumberTextScript.diceNumber = 6;
                    warriorSelect = 6;
                    Debug.Log(DiceNumberTextScript.diceNumber);
                    diceNumberLocal = 6;
                    counter++;
                    dropdownManager.Spawn("Knight");
                    break;
                case "Side2":
                    DiceNumberTextScript.diceNumber = 5;
                    warriorSelect = 5;
                    Debug.Log(DiceNumberTextScript.diceNumber);
                    diceNumberLocal = 5;
                    counter++;
                    dropdownManager.Spawn("Archer");
                    break;
                case "Side3":
                    DiceNumberTextScript.diceNumber = 4;
                    warriorSelect = 4;
                    Debug.Log(DiceNumberTextScript.diceNumber);
                    diceNumberLocal = 4;
                    counter++;
                    dropdownManager.Spawn("Shieldman");
                    break;
                case "Side4":
                    DiceNumberTextScript.diceNumber = 3;
                    warriorSelect = 3;
                    Debug.Log(DiceNumberTextScript.diceNumber);
                    diceNumberLocal = 3;
                    counter++;
                    dropdownManager.Spawn("Pikeman");
                    break;
                case "Side5":
                    DiceNumberTextScript.diceNumber = 2;
                    warriorSelect = 2;
                    Debug.Log(DiceNumberTextScript.diceNumber);
                    diceNumberLocal = 2;
                    counter++;
                    dropdownManager.Spawn("Peasant");
                    break;
                case "Side6":
                    DiceNumberTextScript.diceNumber = 1;
                    warriorSelect = 1;
                    Debug.Log(DiceNumberTextScript.diceNumber);
                    diceNumberLocal = 1;
                    counter++;
                    dropdownManager.Spawn("Peasant");
                    break;
            }
            Debug.Log("Number " + DiceNumberTextScript.diceNumber);
        }
    }
}
