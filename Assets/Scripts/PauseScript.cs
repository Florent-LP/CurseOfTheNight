using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {
    public void BackToMainMenu()
    {
        Time.timeScale = 1;
        enabled = true;
        SceneManager.LoadScene("MainMenu");
    }

    public void Restart()
    {
        Time.timeScale = 1;
        enabled = true;
        SceneManager.LoadScene("DemoSprint1");
    }
}
