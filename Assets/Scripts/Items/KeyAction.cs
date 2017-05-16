using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyAction : MonoBehaviour {

	public GameObject hudText;
	public Text nbKey;
	private bool isTaken;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			hudText.SetActive (true);
		}
	}

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") 
		{
			if (Input.GetKeyDown (KeyCode.E)) 
			{
				if (!isTaken) 
				{
					nbKey.text = "" + (int.Parse (nbKey.text) + 1);
					isTaken = true;
					hudText.SetActive (false);
					Destroy (transform.gameObject);
				}
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		hudText.SetActive (false);
	}

}
