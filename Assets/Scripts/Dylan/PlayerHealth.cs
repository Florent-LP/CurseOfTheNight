using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//Appliquer sur le joueur
[RequireComponent(typeof(AudioSource))]
public class PlayerHealth : MonoBehaviour
{
    public Transform endGamePanel;
    public Transform gameOverPanel;
    public CamMouseLook cam;

    public int maxHealth = 100;
    public int currentHealth;
    public Slider healthSlider; //Barre de vie

	//public float tempsInvincibilite = 0.567f;

	public bool isDead = false;

	private bool invincibility = false;

	private Animator anim;
	private AudioSource audio;

    void Awake()
    {
		currentHealth = maxHealth;
		healthSlider.maxValue = maxHealth;
		healthSlider.value = currentHealth; //MaJ de la barre
		anim = GetComponent<Animator>();
		AudioSource[] audios = GetComponents<AudioSource>();
		audio = audios[1];
    }

	public IEnumerator TakeDamage(int amount, float timeInvincibility)
    {
		if (!invincibility)
		{
			if (anim.GetBool ("isParrying"))
				amount /= 2;
			
            anim.SetBool("isTakingDamage", true);
			currentHealth -= amount; //Réduction de PV
			healthSlider.value = currentHealth; //MaJ de la barre

            if (currentHealth <= 0 && !isDead)
                Die();
            else
            {
                //Temps d'invincibilité après s'être fait touché
                invincibility = true;
				yield return new WaitForSeconds(timeInvincibility);
				invincibility = false;
				anim.SetBool("isTakingDamage", false);
            }
		}
    }


    public void Die()
	{
		audio.Play ();
		isDead = true;
		anim.SetBool ("isDying", true);
        StartCoroutine(printRestartPanel());
    }

    public IEnumerator printRestartPanel()
    {
        yield return new WaitForSeconds(3);

        // Code to execute after the delay
        endGamePanel.gameObject.SetActive(true);
        gameOverPanel.gameObject.SetActive(true);
        cam.paused = true;
        cam.dead = true;
        Time.timeScale = 0;
    }
}