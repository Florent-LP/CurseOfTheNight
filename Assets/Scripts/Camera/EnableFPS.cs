using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Cameras;

public class EnableFPS : MonoBehaviour {

    public GameObject multiPurposeCameraRig; // assign in inspector?
    public GameObject pivot; // assign in inspector?
    public GameObject mainCamera; // assign in inspector?

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Player")
        {
            multiPurposeCameraRig.GetComponent<ProtectCameraFromWallClip>().enabled = false;
            multiPurposeCameraRig.transform.localPosition = new Vector3(0f, 0.5f, 0.2f);
            pivot.transform.localPosition = new Vector3(0f, 0f, 0f);
            mainCamera.transform.localPosition = new Vector3(0f, 0f, 0f);
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
