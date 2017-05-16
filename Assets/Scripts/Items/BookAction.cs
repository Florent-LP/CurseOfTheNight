using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookAction : MonoBehaviour {

    public Transform bookPanel;
    public Transform blackPanel;
    public CamMouseLook cam;

    public GameObject hudText;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            hudText.SetActive(true);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hudText.SetActive(false);
                cam.book = true;
                if (!bookPanel.gameObject.activeInHierarchy)
                {
                    bookPanel.gameObject.SetActive(true);
                    blackPanel.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }   
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        hudText.SetActive(false);
    }
}
