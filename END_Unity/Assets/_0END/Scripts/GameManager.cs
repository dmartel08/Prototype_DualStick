using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject myGO;
    public GameObject player1GO;

    // Start is called before the first frame update
    void Start()
    {


        if (myGO != null)
        {
            Debug.Log("I exist");
        }
        else
            Debug.Log("I don't exist");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
