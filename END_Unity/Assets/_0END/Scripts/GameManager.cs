using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public GameObject player1GO;
    public GameObject enemyProtoGO;

    public Collider p1Trigger;
    

    // Start is called before the first frame update
    void Start()
    {

        p1Trigger = player1GO.transform.Find("Player_Trigger").GetComponent<Collider>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
