using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform target;
    NavMeshAgent agent;
    Animator enemyAnimator;
    Vector3 stoppingVelocity = new Vector3(0f, 0f, 0f);
    public bool isDestroyed;
    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            enemyAnimator.SetBool("Walk Forward", true);
        } 

        //The player has gotten away from our enemies so they stopped moving
        if(agent.velocity == stoppingVelocity)
        {
            enemyAnimator.SetBool("Walk Forward", false);

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
       
       if (other.gameObject.GetComponent<EnemyController>() != null) {

            if (!other.gameObject.GetComponent<EnemyController>().isDestroyed)
            {
                isDestroyed = true;
                transform.localScale *= 1.2f;
                agent.speed *= 1.2f;
                Destroy(other.gameObject);
            }
        }
    }
}
