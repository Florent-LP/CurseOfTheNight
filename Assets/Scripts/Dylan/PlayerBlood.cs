using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerBlood : MonoBehaviour 
{
	public int maxBlood = 500;
	public int currentBlood;
	public Slider bloodSlider;
	public int bloodLostPerSecond = 1;

	private float tempsSang = 1f;
	private float nextSang;

	private PlayerHealth ph;

	void Awake()
	{
		currentBlood = maxBlood;
		bloodSlider.maxValue = maxBlood;
		bloodSlider.value = currentBlood;
		nextSang = 0.0f;
		ph = GetComponent<PlayerHealth>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Time.time > nextSang) 
		{
			nextSang = Time.time + tempsSang;
			currentBlood -= bloodLostPerSecond;
			bloodSlider.value = currentBlood;
		}
		if (currentBlood < 0)
			ph.Die();
	}

	public void addBlood(int amount)
	{
		currentBlood += amount;
		if (currentBlood > maxBlood)
			currentBlood = maxBlood;
	}
}
