using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Fireball : MonoBehaviour
{

    public float projectileSpeed = 5.0f;
    public float projectileLife;

    void Start()
    {

        Destroy(this.gameObject, 1.0f); //way easier than a fucking ienumerator

    }

    void LateUpdate()
    {
        this.transform.position += transform.forward * Time.deltaTime * projectileSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        float health = other.GetComponent<EnemyManager>().health;
        if (other == GameObject.FindGameObjectWithTag("Enemy").GetComponent<Collider>())
            health = health - 10; 
    }

       
    }
