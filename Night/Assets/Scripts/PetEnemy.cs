using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetEnemy : MonoBehaviour
{
    [Header("Variables")]
    public float distanceToEnemy;   //the distance in order to follow the enemy
    public float distanceToAttack;  //distance in order to attack the enemy
    public float distanceToPlayer;  //distance to stop away from the player
    public float speed = 3f;        //flower speed
    public Vector3 offset;      //offset in which the plant must follow

    [Header("GameObjects")]
    public GameObject fireAttack;

    [HideInInspector]
    public GameObject[] nearbyEnemies;
    [HideInInspector]
    public Transform[] enemiesTransform;

    private Transform targetToFollow;       //target to follow aka player


    // Start is called before the first frame update
    void Start()
    {
        targetToFollow = PlayerManager.instance.player.transform;

        offset = new Vector3(0f, transform.position.y, 5f);

        StartCoroutine(SearchForEnemies());

    }

    // Update is called once per frame
    void Update()
    {
        if (targetToFollow != null)
        {

            try
            {
                //we find the closest enemy by calling this function
                Transform closestEnemy = GetClosestEnemy(enemiesTransform);
                //if the plants distance from the enemy is less than the distance from the player then follow and try to kill an enemy
                if (Vector3.Distance(closestEnemy.position, transform.position) < Vector3.Distance(targetToFollow.transform.position + offset, transform.position)
                    || Vector3.Distance(closestEnemy.position, transform.position) < distanceToEnemy)       //prevents from sticking to our player when an enemy passes by
                {

                    //if the player is within plant attack distance then attack
                    if (Vector3.Distance(transform.position,closestEnemy.position) < distanceToAttack)
                    {
                        LookRotation(closestEnemy);
                        //lock the plant position
                        transform.position = this.transform.position;
                        StartCoroutine(Flamethrower());

                    }
                    //move towards the enemy in order to attack him
                    else
                    {
                        LookRotation(closestEnemy);
                        transform.position = Vector3.MoveTowards(transform.position, closestEnemy.position, speed * Time.deltaTime);
                        StopFireAttack();
                    }
                }
                else
                {
                    //check if the plant is closer or not to the player
                    if (Vector3.Distance(transform.position,targetToFollow.transform.position + offset) > distanceToPlayer)
                    {
                        StopFireAttack();
                        LookRotation(targetToFollow);
                        transform.position = Vector3.MoveTowards(transform.position, targetToFollow.transform.position + offset, speed * Time.deltaTime);
                        GetComponent<Animator>().SetBool("Walk", true);
                    }
                    else
                    {
                        GetComponent<Animator>().SetBool("Walk", false);

                    }
                }

            }
            //This means that our target has died
            //reset our enemies in order to find again the nearest
            catch (MissingReferenceException e)
            {
                StartCoroutine(SearchForEnemies());
            }
            catch (NullReferenceException e)
            {

            }


        }
    }

    IEnumerator SearchForEnemies()
    {
        yield return new WaitForSeconds(1);

        if (nearbyEnemies != null)
        {
            nearbyEnemies = GameObject.FindGameObjectsWithTag("Enemy");
            enemiesTransform = new Transform[nearbyEnemies.Length];

            //loop through the 3 closer enemies
            for (int i = 0; i < nearbyEnemies.Length; i++)
            {
                enemiesTransform[i] = nearbyEnemies[i].transform;
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

    Transform GetClosestEnemy(Transform[] enemies)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget;
            }
        }

        return bestTarget;
    }

    //due to the animation delay
    public IEnumerator Flamethrower()
    {
        GetComponent<Animator>().SetBool("Walk", false);

        GetComponent<Animator>().SetBool("Breath Attack", true);

        yield return new WaitForSeconds(1);

        fireAttack.SetActive(true);

    }

    void StopFireAttack()
    {
        fireAttack.SetActive(false);
        GetComponent<Animator>().SetBool("Walk", true);
        GetComponent<Animator>().SetBool("Breath Attack", false);
    }
}
