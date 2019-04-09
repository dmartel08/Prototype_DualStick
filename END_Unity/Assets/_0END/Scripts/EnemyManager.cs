using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    GameManager gM;
    private Rigidbody enemyRb;
    public float enemySpeed = 2.5f;

    public Animator AC;
    AnimatorStateInfo animState;

    private NavMeshAgent agent;

    private enum FIGHTSTATE
    {
        idle,
        patrolling,
        engaging,
        attacking,
        damaged,
        dead
    };

    private int behaviour;
    private bool found = false;


    void Start()
    {
        gM = FindObjectOfType<GameManager>();
        AC = this.GetComponent<Animator>();
        agent = this.GetComponent<NavMeshAgent>();
        behaviour = (int)FIGHTSTATE.engaging;
    }

    void LateUpdate()
    {
        EnemyBehaviour(EnemyState());
    }

    void EnemyBehaviour(int state)
    {
        if(state == (int)FIGHTSTATE.idle)
        {
            //do nothing
        }

        if(state == (int)FIGHTSTATE.engaging)
        {
            agent.destination = gM.player1GO.transform.position;
            AC.SetFloat("speedh", 1);
        }

        if (state == (int)FIGHTSTATE.attacking)
        {
            agent.destination = this.transform.localPosition;
            if (!AC.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1"))
            {
                
                this.transform.LookAt(gM.player1GO.transform.position);
                AC.SetTrigger("Attack1h1");
            }
            
        }
    }

    public int EnemyState()
    {
        if(found == false)
        {
            behaviour = (int)FIGHTSTATE.engaging;
        }
        

        if(found == true)
            {
            behaviour = (int)FIGHTSTATE.attacking;
            }

        return behaviour;
    }

    /// <summary>
    /// Be aware of what colliders. Whethere it's "sword" or hitbox. Or Enter/Stay, etc..
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {

        Collider playerTrigger = gM.player1_M.trigger;

        if (other == playerTrigger)
        {
            found = true;
            Debug.Log("I ran into player");
            Debug.Log(other);
        }

        //If in OnTriggerEnter, calls once. (The proper call it would seem)
        if (other == gM.player1_M.hitZone)  //hitZone, not sword
        {
            if (gM.player1_M.weaponEvent.hitting == true)
            {
                Debug.Log("I the " + this.gameObject + "am hit!");
            }
        }

    }

    private void OnTriggerStay(Collider other)
    {
        ////If in OnTriggerStay, calls for as long as true... multiple hits (wrong it seems).
        //Collider playerTrigger = gM.player1_M.trigger;

        //if (other == gM.player1_M.sword)
        //{
        //    if (gM.player1_M.weaponEvent.hitting == true)
        //    {
        //        Debug.Log("I the " + this.gameObject + "am hit!");
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {

        Collider playerTrigger = gM.player1_M.trigger;

        if (other == playerTrigger)
        {
            found = false;
            Debug.Log("I lost player");
        }
    }

}
