using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If you need help mapping xbox 360 controller behaviour:
// https://www.youtube.com/watch?time_continue=121&v=s5x-TqLqGWA

public class PlayerManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;
    public float playerSpeed = 2.5f;

    public Animator playerAc;

    AnimatorStateInfo animState;

    public bool canMoveAnim;
    public bool canMoveSpell;

    /// <summary>
    /// Set these in inspector because "Find" is fucking stupid.
    /// </summary>
    [Header("Attacking stuff")]
    public Player_AnimEvent weaponEvent;
    [Space(10)]
    [Header("Hitboxes")]
    public Collider trigger;
    public Collider sword;
    public Collider hitZone;

    [Space(10)]
    public bool meleeAttack = false;

    [Space(10)]
    [Header("Spells")]
    public Spell_Fireball sp_fireball;
    private float rangeCooldown = 0.5f;
    public bool beginRangeCooldown;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        playerAc = this.GetComponentInChildren<Animator>();
        animState = playerAc.GetCurrentAnimatorStateInfo(0);

        weaponEvent = this.GetComponentInChildren<Player_AnimEvent>();
        canMoveAnim = true;
        canMoveSpell = true;
    }

    private void Update()
    {
        CheckMoveState();

        if (Input.GetKey(KeyCode.M))
        {
            Debug.Log(animState);
        }
    }

    private void FixedUpdate()
    {
        RangeCooldown();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if (canMoveAnim && canMoveSpell)
        {
            Movement();
        }
        //Check trigger, check if animation is currently playing (cooldown)
        if (Input.GetAxisRaw("R_Trigger") == 1 && !playerAc.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1"))  //Xbox triggers are weird...treated like an axis. Unpressed 0, pressed 1.
        {
            BasicAttack();
        }
        // Button for ease
        if(Input.GetButton("R_Bumper") && !playerAc.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1"))
        {
            BasicRangedAttack();
            
            
        }

        ///Weapon event, because trigger enter/stay... SUCKS need to hide and show the collision volume. Based on the anim event of the sword swing.
        ///This way, because the damage is under TriggerEnter (see EnemyManager), the collision volume disappears and reappears
        ///activating properly. Otherwise if enemy just stands in front, they're not leaving the volume. 
        if (weaponEvent.hitting == false)
        {
            hitZone.enabled = false;
        }

        if (weaponEvent.hitting == true)
        {
            hitZone.enabled = true;
        }
        
    }
    
    /// <summary>
    /// Some rigid body quirks with capsule collider. Check to see if APPLY ROOT MOTION on the animator component in Inspector window is turned OFF.
    /// Otherwise the character mesh seems to pop out of the capsule bounds. Could also be an issue with Update (set to LateUpdate for physics...)
    /// </summary>
    void Movement()
    {
        Vector3 input_Lstick = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 input_Rstick = new Vector3(Input.GetAxisRaw("RStick_Horizontal"), 0.0f, -Input.GetAxisRaw("RStick_Vertical"));

        playerRb.MovePosition(transform.position + input_Lstick * Time.deltaTime * playerSpeed);

        if (input_Lstick.x != 0 || input_Lstick.z != 0)
        {
            int lookInversion = 1;
            if (input_Rstick.x == 0 && input_Rstick.z == 0)
            {
                playerRb.transform.rotation = Quaternion.LookRotation(input_Lstick);
                lookInversion = 1;
                Debug.Log("if a");
            }

            if (input_Rstick.x != 0 || input_Rstick.z != 0)
            {
                playerRb.transform.rotation = Quaternion.LookRotation(input_Rstick);
                lookInversion = -1;
                Debug.Log("if b");
            }

            playerAc.SetFloat("speedh", input_Lstick.x);
            playerAc.SetFloat("speedv", input_Lstick.z * lookInversion);

        }
        else if (input_Rstick.x != 0 || input_Rstick.z != 0)
        {
            playerRb.transform.rotation = Quaternion.LookRotation(input_Rstick);
            Debug.Log("if b");
        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("L_Bumper"))
        {
            playerSpeed = 4.0f;
        }
        else
        {
            playerSpeed = 2.5f;
        }
    }

    /// <summary>
    /// NOTE: ANIMATION quirks. 
    /// Make sure transistion duration is set to 0 in the inspector (when transition line is selected).
    /// Otherwise, pressing a button will fire the animation and then reset the animation during that transistion period (e.g. looks like animated unit is stuttering)
    /// </summary>
    void BasicAttack()
    {

        playerAc.SetTrigger("Attack1h1");
        playerAc.SetFloat("animSpeed", 1.0f);
        
    }

    
    void BasicRangedAttack()
    {
        ////If enabled, this animation calls "hitting" event which starts the collision hitzone. So...can't use this anim with current hitzone behvaiour
        ////of fucking course.
        //playerAc.SetTrigger("Attack1h1");
        //playerAc.SetFloat("animSpeed", 0.5f);
                
        if(beginRangeCooldown == false)
        {
            GameObject fireball;
            canMoveSpell = false;
            fireball = Instantiate(sp_fireball.gameObject, hitZone.transform.position, transform.rotation);
            beginRangeCooldown = true;
            playerAc.SetTrigger("Hit1");
        }
        
        
    }

    void CheckMoveState()
    {
        canMoveAnim = !playerAc.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1");  //canMove is opposite of attack state
        
    }

    void RangeCooldown()
    {
        Debug.Log(rangeCooldown);

        if(beginRangeCooldown == true)
        {
            rangeCooldown = rangeCooldown - Time.deltaTime;
        }
        
        if (rangeCooldown <= 0.0f)
        {
            canMoveSpell = true;
            beginRangeCooldown = false;
            rangeCooldown = 0.5f;
        }
        
    }

}
