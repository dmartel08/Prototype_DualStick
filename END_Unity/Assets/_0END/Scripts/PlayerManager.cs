﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// If you need help mapping xbox 360 controller behaviour:
// https://www.youtube.com/watch?time_continue=121&v=s5x-TqLqGWA

public class PlayerManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;
    public float playerSpeed = 2;

    public Animator playerAc;

    AnimatorStateInfo animState;

    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        playerAc = this.GetComponentInChildren<Animator>();
        animState = playerAc.GetCurrentAnimatorStateInfo(0);

        canMove = true;
    }

    private void Update()
    {
        CheckMoveState();

        if (Input.GetKey(KeyCode.M))
        {
            Debug.Log(animState);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (canMove)
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
            BasicAttack();
        }
    }

    void Movement()
    {
        Vector3 input_Lstick = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 input_Rstick = new Vector3(Input.GetAxisRaw("RStick_Horizontal"), 0.0f, -Input.GetAxisRaw("RStick_Vertical"));

        playerRb.MovePosition(transform.position + input_Lstick * Time.deltaTime * playerSpeed);

        if (input_Lstick.x != 0 || input_Lstick.z != 0)
        {
            if (input_Rstick.x == 0 && input_Rstick.z == 0)
            {
                playerRb.transform.rotation = Quaternion.LookRotation(input_Lstick);
                Debug.Log("if a");
            }

            if (input_Rstick.x != 0 || input_Rstick.z != 0)
            {
                playerRb.transform.rotation = Quaternion.LookRotation(input_Rstick);
                Debug.Log("if b");
            }

        }

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("L_Bumper"))
        {
            playerSpeed = 5.0f;
        }
        else
        {
            playerSpeed = 2.0f;
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

    }

    void CheckMoveState()
    {
        canMove = !playerAc.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1");  //canMove is opposite of attack state
    }

}
