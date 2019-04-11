using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public GameObject player1GO;
    public PlayerManager player1_M;
    public GameObject enemyProtoGO;
    public GameObject fireballGO;

    // Start is called before the first frame update
    void Start()
    {
        player1_M = player1GO.GetComponent<PlayerManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
