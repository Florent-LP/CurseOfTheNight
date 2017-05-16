using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Statistics : MonoBehaviour {

	public static int nbPlayerKills;
	public static int nbPlayerDetected;

	void Awake () 
	{
		nbPlayerKills = 0;
		nbPlayerDetected = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
