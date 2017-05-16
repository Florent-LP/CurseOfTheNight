using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatCodes : MonoBehaviour {

	public GameObject enemies;
	public GameObject player;

	private float interval = 0.3f;
	private float nextCheat;

	public List<GameObject> tp;

	private PlayerHealth playerHP;
	private PlayerMana playerMana;
	private PlayerController charControl;

	private bool active; //Si les ennemis sont actifs
	private int pos; //Position des points de TP

	private bool speedActive;
	private float normalSpeed; //Vitesse de base
	private float cheatSpeed; //Vitesse de triche

	// Use this for initialization
	void Start () 
	{
		playerHP = player.GetComponent<PlayerHealth> ();
		playerMana = player.GetComponent<PlayerMana> ();

		charControl = player.GetComponent<PlayerController> ();

		active = true;

		pos = 0;

		speedActive = false;
		normalSpeed = charControl.walkSpeed;
		cheatSpeed = normalSpeed*3;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Time.time > nextCheat) 
		{
			if (Input.GetKey ("p")) 
			{
				playerHP.currentHealth = playerHP.maxHealth;
				playerHP.healthSlider.value = playerHP.currentHealth;
				nextCheat = Time.time + interval;
			}
			if (Input.GetKey ("o")) 
			{
				playerMana.currentMana = playerMana.maxMana;
				playerMana.manaSlider.value = playerMana.currentMana;
				nextCheat = Time.time + interval;
			}
			if (Input.GetKey ("i")) 
			{
				active = !active;
				enemies.SetActive (active);
				nextCheat = Time.time + interval;
			}
			if (Input.GetKey ("m")) 
			{
				playerHP.Die ();
				nextCheat = Time.time + interval;
			}
			if (Input.GetKey ("l")) 
			{
				if (speedActive)
					charControl.walkSpeed = normalSpeed;
				else
					charControl.walkSpeed = cheatSpeed;
				
				charControl.runSpeed = 2 * charControl.walkSpeed;
				charControl.crouchSpeed = charControl.walkSpeed-1;

				speedActive = !speedActive;
				nextCheat = Time.time + interval;
			}
			if (Input.GetKey ("return")) 
			{
				player.transform.position = tp [pos].transform.position;
				pos++;
				if (pos >= tp.Count)
					pos = 0;
				nextCheat = Time.time + interval;
			}
		}
	}
}
