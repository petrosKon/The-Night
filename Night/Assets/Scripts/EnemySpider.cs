using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpider : MonoBehaviour
{
    //determines in what radius the player is seen by the enemy
    [Header("Player Detection Radius")]
    public float lookRadius = 10f;
    public float combinePowerUpMultiplier = 1.2f;
    public float maxSpeed;

    Transform target; //The player that the enemy is chasing
    NavMeshAgent agent; //The agent that determines our enemy
    Animator enemyAnimator; //animator for basic animations

    //determines and destroys one of the two enemy objects!!
    private bool isDestroyed;

    [Header("Particle Effect")]
    public GameObject pickUpEffect; //pick up effect case an enemy touches each other

    //Static variables that we need in our code for the enemies!!!
    public static Vector3 finalEnemyScale = new Vector3(2f, 2f, 2f);
    public static Vector3 startingEnemyScale = new Vector3(1f, 1f, 1f);
    public static Vector3 stoppingVelocity = new Vector3(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();

        maxSpeed =  2 * agent.speed;
    }

    // Update is called once per frame
    void Update()
    {

        EnemyMovement();

    }

    //enemy movement to follow the player
    public void EnemyMovement()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(target.position, transform.position);

            if (distance <= lookRadius)
            {

                agent.SetDestination(target.position);

                enemyAnimator.SetBool("Run Forward", true);

            }

            //The player has gotten away from our enemies so they stopped moving
            //Transition back to idle state
            if (agent.velocity == stoppingVelocity)
            {
                enemyAnimator.SetBool("Run Forward", false);
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
        PowerEnemyPickup(other);

    }

    //The enemy gets a boost by touching another enemy
    public void PowerEnemyPickup(Collider other)
    {
        //if an enemy touches another enemy
        if (other.CompareTag("Enemy"))
        {
            try
            {

                if (other.gameObject.GetComponent<EnemySpider>() != null)
                {

                    if (!other.gameObject.GetComponent<EnemySpider>().isDestroyed)
                    {
                        isDestroyed = true;
                        SpiderPowerUp(combinePowerUpMultiplier);
                        GameObject clone = Instantiate(pickUpEffect, transform.position, transform.rotation);
                        Destroy(other.gameObject);
                        Destroy(clone, 0.2f);
                    }
                }

            }
            catch (NullReferenceException e)
            {
                Debug.LogError(e);
            }
        }
    }

    //Increases the enemy size
    public void SpiderPowerUp(float increaseMultiplier)
    {
        //prevent the enemy from getting really big!!!
        if (transform.localScale.magnitude * increaseMultiplier < finalEnemyScale.magnitude)
        {

            transform.localScale *= increaseMultiplier;
            agent.speed *= increaseMultiplier;
        }
        else
        {
            transform.localScale = finalEnemyScale;
            agent.speed = maxSpeed;
        }
    }
}
