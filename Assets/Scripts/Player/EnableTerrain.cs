using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTerrain : MonoBehaviour {

    public Collider player; // assign in inspector?
    public TerrainCollider tCollider; // assign in inspector?

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            Debug.Log("false");
            Physics.IgnoreCollision(player, tCollider, false);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
