﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    private GameObject[] teleportortationDevices;
    public GameObject player;
    public static float teleportationTime = 5f;    //prevents from teleporting within 5 seconds
    GameObject randomTeleporter;

    // Start is called before the first frame update
    void Start()
    {
        teleportortationDevices = GameObject.FindGameObjectsWithTag("Teleport");
        player = GameObject.FindGameObjectWithTag("Player");

        int random = Random.Range(0, teleportortationDevices.Length);
        Debug.Log(random);
        randomTeleporter = teleportortationDevices[random];

        //disable the emmision to show that the teleporter is inactive
        if(randomTeleporter == this.gameObject)
        {
            //GetComponentInChildren<ParticleSystem>().gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }

     void Update()
    {
        if(teleportationTime > 0f)
        {
            teleportationTime -= Time.deltaTime;

        }
        else
        {
            teleportationTime = 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //creates serial teleportation devices
        if(other.tag == "Player")
        {
           

                    if (teleportationTime <= 0f)
                    {
                         player.transform.position = randomTeleporter.transform.position;
                        teleportationTime = 5f;
                    }
                }               
                              
        }
    
}
