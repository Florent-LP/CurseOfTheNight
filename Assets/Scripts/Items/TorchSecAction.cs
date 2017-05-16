using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchSecAction : MonoBehaviour {

	public GameObject door;
	private float incr1;

	private bool isAction;
	private bool canOpen;

	public GameObject hudText;

	private bool sound;

	void Start () 
	{
		incr1 = 0;
	}

	void OnTriggerStay(Collider other)
	{
		if (GetComponent<TorchSecAction> ().enabled == true) 
		{
			if (other.tag == "Player") 
			{
				hudText.SetActive (true);
				if (Input.GetKeyDown (KeyCode.E)) 
				{
					isAction = true;
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

	void FixedUpdate () 
	{
		if (isAction) 
		{
			incr1 += 0.75f * Time.deltaTime;
			RotateTorch ();
		}
		if (canOpen) 
		{
			Invoke ("OpenDoor", 1.0f);
		}
	}

	public void RotateTorch()
	{
		Debug.Log ("incr1: " + incr1 * 100);
		if (incr1*100 < 75) 
		{
			transform.Rotate (Vector3.right * -incr1);
		} 
		else 
		{
			canOpen = true;
			isAction = false;
		}
	}

	public void OpenDoor()
	{
		door.GetComponent<Animator> ().SetBool ("open", true);
		canOpen = false;
		GetComponent<HiddenItem> ().enabled = false;
		GetComponent<TorchSecAction> ().enabled = false;
	}
}
