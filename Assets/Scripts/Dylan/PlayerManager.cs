using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Appliquer sur un objet vide qui va gérer le jeu
public class PlayerManager : MonoBehaviour{

	public GameObject player;

	private PlayerHealth hp;
	private PlayerMana mp;

    // Use this for initialization
    void Start ()
    {
		hp = player.GetComponent<PlayerHealth>();
		mp = player.GetComponent<PlayerMana>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKey("h"))
        {
			hp.TakeDamage (1,0);
        }
        if (Input.GetKey("m"))
        {
			mp.UseMana (1);
        }
    }
}
