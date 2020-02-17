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

    [SerializeField]
    private float timeOfDay;
    private bool enemyPowerUp = false;

    //pick up effect
    public GameObject pickUpEffect;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
        timeOfDay = FindObjectOfType<LightingManager>().timeOfDay;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius)
        {
            
            agent.SetDestination(target.position);

            enemyAnimator.SetBool("Run Forward", true);

        } 

        //The player has gotten away from our enemies so they stopped moving
        if(agent.velocity == stoppingVelocity)
        {
            enemyAnimator.SetBool("Walk Forward", false);

        }

        timeOfDay += Time.deltaTime * 0.2f;
        timeOfDay %= 24;

        PowerUpEnemies(timeOfDay);

    }

    private void PowerUpEnemies(float timeOfDay)
    {
        //Night Enters
        if (timeOfDay <= 6 || timeOfDay >= 19)
        {
            if (!enemyPowerUp)
            {
                enemyPowerUp = true;
                transform.localScale *= 1.4f;
                agent.speed *= 1.4f;
            }
        }
        else
        {
            if (enemyPowerUp)
            {
                enemyPowerUp = false;
                transform.localScale *= 0.8f;
                agent.speed *= 0.8f;
            }
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
                Instantiate(pickUpEffect, transform.position, transform.rotation);
                Destroy(other.gameObject);
            }
        }
    }
}
