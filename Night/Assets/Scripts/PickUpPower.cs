using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPower : MonoBehaviour
{
    [Header("Variable")]
    public float sizeIncreaseMultiplier;
    public float moveDecreaseMultiplier;
    private int value = 100;
    //public int healAmount; 

    [Header("Particle Effect")]
    public GameObject pickupEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().AddPoints(value);

            GameObject clone = Instantiate(pickupEffect, transform.position, transform.rotation);

            Destroy(gameObject);

            Destroy(clone, 0.2f);

        }
    }
}
