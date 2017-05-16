using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseZone : MonoBehaviour {

	public GameObject door;
	public bool isPlayer;

	public List<GameObject> torchs;

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player") 
		{
			door.GetComponent<Animator> ().SetBool ("open", false);
			isPlayer = true;
			foreach (GameObject torch in torchs) 
			{
				torch.SetActive (false);
			}
		}
	}
}
