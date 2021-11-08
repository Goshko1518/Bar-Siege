using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public enum WarriorType { PEASANT, PIKEMAN, SHIELDMAN, ARCHER, KNIGHT, SIEGE }
public class GenericFight : MonoBehaviour,IClickable
{
    [HideInInspector]
    public int startPower;
    [HideInInspector]
    public int power;
    public int damageToDeal;
    public string name;
    private Rigidbody rigidbody;
    private GameManager gameManager;
    //The way the unit looks
    private MeshFilter meshFilter;
    private DropdownManager dropdownManager;
    [HideInInspector]
    public Vector3 unitWalkDirection = Vector3.zero;
    private Clicker clicker;
    private UIManager uiManager;
    [SerializeField]
    public RawImage warriorIcon;
    public Texture knightIcon;
    public Texture ballistaIcon;
    public int owner;
    public WarriorType warriorType;
    public ParticleSystem deathEffect;
    public float movementSpeed = 1f;
    //I don't wanna do this...
    private bool ballistaEncounteredWarrior;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        switch (warriorType)
        {
            case WarriorType.PEASANT:
                startPower = 5;
                break;
            case WarriorType.PIKEMAN:
                startPower = 10;
                break;
            case WarriorType.SHIELDMAN:
                startPower = 10;
                break;
            case WarriorType.ARCHER:
                startPower = 10;
                break;
            case WarriorType.KNIGHT:
                startPower = 40;//20
                break;
            case WarriorType.SIEGE:
                startPower = 50;//35
                break;
            default:
                startPower = 10;
                break;
        }
        power = startPower;
        deathEffect = GetComponentInChildren<ParticleSystem>();
        meshFilter = GetComponent<MeshFilter>();
        clicker = GameObject.FindGameObjectWithTag("Clicker").GetComponent<Clicker>();
        unitWalkDirection = owner == 1 ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
        warriorIcon = GetComponentInChildren<RawImage>();
        uiManager = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
        gameManager = clicker.gameManager;
    }
    private void Update()
    {
        damageToDeal = power;
    }
    void FixedUpdate()
    {
        if (power > 0 && gameManager.gamePhase == GamePhase.RESOLUTION)
        {
            if(unitWalkDirection != null)
            rigidbody.AddForce(unitWalkDirection.normalized * movementSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
    private void OnCollisionEnter(Collision other)
    {
        GenericFight genericFight = other.gameObject.GetComponent<GenericFight>();
        Wall wall = other.gameObject.GetComponent<Wall>();

        if (genericFight != null && owner != genericFight.owner)
        {
            Fight(genericFight);
            genericFight = null;
            unitWalkDirection = owner == 1 ? new Vector3(1, 0, 0) : new Vector3(-1, 0, 0);
            if(warriorType == WarriorType.SIEGE)
            ballistaEncounteredWarrior = true;
        }
        else if (wall != null)
        {
            Fight(wall);
            if(warriorType == WarriorType.SIEGE)
            ballistaEncounteredWarrior = false;
        }
        if (power <= 0)
        {
            StartCoroutine("Die");
        }
    }

    void Fight(GenericFight genericFight)
    {
        if (warriorType == WarriorType.PIKEMAN && genericFight.warriorType == WarriorType.ARCHER ||
        warriorType == WarriorType.SHIELDMAN && genericFight.warriorType == WarriorType.PIKEMAN ||
        warriorType == WarriorType.ARCHER && genericFight.warriorType == WarriorType.SHIELDMAN
        )
        {
            damageToDeal /= 2;
        }
        genericFight.power -= damageToDeal;

        if (warriorType == genericFight.warriorType)
        {
            genericFight.StartCoroutine("Die");
            StartCoroutine("Die");
        }
        if (power <= 0)
            StartCoroutine("Die");
        if (genericFight.power <= 0)
            genericFight.StartCoroutine("Die");;
    }
    void Fight(Wall wall)
    {
        int damage = warriorType == WarriorType.SIEGE && ballistaEncounteredWarrior == true ? 0 : power;
        Debug.Log(damage);
        if (!wall.isInvinsible && wall.health > 0)
            wall.TakeDamage(damage);
        if(wall.health <= 0){
            if(wall.gameObject.CompareTag("Wall"))
            { 
                Destroy(wall.gameObject);
            }
            if(wall.gameObject.CompareTag("Citadel") || wall.gameObject.CompareTag("Barracks") ||wall.gameObject.CompareTag("Mine")){
                StartCoroutine("Die");
            }  
        }
        else{
            StartCoroutine("Die");
        }
    }

    public IEnumerator Die()
    {
        deathEffect.Play();
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.useGravity = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        //Just in case
        StopCoroutine("Die");
    }
    public void UpgradeToKnight(){
        if(gameManager.gamePhase == GamePhase.PLACEMENT && warriorType != WarriorType.KNIGHT && warriorType != WarriorType.SIEGE)
        {
            if (gameManager.turnOwner == 1 && gameManager.coinsPl1.coinsPl1 >= 2)
            {
                gameManager.coinsPl1.coinsPl1 -= 2;
                gameManager.coinsPl1.Pl1CoinText.text = gameManager.coinsPl1.coinsPl1.ToString();
                clicker.dropdownManager.UpgradeUnit("Knight");
            }
            else
            {
                Debug.Log("Cannot Upgrade");
            }
            if (gameManager.turnOwner == 2 && gameManager.coinsPl2.coinsPl2 >= 2)
            {
                gameManager.coinsPl2.coinsPl2 -= 2;
                gameManager.coinsPl2.Pl2CoinText.text = gameManager.coinsPl2.coinsPl2.ToString();
                clicker.dropdownManager.UpgradeUnit("Knight");
            }
            else
            {
                Debug.Log("Cannot Upgrade2");
            }
        }       
    }
    public void UpgradeToBallista(){
        if(gameManager.gamePhase == GamePhase.PLACEMENT && warriorType != WarriorType.SIEGE)
        {
            if (gameManager.turnOwner == 1 && gameManager.coinsPl1.coinsPl1 >= 3)
            {
                gameManager.coinsPl1.coinsPl1 -= 3;
                gameManager.coinsPl1.Pl1CoinText.text = gameManager.coinsPl1.coinsPl1.ToString();
                clicker.dropdownManager.UpgradeUnit("Ballista");
            }
            else
            {
                Debug.Log("Cannot Upgrade");
            }
            if (gameManager.turnOwner == 2 && gameManager.coinsPl2.coinsPl2 >= 3)
            {
                gameManager.coinsPl2.coinsPl2 -= 3;
                gameManager.coinsPl2.Pl2CoinText.text = gameManager.coinsPl2.coinsPl2.ToString(); 
                clicker.dropdownManager.UpgradeUnit("Ballista");
            }
            else
            {
                Debug.Log("Cannot Upgrade2");
            }
        }       
    }
    public void DeleteUnit(){
        
        if(gameManager.gamePhase == GamePhase.PLACEMENT)
        {
            uiManager.EnableUpgradeKnightButton(false);
            uiManager.EnableDeleteUnitButton(false);
            Destroy(gameObject);
        }
        
    }
    public void OnClick()
    {
        WarriorSlot unitWarriorSlot = clicker.slots.ToList().Find(slot => slot.GetComponent<WarriorSlot>().warriorToSpawn == this.gameObject).GetComponent<WarriorSlot>();
        clicker.warriorSlot = unitWarriorSlot;
        clicker.warriorSlot.Select();
        
    }
}