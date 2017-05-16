using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorAction : MonoBehaviour {

	public GameObject hudText;
	public Text nbKey;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") 
		{
			if (int.Parse (nbKey.text) > 0) 
			{
				hudText.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					GetComponent<Animator>().SetBool ("open", true);
					nbKey.text = "" + (int.Parse (nbKey.text) - 1);
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
