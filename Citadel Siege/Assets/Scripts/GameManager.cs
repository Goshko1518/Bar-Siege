using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public enum GamePhase { PLACEMENT, PREPARATION, RESOLUTION }
public class GameManager : MonoBehaviour
{
    public BattlefieldOverviewCapturer battlefieldOverviewCapturer;
    public GamePhase gamePhase;
    public Button nextPhaseButton;
    public UnityEvent OnGamePhaseChanged;
    private GameObject musicManagerObject;
    private MusicManager musicManager;
    private GameObject[] lines;
    private UIManager uIManager;
    private Clicker clicker;
    private Camera screenshotCamera;
    public List<Line> lineScripts;
    public DropdownManager dropdownManager;
    public Wall whiteCitadel;
    public Wall blackCitadel;
    [SerializeField]
    public int turnOwner = 1;
    public int target = -1;
    public GameObject minimap;
    public CoinScript coinsPl1;
    public CoinScriptPlayer2 coinsPl2;
    private GameObject barracksWhiteCounter;
    private GameObject barracksBlackCounter;
    private GameObject coinsWhiteCounter;
    private GameObject coinsBlackCounter;
    private void Start()
    {
        Time.timeScale = 1;
        uIManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        gamePhase = GamePhase.PLACEMENT;
        musicManagerObject = GameObject.FindGameObjectWithTag("Music Manager");
        musicManager = musicManagerObject.GetComponent<MusicManager>();
        lines = GameObject.FindGameObjectsWithTag("Line");
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
        coinsPl1 = GameObject.FindGameObjectWithTag("CoinsWhite").GetComponent<CoinScript>();
        coinsPl2 = GameObject.FindGameObjectWithTag("CoinsBlack").GetComponent<CoinScriptPlayer2>();
        screenshotCamera = GameObject.FindGameObjectWithTag("Battlefield Overview Camera").GetComponent<Camera>();
        barracksWhiteCounter = GameObject.FindGameObjectWithTag("BarracksWhite");
        barracksBlackCounter = GameObject.FindGameObjectWithTag("BarracksBlack");
        coinsWhiteCounter = GameObject.FindGameObjectWithTag("CoinsWhite");
        coinsBlackCounter = GameObject.FindGameObjectWithTag("CoinsBlack");
        
        for (int i = 0; i < lines.Length; i++)
        {
            lineScripts.Add(lines[i].GetComponent<Line>());
        }
    }
    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = target;
    }
    public void ChangePhase()
    {
        if (gamePhase == GamePhase.PLACEMENT)
        {
            uIManager.EnableUpgradeKnightButton(false);
            uIManager.EnableUpgradeBallistaButton(false);
            uIManager.EnableDeleteUnitButton(false);
            dropdownManager.ClearAllDropdownOptions();
            dropdownManager.replacementsCounter = 0;
            dropdownManager.gameObject.SetActive(true);
            minimap.SetActive(true);
            uIManager.pinExplanationImage.gameObject.SetActive(true);
            uIManager.unitsDropdown.GetComponent<DropdownManager>().ClearAllDropdownOptions();
            battlefieldOverviewCapturer.TakeScreenshot_Static();
            uIManager.EndTurnP1Button.GetComponentInChildren<Text>().text = "End Turn";
            uIManager.EndTurnP2Button.GetComponentInChildren<Text>().text = "End Turn";
            uIManager.phaseText.text = "Preparation phase";
            dropdownManager.gameObject.GetComponent<Dropdown>().interactable = true;
        }
        if (gamePhase == GamePhase.PREPARATION)
        {
            minimap.SetActive(false);
            uIManager.pinExplanationImage.gameObject.SetActive(false);
            dropdownManager.gameObject.SetActive(false);
            uIManager.phaseText.text = "Battle phase";
            for (int i = 0; i < lineScripts.Count; i++)
            {
                for (int j = 0; j < lineScripts[i].whiteSlots.Length; j++)
                {
                    if (j == 0)
                    {
                        //1v1 situation where black enemy is standing on one slot lower
                        //white is on j position
                        //white has no allies
                        //j is 0
                        //black is on j + 1 position
                        //black has no allies
                        if (lineScripts[i].whiteSlots[j].warriorToSpawn != null &&
                        lineScripts[i].whiteSlots[j + 1].warriorToSpawn == null &&
                        lineScripts[i].blackSlots[j + 1].warriorToSpawn != null &&
                        lineScripts[i].blackSlots[j].warriorToSpawn == null)
                        {
                            lineScripts[i].whiteSlots[j].warriorToSpawn.GetComponent<GenericFight>().unitWalkDirection =
                            lineScripts[i].blackSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition -
                            lineScripts[i].whiteSlots[j].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition;

                            lineScripts[i].blackSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().unitWalkDirection =
                            lineScripts[i].whiteSlots[j].warriorToSpawn.GetComponent<GenericFight>().gameObject.transform.localPosition -
                            lineScripts[i].blackSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition;
                        }
                        //1v1 situation where white enemy is standing on one slot lower
                        //black is on j position
                        //black has no allies
                        //j is 0
                        //white is on j + 1 position
                        //white has no allies
                        if (lineScripts[i].blackSlots[j].warriorToSpawn != null &&
                        lineScripts[i].blackSlots[j + 1].warriorToSpawn == null &&
                        lineScripts[i].whiteSlots[j + 1].warriorToSpawn != null &&
                        lineScripts[i].whiteSlots[j].warriorToSpawn == null)
                        {
                            lineScripts[i].blackSlots[j].warriorToSpawn.GetComponent<GenericFight>().unitWalkDirection =
                            lineScripts[i].whiteSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition -
                            lineScripts[i].blackSlots[j].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition;

                            lineScripts[i].whiteSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().unitWalkDirection =
                            lineScripts[i].blackSlots[j].warriorToSpawn.GetComponent<GenericFight>().gameObject.transform.localPosition -
                            lineScripts[i].whiteSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition;
                        }
                    }
                    if (j > 0 && j < 2)
                    {
                        //1v1 situation where black enemy is standing on one slot lower
                        //white is on j position
                        //white has no allies
                        //black is on j + 1 position
                        //black has no allies
                        if (lineScripts[i].whiteSlots[j].warriorToSpawn != null &&
                        lineScripts[i].whiteSlots[j - 1].warriorToSpawn == null &&
                        lineScripts[i].whiteSlots[j + 1].warriorToSpawn == null &&
                        lineScripts[i].blackSlots[j + 1].warriorToSpawn != null &&
                        lineScripts[i].blackSlots[j - 1].warriorToSpawn == null &&
                        lineScripts[i].blackSlots[j].warriorToSpawn == null)
                        {
                            lineScripts[i].whiteSlots[j].warriorToSpawn.GetComponent<GenericFight>().unitWalkDirection =
                            lineScripts[i].blackSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition -
                            lineScripts[i].whiteSlots[j].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition;

                            lineScripts[i].blackSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().unitWalkDirection =
                            lineScripts[i].whiteSlots[j].warriorToSpawn.GetComponent<GenericFight>().gameObject.transform.localPosition -
                            lineScripts[i].blackSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition;
                        }

                        //1v1 situation where white enemy is standing on one slot lower
                        //black is on j position
                        //black has no allies
                        //white is on j + 1 position
                        //white has no allies
                        if (lineScripts[i].blackSlots[j].warriorToSpawn != null &&
                        lineScripts[i].blackSlots[j - 1].warriorToSpawn == null &&
                        lineScripts[i].blackSlots[j + 1].warriorToSpawn == null &&
                        lineScripts[i].whiteSlots[j + 1].warriorToSpawn != null &&
                        lineScripts[i].whiteSlots[j - 1].warriorToSpawn == null &&
                        lineScripts[i].whiteSlots[j].warriorToSpawn == null)
                        {
                            lineScripts[i].blackSlots[j].warriorToSpawn.GetComponent<GenericFight>().unitWalkDirection =
                            lineScripts[i].whiteSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition -
                            lineScripts[i].blackSlots[j].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition;

                            lineScripts[i].whiteSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().unitWalkDirection =
                            lineScripts[i].blackSlots[j].warriorToSpawn.GetComponent<GenericFight>().gameObject.transform.localPosition -
                            lineScripts[i].whiteSlots[j + 1].warriorToSpawn.GetComponent<GenericFight>().transform.localPosition;
                        }
                    }
                }
            }
        }
        gamePhase++;
        OnGamePhaseChanged?.Invoke();
    }
    private void Update()
    {
        if (gamePhase == GamePhase.RESOLUTION)
        {
            nextPhaseButton.gameObject.SetActive(false);
            int numberOfWarriorsLeft = GameObject.FindGameObjectsWithTag("Warrior").Length;
            if (numberOfWarriorsLeft == 0)
            {
               
                StartCoroutine("NextTurn");
                
            }
        }
        if (whiteCitadel.health <= 0)
        {
            Time.timeScale = 0;
            uIManager.RestartTime();
            uIManager.ShowWinner("Black player won. Do you want to play again?(like you have choice...)");
        }

        if (blackCitadel.health <= 0)
        {
            Time.timeScale = 0;
            uIManager.RestartTime();
            uIManager.ShowWinner("White player won. Do you want to play again?(like you have choice...)");
        }

        if (Application.targetFrameRate != target)
            Application.targetFrameRate = target;

    }
    IEnumerator NextTurn()
    {
        yield return new WaitForSeconds(3f);
        gamePhase = GamePhase.PLACEMENT;
        dropdownManager.dropDown.options.Clear();
        dropdownManager.AddBasicDropDownOptions();
        musicManager.ChangeClip();
        minimap.SetActive(false); 
        uIManager.pinExplanationImage.gameObject.SetActive(false);
        nextPhaseButton.gameObject.SetActive(true);
        coinsPl1.coinsPl1 += 2;
        coinsPl2.coinsPl2 += 2;
        Debug.Log(coinsPl2.coinsPl2);
        coinsPl1.Pl1CoinText.text = coinsPl1.coinsPl1.ToString();
        coinsPl2.Pl2CoinText.text = coinsPl2.coinsPl2.ToString();
        uIManager.EndTurnP1Button.gameObject.SetActive(true);
        uIManager.RollButtonCoinsPl1.interactable = true;
        uIManager.RollButtonPl1.gameObject.SetActive(true);
        uIManager.RollButtonCoinsPl1.gameObject.SetActive(true);
        uIManager.rollText.gameObject.SetActive(true);
        uIManager.rollAmountText.gameObject.SetActive(true);
        uIManager.playerTurnText.text = "Turn: Player 1";
        uIManager.EndTurnP1Button.gameObject.GetComponent<ChangeCamera>().CameraToPlayer1();
        uIManager.EndTurnP1Button.GetComponentInChildren<Text>().text = "End Turn";
        uIManager.EndTurnP1Button.GetComponentInChildren<Text>().text = "End Turn";
        uIManager.phaseText.text = "Placement phase";
        uIManager.warriorExplanationImage.gameObject.SetActive(false);
        uIManager.RollButtonPl1.interactable = false;
        barracksWhiteCounter.gameObject.GetComponent<DiceCheckZoneScript>().counter = 0;
        barracksBlackCounter.GetComponent<DiceCheckerPl2>().counter = 0;
        coinsWhiteCounter.GetComponent<CoinScript>().counter = 0;
        coinsBlackCounter.GetComponent<CoinScriptPlayer2>().counter = 0;
        Debug.Log(coinsPl2.coinsPl2);
        StopCoroutine("NextTurn");
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ChangePlayerTurn(int newPlayerTurn)
    {
        //Change turn
        turnOwner = newPlayerTurn;
        //Find all icons
        GameObject[] allIcons = GameObject.FindGameObjectsWithTag("Warrior Icon");
        //Make separate collection for each side
        List<RawImage> whiteWarriorsIcons = new List<RawImage>();
        List<RawImage> blackWarriorsIcons = new List<RawImage>();
        //Distribute icons by owner
        for (int i = 0; i < allIcons.Length; i++)
        {
            GenericFight warriorScript = allIcons[i].GetComponentInParent<GenericFight>();
            RawImage warriorIcon = warriorScript.warriorIcon;
            if (warriorScript.owner == 1)
            {
                whiteWarriorsIcons.Add(warriorIcon);
            }
            if (warriorScript.owner == 2)
            {
                blackWarriorsIcons.Add(warriorIcon);
            }
        }
        clicker.warriorSlot = null;
        //Manipulate with icons' visibility
        if (turnOwner == 1)
        {
            if (gamePhase == GamePhase.PREPARATION)
            {
                uIManager.RollButtonPl1.gameObject.SetActive(false);
                uIManager.RollButtonCoinsPl1.gameObject.SetActive(false);
                uIManager.unitsDropdown.GetComponent<DropdownManager>().ClearAllDropdownOptions();
            }
            for (int b = 0; b < blackWarriorsIcons.Count; b++)
            {
                blackWarriorsIcons[b].gameObject.SetActive(false);
            }
            for (int w = 0; w < whiteWarriorsIcons.Count; w++)
            {
                whiteWarriorsIcons[w].gameObject.SetActive(true);
            }
        }
        if (turnOwner == 2)
        {
            if (gamePhase == GamePhase.PREPARATION){
                uIManager.RollButtonPl2.gameObject.SetActive(false);
                uIManager.RollButtonCoinsPl2.gameObject.SetActive(false);
                uIManager.unitsDropdown.GetComponent<DropdownManager>().ClearAllDropdownOptions();
            }
            
            for (int b = 0; b < blackWarriorsIcons.Count; b++)
            {
                blackWarriorsIcons[b].gameObject.SetActive(true);
            }
            for (int w = 0; w < whiteWarriorsIcons.Count; w++)
            {
                whiteWarriorsIcons[w].gameObject.SetActive(false);
            }
        }
    }
}