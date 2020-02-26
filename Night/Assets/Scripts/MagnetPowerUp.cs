using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetPowerUp : MonoBehaviour
{
    public float magnetSize = 10f;
    public float magnetDuration = 5f;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            
        }
    }
}
