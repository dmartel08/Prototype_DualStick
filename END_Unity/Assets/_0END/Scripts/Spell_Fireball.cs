using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell_Fireball : MonoBehaviour
{

    public float projectileSpeed = 5.0f;
    public float projectileLife;

    public EnemyManager enemy_M = null;

    void Start()
    {

        Destroy(this.gameObject, 1.0f); //way easier than a fucking ienumerator

    }

    void LateUpdate()
    {
        this.transform.position += transform.forward * Time.deltaTime * projectileSpeed;
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("FUCKING DO SOMETHING" + other.gameObject);

    //    other.gameObject.GetComponent<EnemyManager>().health = other.gameObject.GetComponent<EnemyManager>().health - 30.0f;
       
    //}

       
 }
