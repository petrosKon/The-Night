using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //determines in what radius the player is seen by the enemy
    public float lookRadius = 10f;

    Transform target; //The player that the enemy is chasing
    NavMeshAgent agent; //The agent that determines our enemy
    Animator enemyAnimator; //animator for basic animations
    
    //determines and destroys one of the two enemy objects!!
    private bool isDestroyed;

   // [SerializeField]
    private float timeOfDay; //this is needed for the enemy power up   
    private bool enemyPowerUp = false;  //this bool shows if the enemy gets the power up boost at night
    public GameObject pickUpEffect; //pick up effect case an enemy touches each other

    //Static variables that we need in our code for the enemies!!!
    public static Vector3 finalEnemyScale = new Vector3(2f, 2f, 2f);
    public static Vector3 startingEnemyScale = new Vector3(1f, 1f, 1f);
    public static Vector3 stoppingVelocity = new Vector3(0f, 0f, 0f);

    //Power up Multipliers
    public static float nightPowerUpMultiplier = 1.4f;
    public static float combinePowerUpMultiplier = 1.2f;
    public static float maxEnemyEasySpeed = 8f;

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

        EnemyMovement();

        PowerUpEnemies();
    }

    //enemy movement to follow the player
    public void EnemyMovement()
    {
        if(target != null)
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

    //enemy power up during night time hours
    private void PowerUpEnemies()
    {
        timeOfDay += Time.deltaTime * 0.2f;
        timeOfDay %= 24;

        //Night Enters
        if (timeOfDay <= 6 || timeOfDay >= 19)
        {
            if (!enemyPowerUp)
            {
                enemyPowerUp = true;
                EnemyEasyMaxScale(nightPowerUpMultiplier);
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
        PowerEnemyPickup(other);

    }

    //The enemy gets a boost by touching another enemy
    public void PowerEnemyPickup(Collider other)
    {

        //if an enemy touches another enemy
        if(other.tag == "Enemy")
        {
            try
            {

                if (other.gameObject.GetComponent<EnemyController>() != null)
                {

                    if (!other.gameObject.GetComponent<EnemyController>().isDestroyed)
                    {
                        isDestroyed = true;
                        EnemyEasyMaxScale(combinePowerUpMultiplier);
                        Instantiate(pickUpEffect, transform.position, transform.rotation);
                        Destroy(other.gameObject);
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
    public void EnemyEasyMaxScale(float increaseMultiplier)
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
            agent.speed = maxEnemyEasySpeed;
        }
    }

}
