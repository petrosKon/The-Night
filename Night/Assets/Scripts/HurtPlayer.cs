using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{

    void Start()
    {

      
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {         
          FindObjectOfType<GameManager>().DamagePlayer();     
        }
    }
}
