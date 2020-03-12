using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [Header("GameObjects")]
    private GameObject[] teleportortationDevices;

    private GameObject player;
    GameObject randomTeleporter;

    public static float teleportationTime = 5f;    //prevents from teleporting within 5 seconds

    // Start is called before the first frame update
    void Start()
    {
        teleportortationDevices = GameObject.FindGameObjectsWithTag("Teleport");
        player = PlayerManager.instance.player;

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
        if (other.CompareTag("Player"))
        {
            if (player.transform != null)
            {
                if (teleportationTime <= 0f)
                {
                    player.transform.position = randomTeleporter.transform.position;
                    teleportationTime = 5f;
                }
            }
        }                            
    }   
}
