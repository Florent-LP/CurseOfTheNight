using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableTerrain : MonoBehaviour {

    public Collider player; // assign in inspector?
    public TerrainCollider tCollider; // assign in inspector?

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            Debug.Log("true");
            Physics.IgnoreCollision(player, tCollider, true);
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
