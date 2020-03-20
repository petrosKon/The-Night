using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBat : MonoBehaviour
{
    [Header("Variables")]
    public float speed;

    [Header("GameObjects")]
    public GameObject soundwaveParticles;

    [Header("Particle Effect")]
    public GameObject pickUpEffect; //pick up effect case an enemy touches each other

    private float distanceToFollow = 10f;  //the player need to get really close to the bat
    private Transform playerTarget;
    private Vector3 offset;      //offset in which the plant must follow

    //determines and destroys one of the two enemy objects!!
    private bool isDestroyed;

    //Static variables that we need in our code for the enemies!!!
    public static Vector3 finalEnemyScale = new Vector3(4f, 4f, 4f);
    public static Vector3 startingEnemyScale = new Vector3(2f, 2f, 2f);
    public static Vector3 stoppingVelocity = new Vector3(0f, 0f, 0f);

    float sizeMutliplier = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = PlayerManager.instance.player.transform;

        offset = new Vector3(3f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerTarget != null)
        {
            LookRotation(playerTarget);
            float distancePlayer = Vector3.Distance(transform.position, playerTarget.position);
            if (distancePlayer <= distanceToFollow)
            {
                //create a very big distance in order for the player to not be able to escape the bat
                distanceToFollow = 30f;
                transform.position = Vector3.MoveTowards(transform.position, playerTarget.position - offset, speed * Time.deltaTime);

            }
        }
    }


    void LookRotation(Transform target)
    {
        Vector3 relativePos = target.position - transform.position;
        if (relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
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

                if (other.gameObject.GetComponent<EnemyBat>() != null)
                {

                    if (!other.gameObject.GetComponent<EnemyBat>().isDestroyed)
                    {
                        isDestroyed = true;
                        BatPowerUp(sizeMutliplier);
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
    public void BatPowerUp(float sizeMultiplier)
    {
        //prevent the enemy from getting really big!!!
        if (transform.localScale.magnitude * sizeMultiplier < finalEnemyScale.magnitude)
        {

            transform.localScale *= sizeMultiplier;
        }
        else
        {
            transform.localScale = finalEnemyScale;
        }
    }
}
