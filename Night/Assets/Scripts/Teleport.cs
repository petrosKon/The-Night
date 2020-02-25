using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject[] teleportortationDevices;
    private GameObject thisTeleport;
    public Transform playerTransform;
    public float transportationTime = 5f;

    // Start is called before the first frame update
    void Start()
    {
        teleportortationDevices = GameObject.FindGameObjectsWithTag("Teleport");
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        thisTeleport = this.gameObject;
    }

     void Update()
    {
        transportationTime -= Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(transportationTime <= 0)
            {
                other.GetComponent<CharacterController>().enabled = false;
                int RandomTeleport = Random.Range(0, teleportortationDevices.Length);
                other.transform.position = teleportortationDevices[RandomTeleport].transform.position;
                transportationTime = 5f;
                other.GetComponent<CharacterController>().enabled = true;
            }           
        }
    }
}
