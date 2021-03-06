﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArcher : MonoBehaviour
{
    [Header("Variables")]
    public float speed;                 //speed of the archer
    public float stoppingDistance;      //The distance where the archer stops
    public float retreatDistance;       //The distance where the archer goes back
    public float distanceToShoot;       //the distance where the archer start shooting
    public float startTimeBtwShots;     //time between shots

    private float timeBtwShots;

    [Header("Objects")]
    public GameObject projectile;                   //Arrow projectile

    private Transform playerTarget;                       //Our Player
    private LightingManager lightingManager;        //In order to access the time of day

    // Start is called before the first frame update
    void Start()
    {
        playerTarget = PlayerManager.instance.player.transform;
        timeBtwShots = startTimeBtwShots;
        lightingManager = FindObjectOfType<LightingManager>();
    }

    // Update is called once per frame
    void Update()
    {
        ArcherMovement();
    }

    private void ArcherMovement()
    {
        if (playerTarget != null)
        {
            float distancePlayer = Vector3.Distance(transform.position, playerTarget.position);

            //if player goes within radius then shoot the player
            if (distancePlayer < distanceToShoot)
            {
                if (distancePlayer > stoppingDistance)
                {
                    LookRotation();
                    transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, speed * Time.deltaTime);
                    GetComponent<Animator>().SetBool("Run", true);

                }
                else if (distancePlayer < stoppingDistance && distancePlayer > retreatDistance)
                {

                    transform.position = this.transform.position;
                    GetComponent<Animator>().SetBool("Run", false);


                }
                else if (distancePlayer < retreatDistance)
                {
                    LookRotation();
                    transform.position = Vector3.MoveTowards(transform.position, playerTarget.position, -speed * Time.deltaTime);
                    GetComponent<Animator>().SetBool("Run", true);

                }

                if (timeBtwShots <= 0)
                {
                    LookRotation();
                    GameObject clone = Instantiate(projectile, transform.position, transform.rotation);
                    //rotate the arrow to match the firing point of our player!
                    clone.transform.Rotate(new Vector3(0f, -90f, 90f));
                    GetComponent<Animator>().SetBool("Arrow Attack", true);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }
            }
            //case the player runs away from the enemy radius
            //Stop the enemy
            else
            {
                GetComponent<Animator>().SetBool("Run", false);
                transform.position = this.transform.position;
            }
        }
    }

    void LookRotation()
    {
        Vector3 relativePos = playerTarget.position - transform.position;
        if (relativePos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = rotation;
        }
    }
}
