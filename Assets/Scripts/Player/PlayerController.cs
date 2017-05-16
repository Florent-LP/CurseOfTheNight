using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerController : MonoBehaviour {

    private Animator anim;
    private Rigidbody rb;
    private CapsuleCollider cc;
    private CharacterController charController;

    private bool uncrouchable = true;
    private float speed;

	public float crouchSpeed = 1.0f;
	public float walkSpeed = 2.0f;
	public float runSpeed = 6.0f;
    public float dodgeSpeed = 5.0f;
    
    private float verticalVelocity;
    public float gravity = 9.0f;
    public float jumpForce = 5.0f;

    private float caraControlerMaxHeight;
    private Vector3 caraControlerMaxCenter;
    private float caraControlerMaxRadius;

    private float caraControlerCroutchHeight;
    private Vector3 caraControlerCroutchCenter;
    private float caraControlerCroutchRadius;

    public LayerMask npcMask;
	public float detectionRadius;

  public float enableNpcRadius;

	private AudioSource audioJump;
	private AudioSource audioSiffle;
	private AudioSource audioWalk;

  private Collider[] npcToEnable;

  [HideInInspector]
  public bool detected;

    // Use this for initialization
    void Start () 
	{
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        charController = GetComponent<CharacterController>();
  
        caraControlerMaxHeight = charController.height;
        caraControlerMaxCenter = charController.center;
        caraControlerMaxRadius = charController.radius;

        caraControlerCroutchHeight = caraControlerMaxHeight * 0.6f;
        caraControlerCroutchCenter = caraControlerMaxCenter * 0.55f;
        caraControlerCroutchRadius = 0.3f;

        Cursor.lockState = CursorLockMode.Locked;
		speed = walkSpeed;

		AudioSource[] audios = GetComponents<AudioSource>();
		audioJump = audios[2];
		audioSiffle = audios[3];
		audioWalk = audios[4];

	  detected = false;
	}



  void applyRunChanges()
  {
    Collider[] npcInHearingRadius = Physics.OverlapSphere(transform.position, detectionRadius, npcMask);

    speed = runSpeed;

    for (int i = 0; i < npcInHearingRadius.Length; ++i)
    {
      if (!npcInHearingRadius[i].transform.GetComponent<Animator>().GetBool("isDead"))
      {
        Transform target = npcInHearingRadius[i].transform;
        Vector3 dirToTarget = (target.position - transform.position).normalized;
        float dstToTarget = Vector3.Distance(transform.position, target.position);
        RaycastHit hit;

        Physics.Raycast(transform.position, dirToTarget, out hit, dstToTarget);

        if (hit.collider.gameObject.layer == target.gameObject.layer)
        {
          GolemPatrol goForit = target.gameObject.GetComponent<GolemPatrol>();
          goForit.ChasePlayerFromSound(transform.position);
        }
      }
    }
  }

  void enableNpc()
  {
    Collider[] npcToEnable = Physics.OverlapSphere(transform.position, detectionRadius, npcMask);

    for (int i = 0; i < npcToEnable.Length; ++i)
    {
      if (!npcToEnable[i].transform.GetComponent<Animator>().GetBool("isDead"))
      {
        FieldOfViewPatroller enemy = npcToEnable[i].transform.gameObject.GetComponent<FieldOfViewPatroller>();
        enemy.shouldLookForPlayer = true;
      }
    }
  }
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		float translation = Input.GetAxis ("Vertical") * speed;
		float straffe = Input.GetAxis ("Horizontal") * speed;
		translation *= Time.deltaTime;
		straffe *= Time.deltaTime;

    enableNpc();

        if (anim.GetBool("isCrouching"))
        {
            Vector3 Up = transform.TransformDirection(Vector3.up);
            if (Physics.Raycast(transform.position + new Vector3(0, caraControlerCroutchHeight * 0.5f, 0), Up, 1.15f))
            {
                uncrouchable = false;
            }
            else
            {
                uncrouchable = true;
            }
        }

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0)
        {
			//audioWalk.Play ();
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (Input.GetAxis("Crouch") != 0)//Input.GetKey(KeyCode.X)
        {
            anim.SetBool("isCrouching", true);
        }
        else
        {
            if (uncrouchable)
            {
                anim.SetBool("isCrouching", false);
            }
            else
            {
                anim.SetBool("isCrouching", true);
            }
        }

        if (Input.GetAxis("Run") != 0 && uncrouchable)//Input.GetKey(KeyCode.LeftShift)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetAxis("Punch") !=0 && uncrouchable)//Input.GetMouseButtonDown(0)
        {
            anim.SetBool("isPunching", true);
        }
        else
        {
            if (anim.GetNextAnimatorStateInfo(0).IsTag("Idle"))
            {
                anim.SetBool("isPunching", false);
            }
        }

        if (Input.GetAxis("Parry") != 0 && uncrouchable)
        {
            anim.SetBool("isParrying", true);
        }
        else
        {
            anim.SetBool("isParrying", false);
        }

        if (Input.GetAxis("Kick") != 0 && uncrouchable)
        {
            anim.SetBool("isKicking", true);
        }
        else
        {
            if (anim.GetNextAnimatorStateInfo(0).IsTag("Idle"))
            {
                anim.SetBool("isKicking", false);
            }
        }

        /*if (Input.GetKey(KeyCode.B))
        {
            anim.SetBool("isTakingDamage", true);
        }
        else
        {
            if (anim.GetNextAnimatorStateInfo(0).IsTag("Idle"))
            {
                anim.SetBool("isTakingDamage", false);
            }
        }

        if (Input.GetKey(KeyCode.N))
        {
            anim.SetBool("isDying", true);
        }*/


        if (anim.GetBool("isRunning") && !anim.GetBool("isCrouching") && !anim.GetBool("isDodging"))
        {
            applyRunChanges();
            speed = runSpeed;
        }
        else if (anim.GetBool("isCrouching") && !anim.GetBool("isDodging"))
        {
            speed = crouchSpeed;
        }
        else if (anim.GetBool("isDodging"))
        {
            speed = dodgeSpeed;
        }
        else
        {
            speed = walkSpeed;
        }


        //Capsule Collider modification
        if (anim.GetBool("isCrouching"))
        {
            //if (cc.height != ccMinimumHeight)
            //{
            //    cc.height = ccMinimumHeight;
            //}
            //if(cc.center != ccMinimumCenter)
            //{
            //    cc.center = ccMinimumCenter;
            //}
          

            if (charController.height != caraControlerCroutchHeight)
                charController.height = caraControlerCroutchHeight;
            if (charController.center != caraControlerCroutchCenter)
                charController.center = caraControlerCroutchCenter;
            if (charController.radius != caraControlerCroutchRadius)
                charController.radius = caraControlerCroutchRadius;
        }
        else
        {
            //if (cc.height != ccMaximumHeight)
            //{
            //    cc.height = ccMaximumHeight;
            //}
            //if (cc.center != ccMaximumCenter)
            //{
            //    cc.center = ccMaximumCenter;
            //}
            //var startPos = transform.position + new Vector3(0, caraControlerCroutchHeight - (caraControlerMaxHeight * 0.5f), 0);
            //var length = (caraControlerMaxHeight - caraControlerCroutchHeight);
            //print(startPos);
            /*if (Physics.Raycast(transform.position, Vector3.up, caraControlerMaxHeight))
            { print("2"); }*/

                if (charController.height != caraControlerMaxHeight)
                    charController.height = caraControlerMaxHeight;
                if (charController.center != caraControlerMaxCenter)
                    charController.center = caraControlerMaxCenter;
            if (charController.radius != caraControlerMaxRadius)
                charController.radius = caraControlerMaxRadius;
        }

        //Jump and dodge mechanisms
        if (charController.isGrounded)
        {
            if (!anim.GetNextAnimatorStateInfo(0).IsTag("Jump"))
            {
                anim.SetBool("isJumping", false);
            }
            verticalVelocity = -gravity * Time.deltaTime;
            if ((Input.GetAxis("Jump") != 0 && Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0) 
        || (Input.GetAxis("Jump") != 0 && Input.GetAxis("Parry") != 0 && Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0) && uncrouchable)/*if ((Input.GetKeyDown(KeyCode.Space) && Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0) 
        || (Input.GetKeyDown(KeyCode.Space) && Input.GetMouseButton(1) && Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0) && uncrouchable)*/
            {
                anim.SetBool("isDodging", true);
                verticalVelocity = jumpForce;
                speed = dodgeSpeed;
            }
            else
            {
                if (!anim.GetNextAnimatorStateInfo(0).IsTag("Dodge") || (!anim.GetNextAnimatorStateInfo(0).IsTag("Dodge") && !anim.GetBool("isParrying")))
                {
                    anim.SetBool("isDodging", false);
                }
            }
            if (Input.GetAxis("Jump") != 0 && !anim.GetBool("isDodging") && uncrouchable)
            {
				audioJump.Play();
                anim.SetBool("isJumping", true);
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity -= gravity * Time.deltaTime;
        }

        Vector3 moveVector = Vector3.zero;
        moveVector.y = verticalVelocity;
        if ((!anim.GetBool("isDying") && !anim.GetBool("isTakingDamage") && !anim.GetBool("isParrying")) || (anim.GetBool("isParrying") && anim.GetBool("isDodging")))
        {
            charController.Move(moveVector * Time.deltaTime);
            transform.Translate(straffe, 0, translation);
        }


        /*if (Input.GetKeyDown ("escape"))
			Cursor.lockState = CursorLockMode.None;*/
    }   
}
