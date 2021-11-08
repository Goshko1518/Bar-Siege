using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    public WarriorSlot[] whiteSlots;
    public WarriorSlot[] blackSlots;
    private GameManager gameManager;
    private GameObject gameManagerObject;
    // Start is called before the first frame update
    void Start()
    {
        gameManagerObject = GameObject.FindGameObjectWithTag("Game Manager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public WarriorSlot[] GetCorrespondingSlots(GenericFight selectedWarrior){
        WarriorSlot[] toReturn = selectedWarrior.owner == 1 ? whiteSlots : blackSlots;
        return toReturn;
    }
}
