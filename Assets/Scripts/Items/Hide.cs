using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour {

	public GameObject hudText;
	private bool isHidden;

	void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player") 
		{
			hudText.SetActive (true);
				
			if (!isHidden) 
			{
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					HideIt (other.transform);
				}
			}
			else 
			{
				if (Input.GetKeyDown (KeyCode.E))
				{
					RevealIt (other.transform, 1.5f);
				}
			}
		}
	}

	void Update()
	{
		if (!isHidden) 
		{
			if (Input.GetKeyDown (KeyCode.E)) 
			{
				isHidden = true;
			}
		}
		else 
		{
			if (Input.GetKeyDown (KeyCode.E))
			{
				isHidden = false;
			}
		}
	}

	void OnTriggerExit(Collider other)
	{

		if (other.tag == "Player") 
		{
			hudText.SetActive (false);
			other.transform.GetChild (1).gameObject.SetActive (true);
			other.transform.GetComponent<PlayerController> ().enabled = true;
		}	
	}

	void HideIt(Transform t)
	{
		t.position = transform.position;
		t.GetChild (1).gameObject.SetActive (false);
		t.GetComponent<PlayerController> ().enabled = false;
	}

	void RevealIt(Transform t, float d)
	{
		t.GetChild (1).gameObject.SetActive (true);
		t.GetComponent<PlayerController> ().enabled = true;
		//Vector3 moveVector = Vector3.forward * d;
		t.GetComponent<CharacterController>().Move(t.forward * d);
	}
}
