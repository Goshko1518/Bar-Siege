using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ChangeCamera : MonoBehaviour
{
    private GameManager gameManager;
    public UnityEvent OnPlayerTurnChanged;
    public List<GameObject> WhiteSmoke = new List<GameObject>();
    public List<GameObject> BlackSmoke = new List<GameObject>();

    public GameObject[] Player1Buttons = new GameObject[2];
    public GameObject[] Player2Buttons = new GameObject[2];

    public GameObject[] Player1UITexts = new GameObject[2];
    public GameObject[] Player2UITexts = new GameObject[2];

    private UIManager uIManager;
    public DropdownManager dropdownManager;
    private Clicker clicker;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("Game Manager").GetComponent<GameManager>();
        uIManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraToPlayer1(){
        //Go to p1 Point of view
        GameObject.Find("Main Camera").transform.position = new Vector3(482.3096f, 39.90424f, 579.9988f);
        GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(32.04f, 89.384f, 0);
    } 
    public void CameraToPlayer2(){
        GameObject.Find("Main Camera").transform.position = new Vector3(559.1f, 60.3f, 501.1f);
        GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(43.949f, 0, 0);
    }
    public void Pl2EndTurn()
    {
        //Changing the turn programmatically
        //gameManager.turnOwner = 1;
        
        dropdownManager.replacementsCounter = 0;
        OnPlayerTurnChanged?.Invoke();
        //Go to p1 Point of view
        CameraToPlayer1();
        WhiteSmoke[0].SetActive(false);
        WhiteSmoke[1].SetActive(false);
        WhiteSmoke[2].SetActive(false);
        BlackSmoke[0].SetActive(true);
        BlackSmoke[1].SetActive(true);
        BlackSmoke[2].SetActive(true);

        Player2Buttons[0].SetActive(false);
        Player2Buttons[1].SetActive(false);
        Player1Buttons[0].SetActive(true);
        Player1Buttons[1].SetActive(true);

        Player1UITexts[0].SetActive(true);
        Player1UITexts[1].SetActive(true);
        Player2UITexts[0].SetActive(false);
        Player2UITexts[1].SetActive(false);

        uIManager.playerTurnText.text = "Turn: Player 1";
        uIManager.EndTurnP1Button.gameObject.SetActive(true);
        uIManager.EndTurnP2Button.gameObject.SetActive(false);
        uIManager.upgradeUnitToBallistaButton.SetActive(false);
        uIManager.upgradeUnitToKnightButton.SetActive(false);
        uIManager.deleteUnitButton.gameObject.SetActive(false);
        uIManager.moveUnitToReserveButton.gameObject.SetActive(false);
        Player1Buttons[0].GetComponent<Button>().interactable = true;
        gameManager.ChangePhase();
        if (gameManager.gamePhase == GamePhase.PREPARATION)
        {
            Player2Buttons[0].SetActive(false);
            Player2Buttons[1].SetActive(false);
            Player1Buttons[0].SetActive(false);
            Player1Buttons[1].SetActive(false);
            uIManager.unitsDropdown.GetComponent<DropdownManager>().ClearAllDropdownOptions();
        }
        if(gameManager.gamePhase == GamePhase.RESOLUTION)
        {
            BattleCamera();
            uIManager.EndTurnP1Button.gameObject.SetActive(false);
            uIManager.EndTurnP2Button.gameObject.SetActive(false);
        }
        uIManager.warriorExplanationImage.gameObject.SetActive(false);
    }

    public void BattleCamera()
    {
        //Go to battle view
        if (gameManager.gamePhase == GamePhase.PREPARATION)
        {
            uIManager.unitsDropdown.GetComponent<DropdownManager>().ClearAllDropdownOptions();
        }
        dropdownManager.replacementsCounter = 0;
        CameraToPlayer2();
        uIManager.playerTurnText.text = "";
        WhiteSmoke[0].SetActive(false);
        WhiteSmoke[1].SetActive(false);
        WhiteSmoke[2].SetActive(false);
        BlackSmoke[0].SetActive(false);
        BlackSmoke[1].SetActive(false);
        BlackSmoke[2].SetActive(false);
        Player2Buttons[0].SetActive(false);
        Player2Buttons[1].SetActive(false);
        Player1Buttons[0].SetActive(false);
        Player1Buttons[1].SetActive(false);
        Player1UITexts[0].SetActive(false);
        Player1UITexts[1].SetActive(false);
        Player2UITexts[0].SetActive(false);
        Player2UITexts[1].SetActive(false);
    }
    public void Pl1EndTurn()
    {
        //Changing the turn programmatically
        //gameManager.turnOwner = 2;
        OnPlayerTurnChanged?.Invoke();

        GameObject.Find("Main Camera").transform.position = new Vector3(619.8276f, 38.38189f, 576.4537f);
        GameObject.Find("Main Camera").transform.rotation = Quaternion.Euler(40.118f, -91f, 0f);
        WhiteSmoke[0].SetActive(true);
        WhiteSmoke[1].SetActive(true);
        WhiteSmoke[2].SetActive(true);
        BlackSmoke[0].SetActive(false);
        BlackSmoke[1].SetActive(false);
        BlackSmoke[2].SetActive(false);

        uIManager.playerTurnText.text = "Turn: Player 2";
        Player1Buttons[0].SetActive(false);
        Player1Buttons[1].SetActive(false);
        uIManager.upgradeUnitToBallistaButton.SetActive(false);
        uIManager.upgradeUnitToKnightButton.SetActive(false);
        uIManager.deleteUnitButton.gameObject.SetActive(false);
        uIManager.moveUnitToReserveButton.gameObject.SetActive(false);
        if(clicker.warriorSlot == null){
            Player2Buttons[1].GetComponent<Button>().interactable = false;
        }
        else
        Player2Buttons[1].GetComponent<Button>().interactable = true;
        Player2Buttons[0].SetActive(true);
        Player2Buttons[1].SetActive(true);

        Player1UITexts[0].SetActive(false);
        Player1UITexts[1].SetActive(false);
        Player2UITexts[0].SetActive(true);
        Player2UITexts[1].SetActive(true);
        uIManager.EndTurnP1Button.gameObject.SetActive(false);
        uIManager.EndTurnP2Button.gameObject.SetActive(true);
        uIManager.warriorExplanationImage.gameObject.SetActive(false);
        Player2Buttons[0].GetComponent<Button>().interactable = true;
        if (gameManager.gamePhase == GamePhase.PREPARATION)
        {
            Player2Buttons[0].SetActive(false);
            Player2Buttons[1].SetActive(false);
            Player1Buttons[0].SetActive(false);
            Player1Buttons[1].SetActive(false);
            uIManager.unitsDropdown.GetComponent<DropdownManager>().ClearAllDropdownOptions();
        }
    }
}
