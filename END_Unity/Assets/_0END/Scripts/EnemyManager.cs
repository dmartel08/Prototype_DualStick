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
        attacking
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

    private void OnTriggerEnter(Collider other)
    {

        Collider playerTrigger = gM.p1Trigger;

        if (other == playerTrigger)
        {
            found = true;
            Debug.Log("I ran into player");
            Debug.Log(other);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        Collider playerTrigger = gM.p1Trigger;

        if(other == playerTrigger)
        {
            found = false;
            Debug.Log("I lost player");
        }
    }

}
