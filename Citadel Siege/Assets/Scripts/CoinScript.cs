using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinScript : MonoBehaviour
{
    Vector3 diceVelocity;
	public int coinsPl1;
	public Text Pl1CoinText;
	public static int diceNumber;
	public Rigidbody DiceBody;

	public GameObject fallPlatform;

	public Text HowMuchIRolled;

	public Button DiceButton;

	int diceNumberLocal;

	public float dirX;
	public float dirY;
	public float dirZ;

	public int counter = 0;
	public int limit = 3;
	public Wall whiteMine;

	// Use this for initialization


	// Update is called once per frame
	void Update()
	{
		Pl1CoinText.text = coinsPl1.ToString();
		HowMuchIRolled.text = diceNumberLocal.ToString();
		if(whiteMine.health <= 0)
		limit = 1;

		if(counter >= limit)
        {
			DiceButton.interactable = false;
        }

	}

	// Start is called before the first frame update


    // Update is called once per frame
    

	void OnTriggerStay(Collider col)
	{
		if (diceVelocity.x == 0f && diceVelocity.y == 0f && diceVelocity.z == 0f)
		{
			DiceBody.transform.position = new Vector3(506, 8, 559);
			DiceBody.transform.rotation = Quaternion.identity;
			fallPlatform.SetActive(true);
			DiceButton.interactable = true;
			switch (col.gameObject.name)
			{
				case "Side1":
					DiceNumberTextScript.diceNumber = 6;

					coinsPl1 += 2;

					diceNumberLocal = 6;
					counter++;
					





					



					break;
				case "Side2":
					DiceNumberTextScript.diceNumber = 5;

					coinsPl1 += 1;

					diceNumberLocal = 5;
					counter++;








					break;
				case "Side3":
					DiceNumberTextScript.diceNumber = 4;
					coinsPl1 += 1;

					diceNumberLocal = 4;
					counter++;



					break;
				case "Side4":
					DiceNumberTextScript.diceNumber = 3;

					coinsPl1 += 1;

					diceNumberLocal = 3;
					counter++;

					break;
				case "Side5":
					DiceNumberTextScript.diceNumber = 2;

					coinsPl1 += 0;

					diceNumberLocal = 2;
					counter++;

					break;
				case "Side6":
					DiceNumberTextScript.diceNumber = 1;
					coinsPl1 += 0;

					diceNumberLocal = 1;
					counter++;


					break;
			}
		}
	}
}
