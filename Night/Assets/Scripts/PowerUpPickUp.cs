using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    public float sizeIncreaseMultiplier;
    public float moveDecreaseMultiplier;
    private int value = 100;
    //public int healAmount; 

    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("PlayerMax"))
        {
           // FindObjectOfType<GameManager>().ScaleLerper(sizeIncreaseMultiplier, moveDecreaseMultiplier);

            FindObjectOfType<GameManager>().AddPoints(value);

            //uncomment if you want to add life to the player
            //FindObjectOfType<HealthManager>().HealPlayer(healAmount);

           GameObject clone = Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);

            Destroy(clone, 0.2f);

        }
    }
}
