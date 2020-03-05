using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PointsPickup : MonoBehaviour
{ 
    [Header("Points")]
    public int value;

    [Header("Particle Effect")]
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

            GameObject clone = Instantiate(pickupEffect, transform.position,transform.rotation);

            Destroy(gameObject);

            Destroy(clone, 0.2f);
        }
    }
}
