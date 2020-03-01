using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsPickup : MonoBehaviour
{ 
    public int value;

    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("PlayerMax"))
        {
            FindObjectOfType<GameManager>().AddPoints(value);

            Instantiate(pickupEffect, transform.position,transform.rotation);

            Destroy(gameObject);
        }
    }
}
