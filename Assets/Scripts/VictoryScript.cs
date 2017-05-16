using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryScript : MonoBehaviour {
    public Transform blackPanel;
    public Transform victoryPanel;
    public Transform congratPanel;
    public Text victoryText;
    public Text rankText;
    public CamMouseLook cam;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cam.dead = true;
            if (!congratPanel.gameObject.activeInHierarchy)
            {
                congratPanel.gameObject.SetActive(true);
            }
            if (!victoryPanel.gameObject.activeInHierarchy)
            {
                victoryText.text = "You killed " + Statistics.nbPlayerKills + " enemies.\n"
                    + "You have been detected " + Statistics.nbPlayerDetected + " times.";
                if (Statistics.nbPlayerDetected <= 0 && Statistics.nbPlayerKills <= 1)
                {
                    rankText.text = "Rank: Ghost";
                }
                else if (Statistics.nbPlayerDetected <= 2 && Statistics.nbPlayerKills > 1)
                {
                    rankText.text = "Rank: Vampire Lord";
                }
                else if (Statistics.nbPlayerDetected > 2 && Statistics.nbPlayerKills <= 4)
                {
                    rankText.text = "Rank: Lone Wolf";
                }
                else
                {
                    rankText.text = "Rank: Butcher";
                }
                victoryPanel.gameObject.SetActive(true);
                blackPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}