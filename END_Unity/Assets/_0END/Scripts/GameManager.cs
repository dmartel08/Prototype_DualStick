using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player1GO;
    public PlayerManager player1_M;
    public GameObject enemyProtoGO;

    public Transform enemySpawn;
    public GameObject fireballGO;

    // Start is called before the first frame update
    void Start()
    {
        player1_M = player1GO.GetComponent<PlayerManager>();

        
    }

   public void SpawnEnemy()
    {
        Instantiate(enemyProtoGO, enemySpawn.position, Quaternion.identity);
    }

    public void MyDebug()
    {
        Debug.Log("This button was pressed");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
