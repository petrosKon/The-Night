using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject[] teleportortationDevices;
    public Transform playerTransform;
    public float teleportationTime = 5f;    //prevents from teleporting within 5 seconds

    // Start is called before the first frame update
    void Start()
    {
        teleportortationDevices = GameObject.FindGameObjectsWithTag("Teleport");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

     void Update()
    {
        teleportationTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        //creates serial teleportation devices
        if(other.tag == "Player")
        {
            int random = Random.Range(0, teleportortationDevices.Length);
            GameObject randomTeleporter = teleportortationDevices[random];

                if(randomTeleporter != this.gameObject)
                {
                    if (teleportationTime <= 0f)
                    {
                        other.GetComponent<CharacterController>().enabled = false;
                        other.transform.position = randomTeleporter.transform.position;
                        teleportationTime = 5f;
                        other.GetComponent<CharacterController>().enabled = true;
                    }
                }
               
            }                   
        }
    
}
