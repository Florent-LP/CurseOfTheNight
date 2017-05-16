using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchAction : MonoBehaviour {

	public GameObject door;
	private float doorHeight;
	private float incr1;
	private float incr2;

	private bool isAction;
	private bool canOpen;

	public GameObject hudText;

	private AudioSource audio;
	private bool sound;

	void Start () 
	{
		doorHeight = door.GetComponent<MeshCollider> ().bounds.size.y;
		audio = door.GetComponent<AudioSource> ();
		incr1 = 0;
		incr2= 0;
		//Debug.Log ("doorHeight: " + doorHeight);
	}

	void OnTriggerStay(Collider other)
	{
		if (GetComponent<TorchAction> ().enabled == true) 
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
			incr2 += 0.025f * Time.deltaTime;
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
		if (door!= null && door.transform.position.y <= doorHeight && !sound) 
		{
			audio.PlayOneShot (audio.clip);
			sound = true;
		}

		if (door != null && door.transform.position.y <= doorHeight + 1) 
		{		
			door.transform.position = new Vector3 (door.transform.position.x, door.transform.position.y + incr2, door.transform.position.z);
		} 
		else 
		{
			canOpen = false;
			Destroy (door);
			GetComponent<TorchAction> ().enabled = false;
		}
	}
}
