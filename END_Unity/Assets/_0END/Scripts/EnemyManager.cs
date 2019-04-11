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

    public float health = 100;
    public float damage = 0;

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
    public bool found = false;
    public bool gotHit = false;


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
            agent.isStopped = false; //allow agent to seek again
            agent.destination = gM.player1GO.transform.position;
            AC.SetFloat("speedh", 1);
        }

        if (state == (int)FIGHTSTATE.attacking)
        {

            if (AC.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1"))
            {
                agent.isStopped = true; //stop the agent from seeking/moving
                agent.velocity = Vector3.zero; //any latent velocity should be zerod (no momentum)
            }
                
            if (!AC.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1"))
            {
                
                this.transform.LookAt(gM.player1GO.transform.position);
                AC.SetTrigger("Attack1h1");
            }
            
        }

        if(state == (int)FIGHTSTATE.damaged)
        {
            agent.isStopped = true; //stop the agent from seeking/moving
            agent.velocity = Vector3.zero; //any latent velocity should be zerod (no momentum)
           // health = health - damage;

        }
    }

    public int EnemyState()
    {
        if(gotHit == true)
        {
            behaviour = (int)FIGHTSTATE.damaged;
            return behaviour;
        }

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

    public void FinishedHit()
    {
        Debug.Log("THE FINISHED HIT WAS HIT");
        gotHit = false;
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
        }

        ////If in OnTriggerEnter, calls once. (The proper call it would seem)
        if (other == gM.player1_M.hitZone)  //hitZone, not sword
        {

            //if (gM.player1_M.weaponEvent.hitting == true)
            //{
                Debug.Log("I the " + this.gameObject + "am hit by SWORD!");
                AC.SetTrigger("Hit1");
                damage = 10;  //set the damage based on weapon, so ranged hitbox might change this to 20
                gotHit = true;
                health = health - damage;  //fucking fuck why doesn't this just decrease BY FUCKING TEN
            //}
        }

        if (other == gM.player1_M.sp_fireball.GetComponent<Collider>())
        {
            Debug.Log("I the " + this.gameObject + "am hit by FIREBALL!");
            AC.SetTrigger("Hit1");
            damage = 30;  //set the damage based on weapon, so ranged hitbox might change this to 20
            gotHit = true;
            health = health - damage;
        }

    }

    private void OnTriggerStay(Collider other)
    {
        ////If in OnTriggerStay, calls for as long as true... multiple hits (wrong it seems).
        //Collider playerTrigger = gM.player1_M.trigger;
        
       
    }

    private void OnTriggerExit(Collider other)
    {

        Collider playerTrigger = gM.player1_M.trigger;

        if (other == playerTrigger)
        {
            
            found = false;
        }
    }

}
