using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    public Transform pausePanel;
    public Transform bookPanel;
    public Transform blackPanel;
    public CamMouseLook cam;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) && !cam.dead && !cam.book)
        {
            if (!pausePanel.gameObject.activeInHierarchy)
            {
                pausePanel.gameObject.SetActive(true);
                blackPanel.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                pausePanel.gameObject.SetActive(false);
                blackPanel.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
        }
	}

    public void Unpause()
    {
        cam.paused = false;
        cam.book = false;
        if (pausePanel.gameObject.activeInHierarchy)
        {
            pausePanel.gameObject.SetActive(false);
        }
        if (bookPanel.gameObject.activeInHierarchy)
        {
            bookPanel.gameObject.SetActive(false);
        }
        if (blackPanel.gameObject.activeInHierarchy)
        {
            blackPanel.gameObject.SetActive(false);
        }
        Time.timeScale = 1;
    }
}
