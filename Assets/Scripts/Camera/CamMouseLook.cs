using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMouseLook : MonoBehaviour {

	Vector2 mouseLook;
	Vector2 smoothV;
	public float sensitivity = 5.0f;
	public float smoothing = 2.0f;
	public bool m_cursorIsLocked = true;
    public bool paused = false;
    public bool dead = false;
    public bool book = false;

    GameObject character;

    private Animator anim;

    // Use this for initialization
    void Start () {
        //character = this.transform.parent.parent.parent.parent.gameObject;
        character = this.transform.parent.gameObject;

        anim = character.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!anim.GetBool("isDying") && !paused && !dead && !book)
        {
            var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
            smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
            smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
            mouseLook += smoothV;
            mouseLook.y = Mathf.Clamp(mouseLook.y, -90f, 90f);

            transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
            character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        }

        if (Input.GetKeyUp(KeyCode.Escape) && !paused && !dead && !book)
		{
            paused = true;
		}
        else if (Input.GetKeyUp(KeyCode.Escape) && paused && !dead && !book)
        {
            paused = false;
        }

        if (!paused && !dead && !book)
        {
            m_cursorIsLocked = true;
        }
        else
        {
            m_cursorIsLocked = false;
        }

		if (m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		else if (!m_cursorIsLocked)
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}
}
