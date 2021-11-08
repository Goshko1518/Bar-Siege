using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIManager : MonoBehaviour
{
    public Text nextStageText;
    public Text winnerText;
    public Text rollText;
    public Text rollAmountText;
    public Text phaseText;
    public Text playerTurnText;
    public Image warriorExplanationImage;
    public Image pinExplanationImage;
    public GameObject unitsDropdown;
    public GameObject nextStageButton;
    public GameObject restartButton;
    public GameObject upgradeUnitToKnightButton;
    public GameObject upgradeUnitToBallistaButton;
    public GameObject deleteUnitButton;
    public GameObject moveUnitToReserveButton;
    private GameObject gameManagerObject;
    private GameManager gameManager;
    private Clicker clicker;

    public Button RollButtonPl1;
    public Button RollButtonPl2;
    public Button EndTurnP1Button;
    public Button EndTurnP2Button;
    public Button BattleViewButton;
    public Button RollButtonCoinsPl1;
    public Button RollButtonCoinsPl2;
    public DropdownManager dropdownManager;
    public UnityEvent OnReplacementsLimitExceeded;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
    }
    public void SetButtonText()
    {
        string text = "";
        switch (gameManager.gamePhase)
        {
            case GamePhase.PLACEMENT:
                text = "Go to preparation phase";
                break;
            case GamePhase.PREPARATION:
                text = "To battle!";
                break;
                //works buggy
            case GamePhase.RESOLUTION:
                text = "Go to preparation phase";
                break;
        }
        nextStageText.text = text;
    }
    public void ShowInfoContentButton(){
        warriorExplanationImage.gameObject.SetActive(!warriorExplanationImage.IsActive());
    }
    public void EnableUpgradeKnightButton(bool isSelected)
    {
        upgradeUnitToKnightButton.SetActive(isSelected);
    }
    public void EnableUpgradeBallistaButton(bool isSelected)
    {
        upgradeUnitToBallistaButton.SetActive(isSelected);
    }
    public void EnableDeleteUnitButton(bool isSelected){
        deleteUnitButton.SetActive(isSelected);
    }
    public void EnableMoveUnitToReserve(bool isSelected){
        moveUnitToReserveButton.SetActive(isSelected);
    }
    public void DisablePlacementunitModificationsButtons(){
        upgradeUnitToKnightButton.SetActive(false);
        upgradeUnitToBallistaButton.SetActive(false);
        deleteUnitButton.SetActive(false);
    }
    public void DisableRollButtonUnitsPl1()
    {
        RollButtonPl1.interactable = false;
    }
    public void DisableRollButtonUnitsPl2()
    {
        RollButtonPl2.interactable = false;
    }
    public void DisableRollButtonCoinsPl1()
    {
        RollButtonCoinsPl1.interactable = false;
    }
    public void DisableRollButtonCoinsPl2()
    {
        RollButtonCoinsPl2.interactable = false;
    }
    public void RestartTime(){
        EndTurnP1Button.gameObject.SetActive(false);
        EndTurnP2Button.gameObject.SetActive(false);
        BattleViewButton.gameObject.SetActive(false);
        RollButtonPl1.gameObject.SetActive(false);
        RollButtonPl2.gameObject.SetActive(false);
        RollButtonCoinsPl1.gameObject.SetActive(false);
        RollButtonCoinsPl2.gameObject.SetActive(false);
        clicker.enabled = false;
        dropdownManager.gameObject.SetActive(false);
        restartButton.SetActive(true);
        nextStageButton.SetActive(false);
        moveUnitToReserveButton.SetActive(false);
        upgradeUnitToKnightButton.SetActive(false);
        deleteUnitButton.SetActive(false);
    }
    public void ShowWinner(string winner){
        winnerText.text = winner;
        winnerText.gameObject.SetActive(true);
    }
    public void EnableUnitsDropdown(bool argument){
        unitsDropdown.SetActive(argument);
    }
    public void EnableReplacementsLimitExceededMessage(){
        Debug.Log("You can not relpace more than 2 units");
    }
}