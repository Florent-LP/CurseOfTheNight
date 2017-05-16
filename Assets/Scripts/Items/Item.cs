using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	private Material defaultMaterial;
	public Material highlightMaterial;

	private bool isNear;

	void OnTriggerEnter(Collider other)
	{
		if(other.transform.gameObject.tag == "Player")
		{
			Debug.Log ("Player touched me!!!!!");
			if (!isNear) 
			{
				defaultMaterial = GetComponent<Renderer> ().material;
				isNear = true;
			}
			GetComponent<Renderer> ().material = highlightMaterial;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.transform.gameObject.tag == "Player")
		{
			Debug.Log ("GET OUT!!!!!");
			GetComponent<Renderer> ().material = defaultMaterial;
		}
	}

}
