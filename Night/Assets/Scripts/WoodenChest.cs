using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenChest : MonoBehaviour
{
    public GameObject pickupEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().maxNumberOfExplosions++;
            FindObjectOfType<PlayerController>().explosionCount = 0;
            Instantiate(pickupEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
}
