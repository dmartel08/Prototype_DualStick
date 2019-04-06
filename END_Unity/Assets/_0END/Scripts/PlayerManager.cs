using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private GameObject player;
    private Rigidbody playerRb;
    public float playerSpeed = 2;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0.0f, Input.GetAxisRaw("Vertical"));
        Vector3 inputRot = new Vector3(Input.GetAxisRaw("RStick_Horizontal"), 0.0f, -Input.GetAxisRaw("RStick_Vertical"));
        //Vector3 inputRot = new Vector3(0.0f, Input.GetAxisRaw("Horizontal_Alt"), 0.0f);
        // input = transform.TransformDirection(input);
        //inputRot = transform.rotation
        playerRb.MovePosition(transform.position + input * Time.deltaTime * playerSpeed);

        if(inputRot.x != 0 || input.y !=0)
        {
            playerRb.transform.rotation = Quaternion.LookRotation(inputRot);
        }
        

        //Quaternion deltaRotation = Quaternion.Euler(inputRot * Time.deltaTime * 200.0f);
        //playerRb.MoveRotation(playerRb.rotation * deltaRotation);

        if (Input.GetAxisRaw("RStick_Horizontal") > 0)
        {
            Debug.Log(Input.GetAxisRaw("RStick_Horizontal") + " X IS DOING SOMETHING");
        }
        if (Input.GetAxisRaw("RStick_Vertical") > 0)
        {
            Debug.Log(Input.GetAxisRaw("RStick_Vertical") + " Y IS DOING SOMETHING");
        }
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetButton("L_Bumper"))
        {
            playerSpeed = 5.0f;
        }
        else
        {
            playerSpeed = 2.0f;
        }
        

        //playerRb.MoveRotation(Quaternion.identity + )
    }
}
