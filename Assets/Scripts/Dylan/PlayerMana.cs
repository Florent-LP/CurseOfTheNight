using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMana : MonoBehaviour {

	public int maxMana = 100;
	public int currentMana;
    public int manaRegen = 0; // Par seconde, laisser à 0 pour désactiver
    protected float lastRegen = 0;
	public Slider manaSlider; //Barre de vie

	void Awake()
	{
		currentMana = maxMana;
		manaSlider.maxValue = maxMana;
		manaSlider.value = currentMana; //MaJ de la barre
	}

	void Update()
	{
		if (manaRegen > 0 && currentMana < 100 && Time.time - lastRegen >= 1)
        {
            currentMana += manaRegen;
            if (currentMana > 100) currentMana = 100;
            manaSlider.value = currentMana;
            lastRegen = Time.time;
        }
	}


	public bool UseMana(int amount)
	{
        if (currentMana - amount >= 0)
        {
            currentMana -= amount; //Réduction de mana
            manaSlider.value = currentMana; //MaJ de la barre
            return true;
        }
        return false;
	}
}
