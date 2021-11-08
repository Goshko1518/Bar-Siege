using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollForBarracks : MonoBehaviour
{
	// Start is called before the first frame update

	public static Vector3 diceVelocity;
	public GameObject DiceForUnits;
	public GameObject fallPlatform;

	public static Vector3 diceVelocityPl2;
	public GameObject DiceForUnitsPl2;
	public GameObject fallPlatformPl2;

	private UIManager buttons;

	// Use this for initialization
	void Start()
	{
		buttons = GameObject.FindGameObjectWithTag("UI Manager").GetComponent<UIManager>();
	}

	// Update is called once per frame
	void Update()
	{

	}
	public void Roll()
	{
		Rigidbody rb = DiceForUnits.gameObject.GetComponent<Rigidbody>();
		diceVelocity = rb.velocity;
		DiceNumberTextScript.diceNumber = 0;
		float dirX = Random.Range(0, 500);
		float dirY = Random.Range(0, 500);
		float dirZ = Random.Range(0, 500);

		rb.AddForce(transform.up * 500);
		rb.AddTorque(dirX, dirY, dirZ);
		fallPlatform.SetActive(false);
		buttons.DisableRollButtonUnitsPl1();
	}

	public void RollPl2()
	{
		Rigidbody rb = DiceForUnitsPl2.gameObject.GetComponent<Rigidbody>();
		diceVelocityPl2 = rb.velocity;
		DiceNumberTextScript.diceNumber = 0;
		float dirX = Random.Range(0, 500);
		float dirY = Random.Range(0, 500);
		float dirZ = Random.Range(0, 500);

		rb.AddForce(transform.up * 500);
		rb.AddTorque(dirX, dirY, dirZ);
		fallPlatformPl2.SetActive(false);
		buttons.DisableRollButtonUnitsPl2();
	}
}
