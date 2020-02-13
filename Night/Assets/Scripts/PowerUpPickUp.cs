using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPickUp : MonoBehaviour
{
    public float sizeIncreaseMultiplier;
    public float moveDecreaseMultiplier;
    public int healAmount; 

    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<GameManager>().ScaleLerper(sizeIncreaseMultiplier, moveDecreaseMultiplier);

            FindObjectOfType<HealthManager>().HealPlayer(healAmount);

            Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);
        }
    }
}
