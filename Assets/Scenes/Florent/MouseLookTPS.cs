using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class MouseLookTPS : MonoBehaviour
{
    private Quaternion m_CameraTargetRot;

    // Use this for initialization
    void Start () {
        m_CameraTargetRot = Camera.main.transform.localRotation;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Fixed update is called in sync with physics
    void FixedUpdate () {
        if (CrossPlatformInputManager.GetButtonUp("Fire3")) {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = (Cursor.visible) ? CursorLockMode.None : CursorLockMode.Locked;
        }

        if (!Cursor.visible) {
            float xRot = CrossPlatformInputManager.GetAxis("Mouse Y") * 2f;
            m_CameraTargetRot *= Quaternion.Euler(-xRot, 0f, 0f);
            Camera.main.transform.localRotation = m_CameraTargetRot;
        }
    }
}
