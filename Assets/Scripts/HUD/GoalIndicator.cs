using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalIndicator : MonoBehaviour {

	public Transform player;
	public Transform goalPosition;
	private float distance;
	private TextMesh time;

	private bool isArrived;

	void Start () 
	{
		distance = 0;
		time = goalPosition.GetComponent<TextMesh> ();
	}
	
	void FixedUpdate () 
	{
		if (!isArrived) 
		{
			distance = Vector3.Distance (player.position, goalPosition.position);
			int d = Mathf.RoundToInt (distance);
			time.text = "" + d + "m";
			if (d <= 0) 
			{
				time.text = "";
				isArrived = true;
			}
		}
	}
}
