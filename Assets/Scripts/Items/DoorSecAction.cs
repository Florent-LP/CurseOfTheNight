using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorSecAction : MonoBehaviour {

	public GameObject hudText;
	public GameObject CloseZone;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") 
		{
			if (!GetComponent<Animator>().GetBool("open") && !CloseZone.GetComponent<CloseZone>().isPlayer) 
			{
				hudText.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GetComponent<Animator> ().SetBool ("open", true);
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{

		if (other.tag == "Player") 
		{
			hudText.SetActive (false);
		}	
	}
}
